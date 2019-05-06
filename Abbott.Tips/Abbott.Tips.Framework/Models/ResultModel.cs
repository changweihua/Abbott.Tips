using System;
using System.Collections.Generic;
using System.Text;

namespace Abbott.Tips.Framework.Models
{
    public interface IResultModel
    {
    }

    /// <summary>
    /// 标准API返回结果模型
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public class ApiResultModel
    {
        public int Code { get; set; }

        public object Result { get; set; }
    }

    public enum ResultCode
    {
        SUCCESS,
        FAIL,
        ERROR,
        IDLE
    }
}
