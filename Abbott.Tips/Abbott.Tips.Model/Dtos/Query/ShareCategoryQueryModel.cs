using System;
using System.Collections.Generic;
using System.Text;

namespace Abbott.Tips.Model.Query
{
    /// <summary>
    /// 发布分类创建模型
    /// </summary>
    public class ShareCategoryCreateModel
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
    }

    /// <summary>
    /// 发布分类更新模型
    /// </summary>
    public class ShareCategoryEditModel
    {

        public int CategoryId { get; set; }

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
    }

    /// <summary>
    /// 发布分类列表查询模型
    /// </summary>
    public class ShareCategoryListQueryModel : LayTablePagerParameterQueryModel
    {
        public string CategoryName { get; set; }

        public int ShareType { get; set; }
    }
}
