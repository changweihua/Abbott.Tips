using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Abbott.Tips.Model.Entities
{
    [Table("T_UserRole")]
    public class UserRoleModel : TipsEntity
    {
        #region 导航属性

        public int UserId { get; set; }

        public virtual UserModel User { get; set; }

        public int RoleId { get; set; }

        public virtual RoleModel Role { get; set; }

        #endregion
    }
}
