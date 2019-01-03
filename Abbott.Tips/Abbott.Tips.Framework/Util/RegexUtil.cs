using System;
using System.Collections.Generic;
using System.Text;

namespace Abbott.Tips.Framework.Util
{
    public sealed class RegexUtil
    {
        private System.Text.RegularExpressions.Regex blankRegex = new System.Text.RegularExpressions.Regex(@"\s{1,}", System.Text.RegularExpressions.RegexOptions.Singleline);

    }
}
