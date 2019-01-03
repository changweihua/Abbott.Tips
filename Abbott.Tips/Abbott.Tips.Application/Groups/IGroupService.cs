using Abbott.Tips.EntityFrameworkCore.UnitOfWork.PagedList;
using Abbott.Tips.Framework.Dependency;
using Abbott.Tips.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abbott.Tips.Application.Groups
{
    /// <summary>
    /// 用户组服务接口类
    /// </summary>
    public interface IGroupService : IDependency
    {
        /// <summary>
        /// 获取系统全部用户组
        /// </summary>
        /// <returns></returns>
        IList<GroupModel> GetGroups();

        /// <summary>
        /// 获取系统全部角色
        /// </summary>
        /// <returns></returns>
        GroupModel GetSingleGroup(int id);

        /// <summary>
        /// 分页获取系统全部角色
        /// </summary>
        /// <returns></returns>
        IPagedList<GroupModel> GetPagedGroups(int pageIndex, int pageSize, int pageStart, int pageEnd, string groupName = "");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        int CreateGroup(GroupModel group);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        int EditGroup(GroupModel group);

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        int DeleteGroup(int groupId);

        int AssignRole(int groupId, List<int> roleIds);
    }
}
