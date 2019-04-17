using Abbott.Tips.Framework.Dependency;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abbott.Tips.Core.Mapper
{
    public class ObjectMapper : IObjectMapper
    {
        public AutoMapper.IMapper Mapper { get; set; }

        public TDestination Map<TDestination>(object source)
        {
            return Mapper.Map<TDestination>(source);
        }

        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            return Mapper.Map(source, destination);
        }
    }

    public interface IObjectMapper : ISingletonDependency
    {
        TDestination Map<TDestination>(object source);

        TDestination Map<TSource, TDestination>(TSource source, TDestination destination);
    }
}
