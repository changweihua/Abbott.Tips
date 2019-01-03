using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Abbott.Tips.Model.Entities
{
    [Table("T_RoleMenu")]
    public class RoleMenuModel : IdentityKeyEntity<int>
    {
        #region 导航属性

        public int MenuID { get; set; }

        public MenuModel Menu { get; set; }

        public int RoleID { get; set; }

        public RoleModel Role { get; set; }

        public UserModel CreatedUser { get; set; }

        public UserModel UpdatedUser { get; set; }

        #endregion
    }
}
