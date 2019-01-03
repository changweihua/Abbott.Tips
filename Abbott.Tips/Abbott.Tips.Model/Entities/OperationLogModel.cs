using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Abbott.Tips.Model.Entities
{
    [Table("T_OperationLog")]
    public class OperationLogModel : TipsEntity
    {
        public int OperationType { get; set; }

        public string OpertationTable { get; set; }

        public string OperationName { get; set; }

        public string OperationField { get; set; }

        public string UserADName { get; set; }

        public string UserName { get; set; }

        #region 导航属性

        #endregion

    }
}
