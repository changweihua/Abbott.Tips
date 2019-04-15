using System;
using System.Collections.Generic;
using System.Text;

namespace Abbott.Tips.Core.Mapper
{
    public sealed class ObjectMapper
    {
        public static TDestination Map<TDestination>(object source)
        {
            return AutoMapper.Mapper.Map<TDestination>(source);
        }

        public static TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            return AutoMapper.Mapper.Map<TSource, TDestination>(source);
        }
    }
}
