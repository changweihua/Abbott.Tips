using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Abbott.Tips.Model.Entities
{
    /// <summary>
    /// 发布分类管理表
    /// </summary>
    [Table("T_ShareCategory")]
    public class ShareCategoryModel : IdentityKeyEntity<int>
    {
        /// <summary>
        /// 分享类型
        /// </summary>
        public int ShareType { get; set; }

        /// <summary>
        /// 分类名称
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// 分类状态
        /// </summary>
        public int CategoryStatus { get; set; }

        #region 导航属性

        public UserModel CreatedUser { get; set; }

        public UserModel UpdatedUser { get; set; }

        #endregion
    }
}
