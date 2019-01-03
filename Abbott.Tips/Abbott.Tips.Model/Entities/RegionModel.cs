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
    public class RegionModel : IdentityKeyEntity<int>
    {
        public string RegionName { get; set; }

        public int RegionStatus { get; set; }

        public int RegionLevel { get; set; }

        #region 导航属性

        public int ParentID { get; set; }

        public RegionModel ParentRegion { get; set; }

        public IEnumerable<RegionModel> SubRegions { get; set; }

        public UserModel CreatedUser { get; set; }

        public UserModel UpdatedUser { get; set; }

        #endregion
    }
}
