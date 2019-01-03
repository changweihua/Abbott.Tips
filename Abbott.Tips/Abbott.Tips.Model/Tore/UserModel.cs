using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Abbott.Tips.Model.Entities
{
    [Table("T_User")]
    public class UserModel : TipsEntity
    {
        public string LoginName { get; set; }

        public string LoginPwd { get; set; }

        public string UserName { get; set; }

        #region 导航属性

        public virtual ICollection<UserRoleModel> UserRoles { get; set; }

        public virtual ICollection<UserGroupModel> UserGroups { get; set; }

        #endregion


    }
}
