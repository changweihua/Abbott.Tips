using System;
using System.Collections.Generic;
using System.Text;

namespace Abbott.Tips.Model.Result
{
    /// <summary>
    /// 角色编辑时，菜单模型
    /// </summary>
    public class RoleMenuListModel
    {
        public int MenuID { get; set; }
        public int RoleID { get; set; }
        public int ParentID { get; set; }
        public int MenuOrder { get; set; }
        public string MenuName { get; set; }
        public string MenuLink { get; set; }
        public string MenuController { get; set; }
        public string MenuAction { get; set; }
        public string MenuPermission { get; set; }
        public int IsChecked { get; set; }
    }

    /// <summary>
    /// 菜单列表显示模型
    /// </summary>
    public class MenuListModel
    {
        public int MenuId { get; set; }
        public int MenuOrder { get; set; }
        public string MenuName { get; set; }
        public string MenuLink { get; set; }
        public string MenuController { get; set; }
        public string MenuAction { get; set; }
        public string MenuPermission { get; set; }


        public string CreatedUser { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedUser { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
