using System;
using System.Collections.Generic;
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
                            Label =( attr.Label ?? prop.Name).ToLowerCamelCase(),
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

                return new ElFormModel
                {
                    FormName = type.GetCustomAttribute<ElFormAttribute>()?.FormName ?? type.Name,
                    FormItems = formItems
                };
            }).ToList();

            return elForms;
        }

        #endregion

        #endregion

    }

}