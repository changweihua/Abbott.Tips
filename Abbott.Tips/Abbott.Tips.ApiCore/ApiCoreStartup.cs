﻿using Abbott.Tips.ApiCore.Mappers;
using Abbott.Tips.AspnetCore;
using Abbott.Tips.AspnetCore.Autofacs;
using Abbott.Tips.AspnetCore.BackgroundServices;
using Abbott.Tips.AspnetCore.Jils;
using Abbott.Tips.AspnetCore.Middlewares;
using Abbott.Tips.EntityFrameworkCore;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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

            services.AddDbContext<TipsContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("SqlServer")))
                    .AddUnitOfWork<TipsContext>();

            // 替换控制器所有者
            // 使用Autofac进行注入
            services.Replace(ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator>());

            // 引入 AutoMapper 组件
            //services.AddAutoMapper();

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

            services.AddMemoryCache();

            services.AddMvc(options =>
            {
                //options.Filters.Add(new AddHeaderAttribute("GlobalAddHeader",
                //    "Result filter added to MvcOptions.Filters")); // an instance

                //options.ValueProviderFactoties.Add(new JQueryQueryStringValueProviderFactory());
                //options.Conventions.Add(new ParameterModelConventions.ArraryHandleQueryConvention());
                //options.ModelBinderProviders.Insert(0, new SplitDateTimeModelBinderProvider());

                // 使用 Jil 替换默认的 JSON 解析
                options.InputFormatters.Clear();
                options.InputFormatters.Add(new JilInputFormatter());
                options.OutputFormatters.Clear();
                options.OutputFormatters.Add(new JilOutputFormatter());

            }).AddJsonOptions(options =>
            {
                //不使用驼峰样式的key
                //options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                //忽略循环引用
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                //设置时间格式
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd";
            });

            //Autofac 注入
            var containerBuilder = new ContainerBuilder();
            //模块化注入
            containerBuilder.RegisterModule<ControllerInjectionAutofacModule>();
            containerBuilder.RegisterModule<ServiceInjectionAutofacModule>();

            containerBuilder.Populate(services);

            var container = containerBuilder.Build();

            return new AutofacServiceProvider(container);
        }

        public virtual void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            #region AutoMapper 引入

            AutoMapperConfig.RegisterMappings();

            #endregion
        }

    }
}
