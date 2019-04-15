using Abbott.Tips.Core.Mappers;
using Abbott.Tips.EntityFrameworkCore.Mappers;
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
               .Where(type => typeof(IMapperProfile).GetTypeInfo().IsAssignableFrom(type.AsType()));


            foreach (var typeInfo in allTypes)
            {
                var type = typeInfo.AsType();
                if (type is IMapperProfile)
                //if (type.Equals(typeof(IMapperProfile)))
                {
                    //注册映射
                    AutoMapper.Mapper.Initialize(y =>
                    {
                        y.AddProfiles(type); // Initialise each Profile classe
                    });
                }
            }
        }
    }
}
