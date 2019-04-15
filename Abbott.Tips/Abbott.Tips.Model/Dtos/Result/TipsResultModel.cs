using Abbott.Tips.Framework.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abbott.Tips.Model.Dtos.Result
{
    public class JsonContractResultModel
    {
        public SerializationFilterAttribute SerializationFilter { get; set; }

        public int Code { get; set; }
    }

    public class ListJsonContractResultModel<TModel> : JsonContractResultModel
    {
        public IList<TModel> Items { get; set; }
    }
}
