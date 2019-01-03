using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Abbott.Tips.Model.Entities
{
    [Table("T_Role")]
    public class RoleModel : TipsEntity
    {
        public string RoleName { get; set; }

        public string RoleDescription { get; set; }

        public bool IsInherited { get; set; }

        #region 导航属性

        public int? ParentId { get; set; }

        public virtual RoleModel ParentRole { get; set; }

        public virtual ICollection<RoleModel> SubRoles { get; set; }

        public virtual ICollection<UserRoleModel> UserRoles { get; set; }

        public virtual ICollection<RoleMenuModel> RoleMenus { get; set; }

        #endregion

    }
}
