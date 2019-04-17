using Abbott.Tips.EntityFrameworkCore.Mappers;
using Abbott.Tips.Model.Entities;
using AutoMapper;
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
                .ForMember(dest => dest.CreatedUser, options => options.MapFrom(src => src.Creator))
                .ForMember(dest => dest.UpdatedUser, options => options.MapFrom(src => src.Creator))
                .ForMember(dest => dest.CreatedDate, options => options.MapFrom(src => src.CreatedTime))
                .ForMember(dest => dest.UpdatedDate, options => options.MapFrom(src => src.UpdatedTime))
                .ForMember(dest => dest.ConfigurationDescription, options => options.MapFrom(src => src.ConfigDescription))
                .ForMember(dest => dest.ConfigurationType, options => options.MapFrom(src => src.ConfigType))
                .ForMember(dest => dest.ConfigurationName, options => options.MapFrom(src => src.ConfigName))
                .ForMember(dest => dest.ConfigurationValue, options => options.MapFrom(src => src.ConfigValue));

            CreateMap<ConfigurationCreationModel, ConfigurationModel>()
                 .ForMember(dest => dest.Id, options => options.Ignore())
                .ForMember(dest => dest.CreatedBy, options => options.Ignore())
                .ForMember(dest => dest.CreatedTime, options => options.Ignore())
                .ForMember(dest => dest.UpdatedBy, options => options.Ignore())
                .ForMember(dest => dest.UpdatedTime, options => options.Ignore())
                .ForMember(dest => dest.IsDeleted, options => options.Ignore())
                .ForMember(dest => dest.ConfigDescription, options => options.MapFrom(src => src.ConfigurationDescription))
                .ForMember(dest => dest.ConfigName, options => options.MapFrom(src => src.ConfigurationName))
                .ForMember(dest => dest.ConfigType, options => options.MapFrom(src => src.ConfigurationType))
                .ForMember(dest => dest.ConfigValue, options => options.MapFrom(src => src.ConfigurationValue));
        }
    }

    public static class ConfigurationMapperExt
    {
        public static IMapper Mapper { get; set; }

        #region DB_Model to DTO_Model

        public static ConfigurationListModel ToConfigurationListModel(this ConfigurationModel entity)
        {
            return AutoMapper.Mapper.Map<ConfigurationModel, ConfigurationListModel>(entity);
        }

        #endregion
    }
}
