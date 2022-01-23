using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.Core.Caching
{
    public class RedisNotAvailableException : Exception
    {
        public string _errorCode = "431";
        public override string Message
        {
            get
            {
                return "Redis is not available.";
            }
        }

        public string ErrorCode
        {
            get
            {
                return _errorCode;
            }
        }
    }
}
