using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Abbott.Tips.Model.Entities
{
    [Table("T_Group")]
    public class GroupModel : IdentityKeyEntity<int>
    {
        public string GroupName { get; set; }

        public string GroupDescription { get; set; }

        public bool IsInherited { get; set; }

        #region 导航属性

        public int? ParentID { get; set; }

        public GroupModel ParentGroup { get; set; }

        public IEnumerable<GroupModel> SubGroups { get; set; }

        [NotMapped]
        public int UserID { get; set; }

        public IEnumerable<UserGroupModel> UserGroups { get; set; }

        [NotMapped]
        public int RoleID { get; set; }

        public IEnumerable<GroupRoleModel> GroupRoles { get; set; }

        public UserModel CreatedUser { get; set; }

        public UserModel UpdatedUser { get; set; }

        #endregion
    }
}
