using Jil;
using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Abbott.Tips.AspnetCore.Jils
{
    /// <summary>
    /// 输入 JSON 解析类
    /// </summary>
    public class JilInputFormatter : JilFormatter, IInputFormatter
    {
        private readonly Options _options;

        public JilInputFormatter()
            : this(null)
        { }

        public JilInputFormatter(Options options)
        {
            _options = options ?? Opts;
        }

        public bool CanRead(InputFormatterContext context)
        {
            return true;
        }

        public Task<InputFormatterResult> ReadAsync(InputFormatterContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var request = context.HttpContext.Request;

            using (var reader = context.ReaderFactory(request.Body, Encoding.UTF8))
            {
                // 使用 Jil 反序列化
                var result = JSON.Deserialize(reader, context.ModelType, _options);
                return InputFormatterResult.SuccessAsync(result);
            }
        }
    }

}
