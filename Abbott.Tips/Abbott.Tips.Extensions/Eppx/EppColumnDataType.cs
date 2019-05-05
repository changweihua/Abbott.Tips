using System;
using System.Collections.Generic;
using System.Text;

namespace Abbott.Tips.Extensions.Eppx
{
    /// <summary>
    /// Epplus 列数据格式
    /// </summary>
    public enum EppColumnDataType
    {
        /// <summary>
        /// 文本
        /// </summary>
        STRING,
        /// <summary>
        /// 数字，浮点数
        /// </summary>
        NUMBER,
        /// <summary>
        /// 整数
        /// </summary>
        INTEGER,
        /// <summary>
        /// 日期
        /// </summary>
        DATE,
        /// <summary>
        /// 日期，含小时
        /// </summary>
        DATETIME,
        /// <summary>
        /// 百分比
        /// </summary>
        PERCENTAGE
    }
}
