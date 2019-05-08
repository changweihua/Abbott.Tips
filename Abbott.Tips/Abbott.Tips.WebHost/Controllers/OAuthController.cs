using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using Abbott.Tips.ApiCore.Controllers;
using Abbott.Tips.ApiCore.Events;
using Abbott.Tips.ApiCore.Jwts;
using Abbott.Tips.Application.ApplicationSettings;
using Abbott.Tips.Application.Users;
using Abbott.Tips.Framework.Attributes;
using Abbott.Tips.Framework.ElementUI;
using Abbott.Tips.Framework.EventBus;
using Abbott.Tips.Framework.EventBus.Services;
using Abbott.Tips.Framework.FCL;
using Abbott.Tips.WebHost.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Abbott.Tips.WebHost.Controllers
{
    [EnableCors("Tips")]
    public class OAuthController : JwtOauthController
    {
        public UserService iUserService { get; set; }

        public IApplicationSettingService iApplicationSettingService { get; set; }

        public IEventBus EventBus { get; set; }

        public OAuthController(IEventBus eventBus)
        {
            EventBus = eventBus;
        }

        /// <summary>
        /// 返回新的 token 并设置 Cookie
        /// </summary>
        /// <param name="user"></param>
        [HttpPost("Auth")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> Auth([FromBody]JwtAuthenticationParameters parameters)
        {
            if (parameters != null)
            {
                if (parameters.grant_type == "password")
                {
                    var claims = new Claim[]
                    {
                        new Claim(ClaimTypes.WindowsDeviceClaim, parameters.client_id, ClaimValueTypes.String)
                    };
                    await HttpContext.SignInAsync(parameters.client_id, claims, DateTime.Now.AddMinutes(1));
                    await this.EventBus.PublishAsync(new UserLoginedEvent(parameters.username));
                    var elForms = LoadElForm();
                    var jwtAuthenticationResult = DoPassword(parameters);
                    return Ok(new {
                        code = 0,
                        access_token = jwtAuthenticationResult.Token,
                        refresh_token = jwtAuthenticationResult.RefreshToken,
                        expires_in = (int)TimeSpan.FromMinutes(1).TotalSeconds,
                        ElForms = elForms
                    });
                }
                else if (parameters.grant_type == "refresh_token")
                {
                    var jwtAuthenticationResult = DoRefreshToken(parameters);
                    return Ok(new
                    {
                        code = 0,
                        access_token = jwtAuthenticationResult.Token,
                        refresh_token = jwtAuthenticationResult.RefreshToken,
                        expires_in = (int)TimeSpan.FromMinutes(1).TotalSeconds
                    });
                }
            }
            
            return BadRequest();
        }

        //scenario 1 ： get the access-token by username and password  
        private JwtAuthenticationResult DoPassword(JwtAuthenticationParameters parameters)
        {
            //validate the client_id/client_secret/username/passwo  
            //var isValidated = UserInfo.GetAllUsers().Any(x => x.ClientId == parameters.client_id
            //                        && x.ClientSecret == parameters.client_secret
            //                        && x.UserName == parameters.username
            //                        && x.Password == parameters.password);

            var estUser = iUserService.Login(parameters.username, parameters.password);

            if (estUser != null)
            {
                var refresh_token = Guid.NewGuid().ToString().Replace("-", "");
                var token = GetJwtToken(parameters.client_id, refresh_token);

                //缓存中(数据库中)保存RefreshToken

                var jwtAuthenticationResult = new JwtAuthenticationResult
                {
                    ClientId = parameters.client_id,
                    RefreshToken = refresh_token,
                    Token = token,
                    Id = Guid.NewGuid().ToString(),
                    IsStop = 0
                };

                return jwtAuthenticationResult;
            }
            return null;
        }

        //scenario 2 ： get the access_token by refresh_token  
        private JwtAuthenticationResult DoRefreshToken(JwtAuthenticationParameters parameters)
        {
            //缓存中(数据库中)查找RefreshToken
            //查看RefreshToken是否过期，使用过后，标记过期，重新发放
            //var token = _repo.GetToken(parameters.refresh_token, parameters.client_id);

            var refresh_token = Guid.NewGuid().ToString().Replace("-", "");
            var token = GetJwtToken(parameters.client_id, refresh_token);
            var jwtAuthenticationResult = new JwtAuthenticationResult
            {
                ClientId = parameters.client_id,
                RefreshToken = refresh_token,
                Token = token,
                Id = Guid.NewGuid().ToString(),
                IsStop = 0
            };

            return jwtAuthenticationResult;
        }

        //get the jwt token   
        private string GetJwtToken(string client_id, string refresh_token)
        {
            var now = DateTime.UtcNow;

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.WindowsDeviceClaim, client_id, ClaimValueTypes.String)
                //new Claim(JwtRegisteredClaimNames.Sub, client_id),
                //new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                //new Claim(JwtRegisteredClaimNames.Iat, now.ToUniversalTime().ToString(), ClaimValueTypes.Integer64)
            };

            return JwtTokenGenerator.GenerateToken(client_id, claims, DateTime.Now.AddMinutes(1));
        }


        /// <summary>
        /// 返回新的 token 并设置 Cookie
        /// </summary>
        /// <param name="user"></param>
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        public override async Task<IActionResult> Post([FromBody]AccountModel account)
        {
            var elForms = LoadElForm();

            if (account != null && !string.IsNullOrEmpty(account.UserName))
            {
                var estUser = iUserService.Login(account.UserName, account.Password);

                if (estUser != null)
                {
                    // 假的用户信息
                    var claims = new[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, estUser.LoginName, ClaimValueTypes.String)
                    };
                    var token = JwtTokenGenerator.GenerateToken(estUser.LoginName, claims, DateTime.Now.AddDays(1));
                    await HttpContext.SignInAsync(estUser.LoginName, claims, DateTime.Now.AddDays(1));
                    await this.EventBus.PublishAsync(new UserLoginedEvent(estUser.LoginName));
                    return Ok(new { Code = 0, Token = token, User = estUser, ElForms = elForms });
                }
            }

            return BadRequest();
        }

        [HttpGet("GetPermissions")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> GetPermissions()
        {
            var menus = new List<RouteModel>
            {
                new RouteModel
                {
                    Path = "/configuration",
                    name="configuration",
                    redirect="configurationList",
                    component="@/views/layout/Admin.vue",
                    leaf=false,
                     meta=new RouteMeta
                     {
                         title="configuration",
                         requiredLogin=true
                     },
                     children=new List<RouteModel>
                     {
                         new RouteModel
                         {
                            Path = "list",
                            name="configurationList",
                            component="@/views/configuration/List.vue",
                            leaf=true,
                            meta=new RouteMeta
                             {
                                 title="configurationList",
                                 requiredLogin=true
                             }
                         },
                         new RouteModel
                         {
                            Path = "create",
                            name="configurationCreate",
                            component="@/views/configuration/create.vue",
                            leaf=true,
                            meta=new RouteMeta
                             {
                                hidden= true,
                                 title ="configurationCreate",
                                 requiredLogin=true
                             }
                         }
                     }
                }
            };

            return Ok(new { Code = 0, asyncRouters = menus });
        }

        #region 读取系统所有缓存数据

        #region 读取所有模型类，生成ElementUI Form定义

        private List<ElFormModel> LoadElForm()
        {
            Assembly[] assemblies = Directory.GetFiles(AppContext.BaseDirectory, "*.dll").Select(Assembly.LoadFrom).ToArray();
            var elFormTypes = assemblies.SelectMany(ass => ass.GetTypes().Where(t => t.GetCustomAttributes().Any(attr => attr.GetType() == typeof(ElFormAttribute))));

            var elForms = elFormTypes.Select(type =>
            {
                var formItems = type.GetProperties().Where(prop => prop.GetCustomAttribute<ElFormItemIgnoreAttribute>() == null).Select(prop =>
                {
                    var formItem = new ElFormItemModel();

                    var attr = prop.GetCustomAttribute<ElFormItemAttribute>();
                    if (attr != null)
                    {
                        formItem = new ElFormItemModel
                        {
                            Order = attr.Order,
                            Label = (attr.Label ?? prop.Name).ToLowerCamelCase(),
                            Prop = (attr.Label ?? prop.Name).ToLowerCamelCase(),
                            Type = attr.Type.ToString().ToLower(),
                            Hidden = attr.Hidden
                        };
                    }
                    else
                    {
                        formItem = new ElFormItemModel
                        {
                            Order = 0,
                            Label = prop.Name.ToLowerCamelCase(),
                            Prop = prop.Name.ToLowerCamelCase(),
                            Type = prop.PropertyType.ToElFormItemType().ToString().ToLower(),
                            Hidden = false
                        };
                    }


                    return formItem;
                }).ToList();

                dynamic formItemRules = new ExpandoObject();
                IDictionary<string, object> dict = new Dictionary<string, object>();

                type.GetProperties().Where(prop => prop.GetCustomAttribute<ElFormItemIgnoreAttribute>() == null || prop.GetCustomAttribute<ElFormItemRuleAttribute>() == null).ToList().ForEach(prop =>
                {
                    var attrs = prop.GetCustomAttributes<ElFormItemRuleAttribute>().ToList();
                    if (attrs != null && attrs.Count > 0)
                    {
                        string key = prop.Name.ToLowerCamelCase();
                        var value = attrs.Select(attr =>
                        {
                            dynamic obj = new ExpandoObject();
                            if (attr.Max > 0)
                            {
                                obj.max = attr.Max;
                            }
                            if (attr.Min > 0)
                            {
                                obj.min = attr.Min;
                            }
                            if (!string.IsNullOrEmpty(attr.Message))
                            {
                                obj.message = attr.Message;
                            }
                            if (attr.Required)
                            {
                                obj.required = attr.Required;
                            }
                            if (!string.IsNullOrEmpty(attr.Trigger))
                            {
                                obj.trigger = attr.Trigger;
                            }
                            if (!string.IsNullOrEmpty(attr.Type))
                            {
                                obj.type = attr.Type;
                            }
                            if (attr.Triggers != null && attr.Triggers.Count > 0)
                            {
                                obj.triggers = attr.Triggers;
                            }
                            return obj;
                        });
                        dict.Add(new KeyValuePair<string, object>(key, value));
                    }
                });

                foreach (var kv in dict)
                {
                    ((IDictionary<string, object>)formItemRules).Add(kv);
                }

                return new ElFormModel
                {
                    FormName = type.GetCustomAttribute<ElFormAttribute>()?.FormName ?? type.Name,
                    FormItems = formItems,
                    FormRules = formItemRules
                };
            }).ToList();

            return elForms;
        }

        #endregion

        #endregion

    }

}