using System;
using System.Collections.Generic;
using System.Text;

namespace Abbott.Tips.Extensions.Eppx
{
    /// <summary>
    /// Epplus导入单元格验证错误模型
    /// </summary>
    public class EppCellErrorModel
    {
        public int RowIndex { get; set; }
        public int ColumnIndex { get; set; }
        public string ErrorItem { get; set; }
        public string ErrorMessage { get; set; }
    }
}
