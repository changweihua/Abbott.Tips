using Abbott.Tips.EntityFrameworkCore.UnitOfWork.PagedList;
using Abbott.Tips.Framework.Dependency;
using Abbott.Tips.Model.Entities;
using Abbott.Tips.Model.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abbott.Tips.Application.Roles
{
    /// <summary>
    /// 角色服务接口类
    /// </summary>
    public interface IRoleService : IDependency
    {
        /// <summary>
        /// 获取系统全部角色
        /// </summary>
        /// <returns></returns>
        IList<RoleModel> GetRoles();

        /// <summary>
        /// 获取系统全部角色
        /// </summary>
        /// <returns></returns>
        RoleModel GetSingleRole(int id);

        /// <summary>
        /// 分页获取系统全部角色
        /// </summary>
        /// <returns></returns>
        IPagedList<RoleModel> GetPagedRoles(int pageIndex, int pageSize, int pageStart, int pageEnd, string roleName = "");

        /// <summary>
        /// 新增角色及菜单
        /// </summary>
        /// <param name="roleName"></param>
        /// <param name="menuIds"></param>
        /// <returns></returns>
        int CreateRole(string roleName, int parentId, bool isInherited, IList<int> menuIds);

        /// <summary>
        /// 编辑角色菜单
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="roleName"></param>
        /// <param name="menuIds"></param>
        /// <returns></returns>
        int EditRole(int roleId, string roleName, int parentId, bool isInherited, IList<int> menuIds);

        /// <summary>
        /// 查询当前角色对应菜单
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Tuple<string, IList<RoleMenuListModel>> GetRoleMenus(int roleId);

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        int DeleteRole(int roleId);
    }
}
