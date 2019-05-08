using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Abbott.Tips.ApiCore;
using Abbott.Tips.ApiCore.Corss;
using Abbott.Tips.ApiCore.Events;
using Abbott.Tips.Application.ApplicationSettings;
using Abbott.Tips.AspnetCore.Middlewares;
using Abbott.Tips.Framework.Attributes;
using Abbott.Tips.Framework.ElementUI;
using Abbott.Tips.Framework.EventBus;
using Abbott.Tips.Framework.EventBus.Services;
using Abbott.Tips.WebHost.Hubs;
using Herbalife_HGDX.MVC.Authority;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NLog.Extensions.Logging;
using NLog.Web;
using Swashbuckle.AspNetCore.Swagger;

namespace Abbott.Tips.WebHost
{
    public class Startup : ApiCoreStartup
    {
        public Startup(IConfiguration configuration) : base(configuration)
        {
        }

        // This method gets called by the runtime. Use this method to add services to the container.

        public override IServiceProvider ConfigureServices(IServiceCollection services)
        {
            #region Session & Cookie


            services.Configure<IISOptions>(options =>
            {
                options.ForwardClientCertificate = false;
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                // options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
                options.HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.Always;
            });

            // 增加Cookie中间件配置
            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = "TIPS-CookieAuthenticationScheme";
            //    options.DefaultChallengeScheme = "TIPS-CookieAuthenticationScheme";
            //    options.DefaultSignInScheme = "TIPS-CookieAuthenticationScheme";
            //})
            //.AddCookie("TIPS-CookieAuthenticationScheme", options =>
            //{
            //    options.AccessDeniedPath = @"/AccessDenied";
            //    options.LoginPath = "/Account/Login";
            //    options.SlidingExpiration = true;
            //    options.Cookie.HttpOnly = true;
            //});

            #endregion

            #region 实现资源授权Handler

            // 将Handler注册到DI系统中
            services.AddSingleton<IAuthorizationHandler, TipsResourceAuthorizationHandler>();

            #endregion

            #region 实现权限授权Handler

            // 将Handler注册到DI系统中
            services.AddSingleton<IAuthorizationHandler, TipsPermissionAuthorizationHandler>();

            #endregion

            #region 自定义过滤器



            #endregion

            #region 目录浏览功能

            services.AddDirectoryBrowser();

            #endregion

            services.AddTransient<IEventHandler, UserLoginedEventHandler>();
            //services.AddTransient<IEventStore>(serviceProvider => new EntityFrameworkEventStore());
            services.AddTransient<IEventStore, EntityFrameworkEventStore>();
            services.AddSingleton<IEventBus, PassThroughEventBus>();

            #region CROS

            //services.AddCors(option => option.AddPolicy("Tips", policy => policy.AllowAnyHeader().AllowAnyMethod().AllowCredentials().AllowAnyOrigin()));

            //services.Add(ServiceDescriptor.Transient<ICorsService, WildcardCorsService>());
            //services.Configure<CorsOptions>(options => options.AddPolicy(
            //    "AllowSameDomain",
            //    builder => builder.WithOrigins("*.cmono.net")));
            services.AddCors(option => option.AddPolicy("Tips", policy => policy.WithOrigins("http://localhost:8090").AllowAnyMethod().AllowAnyHeader().AllowCredentials()));

            services.AddCors(options =>
            {
                options.AddPolicy("SignalrCore",
                    policy => policy.AllowAnyOrigin()
                                    .AllowAnyHeader()
                                    .AllowAnyMethod());
            });

            //配置跨域处理
            //services.AddCors(options =>
            //{
            //    options.AddPolicy("any", builder =>
            //    {
            //        builder.AllowAnyOrigin() //允许任何来源的主机访问
            //        .AllowAnyMethod()
            //        .AllowAnyHeader()
            //        .AllowCredentials();//指定处理cookie
            //    });
            //});

            #endregion

            //注册Swagger生成器，定义一个和多个Swagger 文档
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "TIPS API",
                    Description = "A simple example ASP.NET Core Web API",
                    TermsOfService = "None",
                    Contact = new Contact
                    {
                        Name = "常伟华 Lance.Chang",
                        Email = string.Empty,
                        Url = "http://www.cmono.net"
                    },
                    License = new License
                    {
                        Name = "知识共享署名-相同方式共享 4.0 国际许可协议",
                        Url = "https://creativecommons.org/licenses/by-sa/4.0/"
                    }
                });

