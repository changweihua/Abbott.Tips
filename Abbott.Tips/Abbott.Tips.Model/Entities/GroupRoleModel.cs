using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Abbott.Tips.Model.Entities
{

    [Table("T_GroupRole")]
    public class GroupRoleModel : IdentityKeyEntity<int>
    {
        #region 导航属性

        public int RoleID { get; set; }

        public RoleModel Role { get; set; }

        public int GroupID { get; set; }

        public GroupModel Group { get; set; }

        public UserModel CreatedUser { get; set; }

        public UserModel UpdatedUser { get; set; }

        #endregion
    }
}
