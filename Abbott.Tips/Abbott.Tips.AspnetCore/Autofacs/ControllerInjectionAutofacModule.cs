using Autofac;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Abbott.Tips.AspnetCore.Autofacs
{
    /// <summary>
    /// Autofac 注入
    /// </summary>
    public class ControllerInjectionAutofacModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            //builder.RegisterType<IUnitOfWork>().AsImplementedInterfaces().PropertiesAutowired();

            //获取全部的Controller
            Assembly[] assemblies = Directory.GetFiles(AppContext.BaseDirectory, "*.dll").Select(Assembly.LoadFrom).ToArray();

            var manager = new ApplicationPartManager();
            assemblies.ToList().ForEach(assembly =>
            {
                manager.ApplicationParts.Add(new AssemblyPart(assembly));
            });
            manager.FeatureProviders.Add(new ControllerFeatureProvider());
            var feature = new ControllerFeature();
            manager.PopulateFeature(feature);
            builder.RegisterTypes(feature.Controllers.Select(ti => ti.AsType()).ToArray()).PropertiesAutowired();
        }
    }
}
