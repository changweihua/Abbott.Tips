using Abbott.Tips.Core.Mappers;
using Abbott.Tips.EntityFrameworkCore.Mappers;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Abbott.Tips.ApiCore.Mappers
{
    public static class AutoMapperConfig
    {
        public static IConfigurationProvider GetConfigurationProvider()
        {
            var profiles = Directory.GetFiles(AppContext.BaseDirectory, "*.dll").Select(Assembly.LoadFrom)
              .SelectMany(y => y.DefinedTypes)
              .Where(type => typeof(Profile).GetTypeInfo().IsAssignableFrom(type.AsType())).ToList();

            IConfigurationProvider config = new MapperConfiguration(cfg =>
            {
                profiles.ForEach(profile =>
                {
                    cfg.AddProfile(profile.AsType());
                });
            });

            return config;
        }

        public static void RegisterMappings()
        {
            //获取所有IProfile实现类
            var allType =
            Assembly
               .GetEntryAssembly()//获取默认程序集
               .GetReferencedAssemblies()//获取所有引用程序集
               .Select(Assembly.Load)
               .SelectMany(y => y.DefinedTypes)
               .Where(type => typeof(IMapperProfile).GetTypeInfo().IsAssignableFrom(type.AsType()));

            var allTypes = Directory.GetFiles(AppContext.BaseDirectory, "*.dll").Select(Assembly.LoadFrom)
               .SelectMany(y => y.DefinedTypes)
               .Where(type => typeof(Profile).GetTypeInfo().IsAssignableFrom(type.AsType()));


            foreach (var typeInfo in allTypes)
            {
                var type = typeInfo.AsType();
                //if (type is IMapperProfile)
                if (type.Equals(typeof(Profile)))
                {
                    //注册映射
                    Mapper.Initialize(y =>
                    {
                        y.AddProfiles(type); // Initialise each Profile classes
                    });
                }
            }
        }
    }
}
