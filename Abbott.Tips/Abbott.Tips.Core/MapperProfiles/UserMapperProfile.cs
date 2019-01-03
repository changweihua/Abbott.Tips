using Abbott.Tips.EntityFrameworkCore.Mappers;
using Abbott.Tips.Model.Entities;
using Abbott.Tips.Model.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abbott.Tips.Core.Mappers
{
    public class UserMapperProfile : AutoMapper.Profile, IMapperProfile
    {
        public UserMapperProfile()
        {
            CreateMap<UserModel, UserListModel>()
                .ForMember(dest => dest.CreatedUser, options => options.MapFrom(src => src.CreatedUser.UserName))
                .ForMember(dest => dest.UpdatedUser, options => options.MapFrom(src => src.UpdatedUser == null ? string.Empty : src.UpdatedUser.UserName))
                .ForMember(dest => dest.UserId, options => options.MapFrom(src => src.Id))
                .ForMember(dest => dest.CreatedDate, options => options.MapFrom(src => src.CreatedTime))
                .ForMember(dest => dest.UpdatedDate, options => options.MapFrom(src => src.UpdatedTime))
                .ForMember(dest => dest.Groups, options => options.MapFrom(src => "所属用户组"))
                .ForMember(dest => dest.UserName, options => options.MapFrom(src => src.UserName))
                .ForMember(dest => dest.LoginName, options => options.MapFrom(src => src.LoginName));
        }
    }

    public static class UserMapperExt
    {
        #region DB_Model to DTO_Model

        public static UserListModel ToUserListModel(this UserModel entity)
        {
            return AutoMapper.Mapper.Map<UserModel, UserListModel>(entity);
        }

        #endregion
    }
}
