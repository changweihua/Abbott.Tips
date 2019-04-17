using Abbott.Tips.Framework.Attributes;
using Abbott.Tips.Pager;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abbott.Tips.Model.Result
{
    public interface IResultModel
    {
    }

    /// <summary>
    /// 标准API返回结果模型
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public class ResultModel
    {
        public ResultCode Code { get; set; }

        public object Result { get; set; }
    }

    public enum ResultCode
    {
        SUCCESS,
        FAIL,
        ERROR,
        IDLE
    }

    /// <summary>
    /// 标准Json格式数据，包含状态码和属性过滤器
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public class JsonResultModel : ResultModel
    {
        public SerializationFilterAttribute SerializationFilter { get; set; }
    }

    /// <summary>
    /// 标准Json格式，返回表格数据
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public class JsonListResultModel<TResult> : JsonResultModel
    {
        public List<TResult> Items { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
    }

    /// <summary>
    /// 标准Json格式，返回表格数据
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public class PagerResultModel<TEntity> : JsonResultModel
    {
        public IPager<TEntity> Pager { get; set; }
    }
}
