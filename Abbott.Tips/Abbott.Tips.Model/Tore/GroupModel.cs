using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Abbott.Tips.Model.Entities
{
    /// <summary>
    /// 用户组数据模型
    /// </summary>
    [Table("T_Group")]
    public class GroupModel : TipsEntity
    {
        public string GroupName { get; set; }

        public string GroupDescription { get; set; }

        public bool IsInherited { get; set; }

        #region 导航属性

        public int? ParentId { get; set; }

        public virtual GroupModel ParentGroup { get; set; }

        public virtual ICollection<GroupModel> SubGroups { get; set; }

        public virtual ICollection<UserGroupModel> UserGroups { get; set; }

        #endregion
    }
}
