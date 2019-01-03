using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Abbott.Tips.Model.Result
{
    public class OperationLogListModel
    {
        public int OperationType { get; set; }

        public string OperationTypeName { get; set; }

        public string OpertationTable { get; set; }

        public string OperationName { get; set; }

        public string OperationField { get; set; }

        public string UserADName { get; set; }

        public string UserName { get; set; }

        [NotMapped]
        public string CreatedUser { get; set; }

        [NotMapped]
        public DateTime CreatedDate { get; set; }

        [NotMapped]
        public string UpdatedUser { get; set; }

        [NotMapped]
        public DateTime UpdatedDate { get; set; }
    }
}
