using Microsoft.AspNetCore.Authorization.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abbott.Tips.AspnetCore.Authority
{
    /// <summary>
    /// 数据操作权限
    /// 查：有权限即可
    /// 改：只能由Admin或者创建人进行修改
    /// 曾：有权限即可
    /// 删：只能由Admin或者创建人进行修改
    /// </summary>
    public static class Operations
    {
        public static OperationAuthorizationRequirement Create =
            new OperationAuthorizationRequirement { Name = "Create" };
        public static OperationAuthorizationRequirement Read =
            new OperationAuthorizationRequirement { Name = "Read" };
        public static OperationAuthorizationRequirement Update =
            new OperationAuthorizationRequirement { Name = "Update" };
        public static OperationAuthorizationRequirement Delete =
            new OperationAuthorizationRequirement { Name = "Delete" };
    }

    /// <summary>
    /// 系统所有内置权限，对应数据库Menu表的MenuPermission字段
    /// 通常会对权限项进行分组，构成一个树形结构，这样在展示和配置权限时，都会方便很多。
    /// 在这里，使用.来表示层级进行分组，其中User权限项包含所有以User.开头的权限。
    /// 
    /// HGDX 采用如下四级组织形式
    /// 
    /// ApplicationName + AreaName + ControllerName + ActionName
    /// 
    /// HGDX
    /// HGDX.Dashboard
    /// HGDX.Dashboard.Role
    /// HGDX.Dashboard.Role.Read
    /// 
    /// HGDX.Portal
    /// HGDX.Dashboard.Home
    /// HGDX.Dashboard.Home.Read
    /// 
    /// </summary>
    public static class Permissions
    {
        #region Account

        public const string Account = "HGDX.Dashboard.Account";
        public const string AccountLogin = "HGDX.Dashboard.Account.Login";
        public const string AccountLock = "HGDX.Dashboard.Account.Lock";
        public const string AccountLogout = "HGDX.Dashboard.Account.Logout";
        public const string AccountProfile = "HGDX.Dashboard.Account.Profile";

        #endregion

        #region User

        public const string User = "HGDX.Dashboard.User";
        public const string UserCreate = "HGDX.Dashboard.User.Create";
        public const string UserRead = "HGDX.Dashboard.User.Read";
        public const string UserUpdate = "HGDX.Dashboard.User.Update";
        public const string UserDelete = "HGDX.Dashboard.User.Delete";

        #endregion

        #region Role

        public const string Role = "HGDX.Dashboard.Role";
        public const string RoleCreate = "HGDX.Dashboard.Role.Create";
        public const string RoleRead = "HGDX.Dashboard.Role.Read";
        public const string RoleUpdate = "HGDX.Dashboard.Role.Update";
        public const string RoleDelete = "HGDX.Dashboard.Role.Delete";

        #endregion

        #region Menu

        public const string Menu = "HGDX.Dashboard.Menu";
        public const string MenuCreate = "HGDX.Dashboard.Menu.Create";
        public const string MenuRead = "HGDX.Dashboard.Menu.Read";
        public const string MenuUpdate = "HGDX.Dashboard.Menu.Update";
        public const string MenuDelete = "HGDX.Dashboard.Menu.Delete";

        #endregion

        #region Group

        public const string Group = "HGDX.Dashboard.Group";
        public const string GroupCreate = "HGDX.Dashboard.Group.Create";
        public const string GroupRead = "HGDX.Dashboard.Group.Read";
        public const string GroupUpdate = "HGDX.Dashboard.Group.Update";
        public const string GroupDelete = "HGDX.Dashboard.Group.Delete";

        #endregion

        #region Organization

        public const string Organization = "HGDX.Dashboard.Organization";
        public const string OrganizationCreate = "HGDX.Dashboard.Organization.Create";
        public const string OrganizationRead = "HGDX.Dashboard.Organization.Read";
        public const string OrganizationUpdate = "HGDX.Dashboard.Organization.Update";
        public const string OrganizationDelete = "HGDX.Dashboard.Organization.Delete";

        #endregion

        #region Region

        public const string Region = "HGDX.Dashboard.Region";
        public const string RegionCreate = "HGDX.Dashboard.Region.Create";
        public const string RegionRead = "HGDX.Dashboard.Region.Read";
        public const string RegionUpdate = "HGDX.Dashboard.Region.Update";
        public const string RegionDelete = "HGDX.Dashboard.Region.Delete";

        #endregion

    }

    public class PermissionDefinationAttribute : Attribute
    {
        /// <summary>
        /// 权限组，后端权限，前端权限
        /// </summary>
        public string PermissionGroup { get; set; }

        /// <summary>
        /// 权限名称
        /// </summary>
        public string PermissionName { get; set; }

    }

}
