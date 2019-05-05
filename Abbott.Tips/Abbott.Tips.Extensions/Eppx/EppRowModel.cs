using System;
using System.Collections.Generic;
using System.Text;

namespace Abbott.Tips.Extensions.Eppx
{
    /// <summary>
    /// Epplus导入行模型
    /// </summary>
    public class EppRowModel
    {
        public int RowIndex { get; set; }
        public bool IsValid
        {
            get
            {
                return CellErrors != null && CellErrors.Count > 0;
            }
        }
        public IList<EppCellErrorModel> CellErrors { get; set; }
    }
}
