using Abbott.Tips.Framework.Audition;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Abbott.Tips.Model.Entities
{
    [Table("T_Menu")]
    public class MenuModel : TipsEntity
    {
        public string MenuName { get; set; }

        public string MenuPermission { get; set; }

        public string MenuLink { get; set; }

        public string MenuController { get; set; }

        public string MenuAction { get; set; }

        #region 导航属性

        public int? ParentId { get; set; }

        public virtual MenuModel ParentMenu { get; set; }

        public virtual ICollection<MenuModel> SubMenus { get; set; }

        public virtual ICollection<RoleMenuModel> RoleMenus { get; set; }

        #endregion
    }
}
