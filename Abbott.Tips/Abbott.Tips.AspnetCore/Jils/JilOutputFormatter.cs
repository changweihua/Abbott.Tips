using Jil;
using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Abbott.Tips.AspnetCore.Jils
{
    /// <summary>
    /// 输出 JSON 解析类
    /// </summary>
    public class JilOutputFormatter : JilFormatter, IOutputFormatter
    {
        private readonly Options _options;

        public JilOutputFormatter()
            : this(null)
        { }

        public JilOutputFormatter(Options options)
        {
            _options = options ?? Opts;
        }

        public bool CanWriteResult(OutputFormatterCanWriteContext context)
        {
            return true;
        }

        public Task WriteAsync(OutputFormatterWriteContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var response = context.HttpContext.Response;
            response.ContentType = CONTENT_TYPE;

            if (context.Object == null)
            {
                // 忘了在哪里看的了，192 好像在 Response.Body 中表示 null
                response.Body.WriteByte(192);
                return Task.CompletedTask;
            }

            using (var writer = context.WriterFactory(response.Body, Encoding.UTF8))
            {
                // 使用 Jil 序列化
                JSON.Serialize(context.Object, writer, _options);
                return Task.CompletedTask;
            }
        }
    }

}
