﻿using Abbott.Tips.ApiCore.Jwts;
using Abbott.Tips.ApiCore.Mappers;
using Abbott.Tips.AspnetCore;
using Abbott.Tips.AspnetCore.Autofacs;
using Abbott.Tips.AspnetCore.BackgroundServices;
using Abbott.Tips.AspnetCore.HttpContexts;
using Abbott.Tips.AspnetCore.Jils;
using Abbott.Tips.AspnetCore.Middlewares;
using Abbott.Tips.EntityFrameworkCore;
using Abbott.Tips.EntityFrameworkCore.Configurations;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abbott.Tips.ApiCore
{
    public class ApiCoreStartup : AspnetCoreStartup
    {
        public ApiCoreStartup(IConfiguration configuration) : base(configuration)
        {
        }

        protected IList<Type> actionFilterTypes = null;

        public virtual IServiceProvider ConfigureServices(IServiceCollection services)
        {
            #region EF 注入

            services.AddDbContext<TipsContext>(opt =>
                {
                    //b => b.UseRowNumberForPaging()，解决低版本SQL数据库不支持OFFSET语法
                    opt.UseSqlServer(Configuration.GetConnectionString("SqlServer"), b => b.UseRowNumberForPaging());
                    //opt.UseRemoveForeignKeyService();
                }).AddUnitOfWork<TipsContext>();

            // 替换控制器所有者
            // 使用Autofac进行注入
            services.Replace(ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator>());

            // 引入 AutoMapper 组件
            // 会在当前程序集自动找出所有继承自 Profile 的子类添加到配置中
            //services.AddAutoMapper();

            #region 不想使用 AddAutoMapper()  通过反射自动找出 Profile ，建议使用这种方式

            services.AddSingleton(AutoMapperConfig.GetConfigurationProvider());
            services.AddScoped<IMapper, Mapper>();

            #endregion

            #endregion

            #region Session & Cookie


            #endregion

            #region 读取自定义配置

            services.AddOptions();

            #endregion

            #region 注册后台服务

            //注册简单的定时任务执行
            services.AddSingleton<Microsoft.Extensions.Hosting.IHostedService, TimedExecutService>();

            #endregion

            #region 开启JWT服务

            // 使用对称加密算法
            services.AddTipsJwt(new MD5JwtOption("dd%88*377f6d&f£$$£$FdddFF33fssDG^!3")
            {
                Audience = "test",
                Issuer = "test",
                EnableCookie = true
            });

            // 或者你可以使用非对称加密算法
            //services.AddTipsJwt(new RSAJwtOption(PlatformServices.Default.Application.ApplicationBasePath)
            //{
            //    Audience = "test",
            //    Issuer = "test",
            //    EnableCookie = true
            //});

            #endregion

            services.AddMemoryCache();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                //禁用自带ModelValidate
                //options.SuppressModelStateInvalidFilter = true;
                //options.SuppressConsumesConstraintForFormFileParameters = true;

           //     options.InvalidModelStateResponseFactory = actionContext =>
           //     {
           ////         var errors = actionContext.ModelState
           ////.Where(e => e.Value.Errors.Count > 0)
           ////.Select(e => new Error
           ////{
           ////    Name = e.Key,
           ////    Message = e.Value.Errors.First().ErrorMessage
           ////}).ToArray();

           //         return new BadRequestObjectResult(actionContext.ModelState.Errors());
           //     };
            });

            services.AddMvc(options =>
            {
                //options.Filters.Add(new AddHeaderAttribute("GlobalAddHeader",
                //    "Result filter added to MvcOptions.Filters")); // an instance

                //options.ValueProviderFactoties.Add(new JQueryQueryStringValueProviderFactory());
                //options.Conventions.Add(new ParameterModelConventions.ArraryHandleQueryConvention());
                //options.ModelBinderProviders.Insert(0, new SplitDateTimeModelBinderProvider());

                // 使用 Jil 替换默认的 JSON 解析
                //options.InputFormatters.Clear();
                //options.InputFormatters.Add(new JilInputFormatter(new Jil.Options()));
                //options.OutputFormatters.Clear();
                //options.OutputFormatters.Add(new JilOutputFormatter());

            }).AddJsonOptions(options =>
            {
                //不使用驼峰样式的key
                //options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                //忽略循环引用
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                //设置时间格式
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd";
            })
            //.SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
            .ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    return new BadRequestObjectResult(actionContext.ModelState.Errors());
                };
                options.SuppressConsumesConstraintForFormFileParameters = true;
                options.SuppressInferBindingSourcesForParameters = true;
                //options.SuppressModelStateInvalidFilter = true;
                //options.SuppressMapClientErrors = true;
                //options.ClientErrorMapping[404].Link =
                //    "https://httpstatuses.com/404";
            });

            //Autofac 注入
            var containerBuilder = new ContainerBuilder();
            //模块化注入
            containerBuilder.RegisterModule<ControllerInjectionAutofacModule>();
            containerBuilder.RegisterModule<ServiceInjectionAutofacModule>();
            containerBuilder.RegisterModule<DependencyInjectionAutofacModule>();

            containerBuilder.Populate(services);

            var container = containerBuilder.Build();

            return new AutofacServiceProvider(container);
        }

        public virtual void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            #region AutoMapper 引入

            //AutoMapperConfig.RegisterMappings();

            #endregion

            #region 添加JWT认证

            app.UseAuthentication();

            #endregion

        }

    }
}
