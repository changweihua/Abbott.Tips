using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Abbott.Tips.Model.Entities
{

    [Table("T_UserGroup")]
    public class UserGroupModel : TipsEntity
    {
        #region 导航属性

        public int UserId { get; set; }

        public virtual UserModel User { get; set; }

        public int GroupId { get; set; }

        public virtual GroupModel Group { get; set; }

        #endregion
    }
}
