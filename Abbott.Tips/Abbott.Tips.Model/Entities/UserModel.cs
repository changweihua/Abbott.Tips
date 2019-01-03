using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Abbott.Tips.Model.Entities
{
    [Table("T_User")]
    public class UserModel : IdentityKeyEntity<int>
    {
        public string LoginName { get; set; }

        public string LoginPwd { get; set; }

        public string UserName { get; set; }

        #region 导航属性

        public IEnumerable<UserRoleModel> UserRoles { get; set; }

        public IEnumerable<UserGroupModel> UserGroups { get; set; }

        public UserModel CreatedUser { get; set; }

        public UserModel UpdatedUser { get; set; }

        #endregion


    }
}
