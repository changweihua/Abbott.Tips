using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Abbott.Tips.Model
{
    /// <summary>
    /// 分页请求的参数：page、limit
    /// 适用LayUI.Table组件
    /// </summary>
    public class LayTablePagerParameterQueryModel
    {
        /// <summary>
        /// 页码,从1开始
        /// </summary>
        [Required(ErrorMessage = "页码不能为空")]
        [MinValue(1, ErrorMessage = "页码最小值为1")]
        public int Page { get; set; }

        /// <summary>
        /// 每页数据量
        /// </summary>
        [Required(ErrorMessage = "页大小不能为空")]
        [Range(10, 30, ErrorMessage = "每页大小范围：10-30")]
        public int Limit { get; set; }

        public int PageIndex
        {
            get
            {
                return Page - 1;
            }
        }

        public int PageSize
        {
            get
            {
                return Limit;
            }
        }

        /// <summary>
        /// EF Skip
        /// </summary>
        public int SkipIndex
        {
            get
            {
                return (PageIndex - 1) * PageSize;
            }
        }

        /// <summary>
        /// 分页结束
        /// </summary>
        public int PageEnd
        {
            get
            {
                return PageIndex * PageSize;
            }
        }

        /// <summary>
        /// 分页结束
        /// </summary>
        public int PageStart
        {
            get
            {
                return (PageIndex - 1) * PageSize + 1;
            }
        }

        /// <summary>
        /// 是否开启分页
        /// </summary>
        public int AllowPagination { get; set; }
    }

    #region 参数校验

    public class MaxValueAttribute : ValidationAttribute
    {
        private readonly int _maxValue;

        public MaxValueAttribute(int maxValue)
        {
            _maxValue = maxValue;
        }

        public override bool IsValid(object value)
        {
            return (int)value <= _maxValue;
        }
    }

    public class MinValueAttribute : ValidationAttribute
    {
        private readonly int _minValue;

        public MinValueAttribute(int minValue)
        {
            _minValue = minValue;
        }

        public override bool IsValid(object value)
        {
            return (int)value >= _minValue;
        }
    }

    #endregion

}
