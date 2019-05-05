using System;
using System.Collections.Generic;
using System.Text;

namespace Abbott.Tips.Extensions.Eppx
{
    /// <summary>
    /// Epplus 列定义
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class EppColumnAttribute : Attribute
    {
        public int columnIndex { get; set; }
        public string columName { get; set; }

        public string ColumnDescription { get; set; }
        public string ColumnCellDataValidationSource { get; set; }
        public string ColumnNumberFormat { get; set; }
        /// <summary>
        /// 单元格列对应数据类型
        /// </summary>
        public EppColumnDataType ColumnDataType { get; set; }

        public EppColumnAttribute(int index, string name)
        {
            columnIndex = index;
            columName = name;
        }

        public int ColumnIndex
        {
            get
            {
                return columnIndex;
            }
        }
        public string ColumName
        {
            get
            {
                return columName;
            }
        }
    }
}
