using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Abbott.Tips.Model.Entities
{
    [Table("T_Menu")]
    public class MenuModel : IdentityKeyEntity<int>
    {
        public string MenuName { get; set; }

        public string MenuPermission { get; set; }

        public string MenuLink { get; set; }

        public string MenuController { get; set; }

        public string MenuAction { get; set; }

        #region 导航属性

        public int? ParentID { get; set; }

        public MenuModel ParentMenu { get; set; }

        public IEnumerable<MenuModel> SubMenus { get; set; }

        public IEnumerable<RoleMenuModel> RoleMenus { get; set; }

        public UserModel CreatedUser { get; set; }

        public UserModel UpdatedUser { get; set; }

        #endregion
    }
}
