using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Abbott.Tips.Model.Entities
{
    /// <summary>
    /// 组织架构模型
    /// </summary>
    [Table("T_Organization")]
    public class OrganizationModel : IdentityKeyEntity<int>
    {
        public string OrganizationName { get; set; }

        public int OrganizationStatus { get; set; }

        public int OrganizationLevel { get; set; }

        #region 导航属性

        public int ParentID { get; set; }

        public OrganizationModel ParentOrganization { get; set; }

        public IEnumerable<OrganizationModel> SubOrganizations { get; set; }

        public UserModel CreatedUser { get; set; }

        public UserModel UpdatedUser { get; set; }

        #endregion
    }
}
