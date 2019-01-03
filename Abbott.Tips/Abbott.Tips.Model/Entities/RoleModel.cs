using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Abbott.Tips.Model.Entities
{
    [Table("T_Role")]
    public class RoleModel : IdentityKeyEntity<int>
    {
        public string RoleName { get; set; }

        public string RoleDescription { get; set; }

        public bool IsInherited { get; set; }

        //[ForeignKey("ParentID")]
        //public virtual RoleModel ParentRole { get; set; }

        #region 导航属性

        public int? ParentID { get; set; }

        public RoleModel ParentRole { get; set; }

        public IEnumerable<RoleModel> SubRoles { get; set; }

        [NotMapped]
        public int UserID { get; set; }

        public IEnumerable<UserRoleModel> UserRoles { get; set; }

        [NotMapped]
        public int GroupID { get; set; }

        public IEnumerable<GroupRoleModel> RoleGroups { get; set; }

        [NotMapped]
        public int MenuID { get; set; }

        public IEnumerable<RoleMenuModel> RoleMenus { get; set; }

        public UserModel CreatedUser { get; set; }

        public UserModel UpdatedUser { get; set; }

        #endregion

    }
}