                // 为 Swagger JSON and UI设置xml文档注释路径
                // AppContext.BaseDir
                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);//获取应用程序所在目录（绝对，不受工作目录影响，建议采用此方法获取路径）
                var xmlPath = Path.Combine(basePath, "Abbott.Tips.WebHost.xml");
                c.IncludeXmlComments(xmlPath);
            });

            #region SignalR

            services.AddSignalR();

            #endregion

            var provider = base.ConfigureServices(services);

            return provider;
        }

        private static void HandleMapTest1(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync("Map Test 1");
            });
        }

        private static void HandleMapTest2(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync("Map Test 2");
            });
        }

        public override void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            #region 中间件管道

            #region 无分支管道

            ////A(before)
            ////B(before)
            ////C
            ////B(after)
            ////A(after)

            //// Middleware A
            //app.Use(async (context, next) =>
            //{
            //    Console.WriteLine("A (before)");
            //    await next();
            //    Console.WriteLine("A (after)");
            //});

            //// Middleware B
            //app.Use(async (context, next) =>
            //{
            //    Console.WriteLine("B (before)");
            //    await next();
            //    Console.WriteLine("B (after)");
            //});

            //// Middleware C (terminal)
            //app.Run(async context =>
            //{
            //    Console.WriteLine("C");
            //    await context.Response.WriteAsync("Hello world");
            //});

            #endregion

            #region 无连结分支

            //无连结分支很容易就理解了，就是不同的路径跑不同的分支。
            //如果是有参数匹配的话，就要使用 MapWhen，而 MapWhen 基于给定谓词的结果创建请求管道分支。
            //Func<HttpContext, bool> 类型的任何谓词均可用于将请求映射到管道的新分支。 
            //谓词用于检测查询字符串变量 branch 是否存在。

            //app.Map("/map1", HandleMapTest1);

            //app.Map("/map2", HandleMapTest2);

            //app.Run(async context =>
            //{
            //    await context.Response.WriteAsync("Hello from non-Map delegate. <p>");
            //});

            #endregion

            #region 有连结（重新连接上主管道）分支，创建有连结分支管道就要使用到 UseWhen

            //app.Use(async (context, next) =>
            //{
            //    Console.WriteLine("A (before)");
            //    await next();
            //    Console.WriteLine("A (after)");
            //});

            //app.UseWhen(
            //    context => context.Request.Path.StartsWithSegments(new PathString("/foo")),
            //    a => a.Use(async (context, next) =>
            //    {
            //        Console.WriteLine("B (before)");
            //        await next();
            //        Console.WriteLine("B (after)");
            //    }));

            //app.Run(async context =>
            //{
            //    Console.WriteLine("C");
            //    await context.Response.WriteAsync("Hello world");
            //});

            //像上面的代码，当请求不是以 " /foo " 开头的时候，结果为：
            //A(before)
            //C
            //A(after)

            //当请求是以 " /foo " 开头的时候，结果为：
            //A(before)
            //B(before)
            //C
            //B(after)
            //A(after)

            #endregion

            #endregion

            #region NLog 日志组件

            loggerFactory.AddNLog();    //添加NLog  
            env.ConfigureNLog("nlog.config");    //读取Nlog配置文件 

            #endregion

            #region Cookie

            //app.UseCookiePolicy();

            // 验证中间件
            //app.UseAuthentication();

            #endregion

            #region Authentication

            // 添加权限中间件, 一定要放在app.UseAuthentication后
            // 因为UseAuthentication要从Cookie中加载通过验证的用户信息到Context.User中
            // 所以一定放在加载完后才能去验用户信息（当然自己读取Cookie也可以）

            //app.UsePermission(new PermissionMiddlewareOption()
            //{
            //    LoginAction = @"/login",
            //    NoPermissionAction = @"/denied",
            //    //这个集合从数据库中查出所有用户的全部权限
            //    UserPerssions = new List<UserPermission>()
            //         {
            //             new UserPermission { Url="/", UserName="gsw"},
            //             new UserPermission { Url="/home/contact", UserName="gsw"},
            //             new UserPermission { Url="/home/about", UserName="aaa"},
            //             new UserPermission { Url="/", UserName="aaa"}
            //         }
            //});

            #endregion

            base.Configure(app, env, loggerFactory);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                #region 模拟用户登录

                ////创建一个身份认证
                //var claims = new List<Claim>() {
                //            new Claim(ClaimTypes.Sid, "1"), //用户ID
                //            new Claim(ClaimTypes.Name, "LoginName")  //用户名称
                //        };

                //var identity = new ClaimsIdentity(claims, "HGDX-Login");

                ////ClaimsIdentity的持有者就是 ClaimsPrincipal
                //var userPrincipal = new ClaimsPrincipal(identity);

                ////一个ClaimsPrincipal可以持有多个ClaimsIdentity，就比如一个人既持有驾照，又持有护照.
                //HttpContext.SignInAsync("NET-CMONO-CookieAuthenticationScheme", userPrincipal, new AuthenticationProperties
                //{
                //    ExpiresUtc = DateTime.UtcNow.AddMinutes(20),
                //    IsPersistent = false,
                //    AllowRefresh = false
                //});

                #endregion
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            //请求错误提示配置
            app.UseErrorHandling();

            #region 404等错误处理

            app.UseStatusCodePagesWithReExecute("/error/{0}");

            #endregion

            #region 访问静态文件

            app.UseStaticFiles();

            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images")),
            //    RequestPath = "/files"
            //});

            #endregion

            #region File Server 是把目录浏览（默认关闭的）和静态文件访问合起来

            //app.UseFileServer();

            //app.UseFileServer(new FileServerOptions
            //{
            //    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images")),
            //    RequestPath = "/files",
            //    EnableDirectoryBrowsing = true
            //});

            #endregion

            #region 使用目录浏览,为了能够浏览目录的同时访问文件，UseDirectoryBrowser 与 UseStaticFiles 中的配置要相同，比如，指定的物理路径要相同，分配的相对 URL 要相同。

            app.UseDirectoryBrowser();

            //app.UseDirectoryBrowser(new DirectoryBrowserOptions
            //{
            //    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images")),
            //    RequestPath = "/files"
            //});

            #endregion

            //// 读取读取Views文件夹下的js和css
            //app.UseStaticFiles(new StaticFileOptions()
            //{
            //    FileProvider = new PhysicalFileProvider(
            //        Path.Combine(Directory.GetCurrentDirectory(), @"Views")),
            //        RequestPath = new PathString("/Views"),
            //        ContentTypeProvider = new FileExtensionContentTypeProvider(
            //        new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            //        {
            //            { ".js", "application/javascript" },
            //            { ".css", "text/css" },
            //        }
            //    )
            // });


            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe();
            
            app.UseCors("Tips");//顺序必须在 app.UseSignalR ... 这句设置之前才行，  
            app.UseCors("SignalrCore");
            app.UseSignalR(routes =>
            {
                routes.MapHub<TestHub>("/test");
                routes.MapHub<SignalrHubs>("/signalrHubs");
            });

            //启用中间件服务生成Swagger作为JSON终结点
            app.UseSwagger();
            //启用中间件服务对swagger-ui，指定Swagger JSON终结点
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                //c.RoutePrefix = string.Empty;
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            EntityFrameworkCore.Seed.SeedData.Initialize(app.ApplicationServices); //初始化数据
        }
    }
}