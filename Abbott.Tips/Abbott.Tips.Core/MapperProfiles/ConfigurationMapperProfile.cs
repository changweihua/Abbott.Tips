using Abbott.Tips.EntityFrameworkCore.Mappers;
using Abbott.Tips.Model.Entities;
using Abbott.Tips.Model.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abbott.Tips.Core.Mappers
{
    public class ConfigurationMapperProfile : AutoMapper.Profile, IMapperProfile
    {
        public ConfigurationMapperProfile()
        {
            CreateMap<ConfigurationModel, ConfigurationListModel>()
                .ForMember(dest => dest.CreatedUser, options => options.MapFrom(src => src.CreatedUser.UserName))
                .ForMember(dest => dest.UpdatedUser, options => options.MapFrom(src => src.UpdatedUser == null ? string.Empty : src.UpdatedUser.UserName))
                .ForMember(dest => dest.CreatedDate, options => options.MapFrom(src => src.CreatedTime))
                .ForMember(dest => dest.UpdatedDate, options => options.MapFrom(src => src.UpdatedTime))
                .ForMember(dest => dest.ConfigurationName, options => options.MapFrom(src => src.ConfigName));
        }
    }

    public static class ConfigurationMapperExt
    {
        #region DB_Model to DTO_Model

        public static ConfigurationListModel ToConfigurationListModel(this ConfigurationModel entity)
        {
            return AutoMapper.Mapper.Map<ConfigurationModel, ConfigurationListModel>(entity);
        }

        #endregion
    }
}
