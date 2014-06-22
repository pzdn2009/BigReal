using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace BigReal.Utility
{
    public class LogHelper
    {
        static LogHelper()
        {
        }

        public static void Configure()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        public static void Error(string message)
        {
            StackFrame frame = new StackFrame(1);
            ILog log = log4net.LogManager.GetLogger(frame.GetMethod().DeclaringType);
            if (log.IsErrorEnabled)
            {
                log.Error(message);
            }

        }

        public static void Error(string message, Exception ex)
        {
            StackFrame frame = new StackFrame(1);
            ILog log = log4net.LogManager.GetLogger(frame.GetMethod().DeclaringType);
            if (log.IsErrorEnabled)
            {
                log.Error(message, ex);
            }

        }

        public static void Info(string message)
        {
            StackFrame frame = new StackFrame(1);
            ILog log = log4net.LogManager.GetLogger("loginfo");
            if (log.IsInfoEnabled)
            {
                log.Info(message);
            }

        }
    }
}
