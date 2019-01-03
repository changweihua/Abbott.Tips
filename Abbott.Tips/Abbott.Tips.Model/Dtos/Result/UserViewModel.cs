using System;
using System.Collections.Generic;
using System.Text;

namespace Abbott.Tips.Model.Result
{

    public class UserListModel
    {
        public string UserName { get; set; }

        public string LoginName { get; set; }

        public int UserId { get; set; }

        public string Groups { get; set; }

        public string CreatedUser { get; set; }

        public DateTime CreatedDate { get; set; }

        public string UpdatedUser { get; set; }

        public DateTime UpdatedDate { get; set; }
    }

    /// <summary>
    /// 用户身份信息，包含角色及菜单
    /// </summary>
    public class UserIdentityModel
    {
        public int UserID { get; set; }
        public string ADName { get; set; }
        public string UserName { get; set; }

        public IList<UserRolePlainInfoModel> UserRoles { get; set; }
    }

    public class UserRolePlainInfoModel
    {
        public int UserID { get; set; }
        public int RoleID { get; set; }
        public string RoleName { get; set; }

        public IList<UserRoleMenuPlainInfoModel> UserRoleMenus { get; set; }
    }

    public class UserRoleMenuPlainInfoModel
    {
        public int RoleID { get; set; }
        public int MenuID { get; set; }
        public string MenuName { get; set; }
        public string MenuLink { get; set; }
        public string MenuController { get; set; }
        public string MenuAction { get; set; }
    }

    #region 用户认证相关

    /// <summary>
    /// 用户所有的权限，用于权限认证
    /// </summary>
    public class UserPermissionModel
    {
        public string MenuPermission { get; set; }
    }

    /// <summary>
    /// 用户所有的菜单及权限，用于左侧菜单显示
    /// </summary>
    public class UserMenuPermissionModel
    {
        public int MenuID { get; set; }
        public int MenuOrder { get; set; }
        public int ParentID { get; set; }
        public string MenuName { get; set; }
        public string MenuLink { get; set; }
        public string MenuController { get; set; }
        public string MenuAction { get; set; }
        public string MenuPermission { get; set; }
        public string MenuIcon { get; set; }
    }

    #endregion

}
