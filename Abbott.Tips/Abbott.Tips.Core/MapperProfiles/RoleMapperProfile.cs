using Abbott.Tips.EntityFrameworkCore.Mappers;
using Abbott.Tips.Model.Entities;
using Abbott.Tips.Model.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abbott.Tips.Core.Mappers
{
    public class RoleMapperProfile : AutoMapper.Profile, IMapperProfile
    {
        public RoleMapperProfile()
        {
            CreateMap<RoleModel, RoleListModel>()
                .ForMember(dest => dest.CreatedUser, options => options.MapFrom(src => src.CreatedUser.UserName))
                .ForMember(dest => dest.UpdatedUser, options => options.MapFrom(src => src.UpdatedUser == null ? string.Empty : src.UpdatedUser.UserName))
                .ForMember(dest => dest.ParentID, options => options.MapFrom(src => src.ParentID))
                .ForMember(dest => dest.RoleId, options => options.MapFrom(src => src.Id))
                .ForMember(dest => dest.ParentName, options => options.MapFrom(src => src.ParentRole == null ? "空角色" : src.ParentRole.RoleName))
                .ForMember(dest => dest.CreatedDate, options => options.MapFrom(src => src.CreatedTime))
                .ForMember(dest => dest.UpdatedDate, options => options.MapFrom(src => src.UpdatedTime))
                .ForMember(dest => dest.RoleName, options => options.MapFrom(src => src.RoleName))
                .ForMember(dest => dest.RoleDescription, options => options.MapFrom(src => src.RoleDescription));
        }
    }

    public static class RoleMapperExt
    {
        #region DB_Model to DTO_Model

        public static RoleListModel ToRoleListModel(this RoleModel entity)
        {
            return AutoMapper.Mapper.Map<RoleModel, RoleListModel>(entity);
        }

        #endregion
    }
}
