using Abbott.Tips.EntityFrameworkCore.Mappers;
using Abbott.Tips.Model;
using Abbott.Tips.Model.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abbott.Tips.Core.Mapper
{
    public class ResultMapperProfile : AutoMapper.Profile, IMapperProfile
    {
        public ResultMapperProfile()
        {
            //CreateMap(typeof(IPager<>), typeof(JsonListResultModel<>))
            //    .ForMember(dest => dest.Items, options => options.MapFrom(src => src.Items));
        }
    }
}
