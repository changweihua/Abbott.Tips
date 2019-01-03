using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Abbott.Tips.Model.Entities
{
    /// <summary>
    /// 区域模型
    /// </summary>
    [Table("T_Region")]
    public class RegionModel : TipsEntity
    {
        public string RegionName { get; set; }

        public int RegionStatus { get; set; }

        public int RegionLevel { get; set; }

        #region 导航属性

        public int? ParentId { get; set; }

        public virtual RegionModel ParentRegion { get; set; }

        public virtual ICollection<RegionModel> SubRegions { get; set; }

        #endregion
    }
}
