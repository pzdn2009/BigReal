using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace LogisticsTracking.Lib.Service
{
    public class ServiceLogger
    {
        private static readonly bool enable = !string.IsNullOrEmpty(ConfigurationManager.AppSettings["ServiceLoggersEnable"]);

        private static IServiceLog logger;
        public static void Debug(string desc, string methodName)
        {
            if (enable)
            {
                StackFrame sf = new StackFrame(1);

                logger.Debug(desc, "", methodName);
            }
        }

        public static void Error(string desc, string methodName)
        {
            if (enable)
            {
                Console.WriteLine(desc);
                //logger.Error(desc, "", methodName);
            }
        }

        public static void Error(string desc, Exception ex)
        {
            if (enable)
            {
                logger.Error(desc, ex);
            }
        }
    }
}
