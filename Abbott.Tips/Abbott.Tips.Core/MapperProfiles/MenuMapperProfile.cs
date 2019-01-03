using Abbott.Tips.EntityFrameworkCore.Mappers;
using Abbott.Tips.Model.Entities;
using Abbott.Tips.Model.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abbott.Tips.Core.Mappers
{
    public class MenuMapperProfile : AutoMapper.Profile, IMapperProfile
    {
        public MenuMapperProfile()
        {
            CreateMap<MenuModel, MenuListModel>()
                .ForMember(dest => dest.CreatedUser, options => options.MapFrom(src => src.CreatedUser.UserName))
                .ForMember(dest => dest.UpdatedUser, options => options.MapFrom(src => src.UpdatedUser == null ? string.Empty : src.UpdatedUser.UserName))
                .ForMember(dest => dest.MenuAction, options => options.MapFrom(src => src.MenuAction))
                .ForMember(dest => dest.MenuController, options => options.MapFrom(src => src.MenuController))
                .ForMember(dest => dest.MenuPermission, options => options.MapFrom(src => src.MenuPermission))
                .ForMember(dest => dest.MenuLink, options => options.MapFrom(src => src.MenuLink))
                .ForMember(dest => dest.MenuId, options => options.MapFrom(src => src.Id))
                .ForMember(dest => dest.CreatedDate, options => options.MapFrom(src => src.CreatedTime))
                .ForMember(dest => dest.UpdatedDate, options => options.MapFrom(src => src.UpdatedTime))
                .ForMember(dest => dest.MenuName, options => options.MapFrom(src => src.MenuName));
        }
    }

    public static class MenuMapperExt
    {
        #region DB_Model to DTO_Model

        public static MenuListModel ToMenuListModel(this MenuModel entity)
        {
            return AutoMapper.Mapper.Map<MenuModel, MenuListModel>(entity);
        }

        #endregion
    }
}
