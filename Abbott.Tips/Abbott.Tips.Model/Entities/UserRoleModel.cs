using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Abbott.Tips.Model.Entities
{
    [Table("T_UserRole")]
    public class UserRoleModel : IdentityKeyEntity<int>
    {
        #region 导航属性

        public int UserID { get; set; }

        public UserModel User { get; set; }

        public int RoleID { get; set; }

        public RoleModel Role { get; set; }

        public UserModel CreatedUser { get; set; }

        public UserModel UpdatedUser { get; set; }

        #endregion
    }
}
