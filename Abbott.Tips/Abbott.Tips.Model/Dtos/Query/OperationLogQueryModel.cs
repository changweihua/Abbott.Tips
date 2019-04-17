using System;
using System.Collections.Generic;
using System.Text;

namespace Abbott.Tips.Model.Query
{
    /// <summary>
    /// 操作日志列表查询模型
    /// </summary>
    public class OperationLogListQueryModel : PagerParameterQueryModel
    {
        public string UserName { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
