using Abbott.Tips.Framework.Dependency;
using Autofac;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Abbott.Tips.AspnetCore.Autofacs
{
    public class ServiceInjectionAutofacModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //注册所有实现了 IDependency 接口的类型
            Assembly[] assemblies = Directory.GetFiles(AppContext.BaseDirectory, "*.dll").Select(Assembly.LoadFrom).ToArray();

            Type baseType = typeof(IDependency);
            builder.RegisterAssemblyTypes(assemblies)
                   .Where(type => baseType.IsAssignableFrom(type) && !type.IsAbstract && type.Name.EndsWith("Service"))
                   .AsSelf().AsImplementedInterfaces()
                   .PropertiesAutowired();

            Type isBaseType = typeof(ISingletonDependency);
            builder.RegisterAssemblyTypes(assemblies)
                   .Where(type => isBaseType.IsAssignableFrom(type) && !type.IsAbstract)
                   .AsSelf().AsImplementedInterfaces()
                   .PropertiesAutowired().SingleInstance();

            //Type interceptorType = typeof(IInterceptor);
            //builder.RegisterAssemblyTypes(assemblies)
            //       .Where(type => interceptorType.IsAssignableFrom(type) && !type.IsAbstract && type.Name.EndsWith("Interceptor"))
            //       .AsSelf().AsImplementedInterfaces()
            //       .PropertiesAutowired();//.EnableClassInterceptors();//.InterceptedBy(typeof(AOPTestInterceptor));

        }
    }
}
