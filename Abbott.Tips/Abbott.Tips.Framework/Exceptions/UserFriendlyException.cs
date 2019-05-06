using System;
using System.Collections.Generic;
using System.Text;

namespace Abbott.Tips.Framework.Exceptions
{
    public class UserFriendlyException : Exception
    {
        public UserFriendlyException(string message) : base(message)
        {

        }
    }
}
