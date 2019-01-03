using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Abbott.Tips.Model.Entities
{
    [Table("T_OperationLog")]
    public class OperationLogModel : IdentityKeyEntity<int>
    {
        public int OperationType { get; set; }

        public string OpertationTable { get; set; }

        public string OperationName { get; set; }

        public string OperationField { get; set; }

        public string UserADName { get; set; }

        public string UserName { get; set; }

        #region 导航属性

        public UserModel CreatedUser { get; set; }

        public UserModel UpdatedUser { get; set; }

        #endregion

    }
}
