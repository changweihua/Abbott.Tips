using Jil;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abbott.Tips.AspnetCore.Jils
{
    /// <summary>
    /// Jil Formatter 基类
    /// </summary>
    public abstract class JilFormatter
    {
        protected const string CONTENT_TYPE = "application/json";

        protected readonly Options Opts = new Options(excludeNulls: true, includeInherited: true,
                                                      dateFormat: DateTimeFormat.SecondsSinceUnixEpoch,
                                                      serializationNameFormat: SerializationNameFormat.CamelCase);
    }

}
