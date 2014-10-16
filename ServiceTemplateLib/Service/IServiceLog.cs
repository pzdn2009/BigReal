using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogisticsTracking.Lib.Service
{
    public interface IServiceLog
    {
        void Debug(string msg);

        void Debug(string msg, string className, string methodName);

        void Error(string msg);

        void Error(string msg, string className, string methodName);

        void Error(string msg, Exception ex);
    }
}
