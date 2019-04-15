using Abbott.Tips.EntityFrameworkCore.Mappers;
using Abbott.Tips.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abbott.Tips.Application.Configurations.Dtos
{
    public class ConfigurationMapperProfile : AutoMapper.Profile, IMapperProfile
    {
        public ConfigurationMapperProfile()
        {
            CreateMap<ConfigurationModel, ConfigurationListModel>()
                .ForMember(dest => dest.CreatedUser, options => options.Ignore())
                .ForMember(dest => dest.UpdatedUser, options => options.Ignore())
                .ForMember(dest => dest.CreatedDate, options => options.Ignore())
                .ForMember(dest => dest.UpdatedDate, options => options.Ignore())
                .ForMember(dest => dest.ConfigurationName, options => options.Ignore())
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
