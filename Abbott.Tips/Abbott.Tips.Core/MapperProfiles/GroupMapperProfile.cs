using Abbott.Tips.EntityFrameworkCore.Mappers;
using Abbott.Tips.Model.Entities;
using Abbott.Tips.Model.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abbott.Tips.Core.Mappers
{
    public class GroupMapperProfile : AutoMapper.Profile, IMapperProfile
    {
        public GroupMapperProfile()
        {
            CreateMap<GroupModel, GroupListModel>()
                .ForMember(dest => dest.CreatedUser, options => options.MapFrom(src => src.CreatedUser.UserName))
                .ForMember(dest => dest.UpdatedUser, options => options.MapFrom(src => src.UpdatedUser == null ? string.Empty : src.UpdatedUser.UserName))
                .ForMember(dest => dest.ParentID, options => options.MapFrom(src => src.ParentID))
                .ForMember(dest => dest.GroupId, options => options.MapFrom(src => src.Id))
                .ForMember(dest => dest.ParentName, options => options.MapFrom(src => src.ParentGroup == null ? "" : src.ParentGroup.GroupName))
                .ForMember(dest => dest.CreatedDate, options => options.MapFrom(src => src.CreatedTime))
                .ForMember(dest => dest.UpdatedDate, options => options.MapFrom(src => src.UpdatedTime))
                .ForMember(dest => dest.GroupName, options => options.MapFrom(src => src.GroupName))
                .ForMember(dest => dest.GroupDescription, options => options.MapFrom(src => src.GroupDescription));
        }
    }

    public static class GroupMapperExt
    {
        #region DB_Model to DTO_Model

        public static GroupListModel ToGroupListModel(this GroupModel entity)
        {
            return AutoMapper.Mapper.Map<GroupModel, GroupListModel>(entity);
        }

        #endregion
    }
}
