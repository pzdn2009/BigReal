using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPYA.BasicInfrastructure
{
    /// <summary>
    /// 断言
    /// </summary>
    public class Guard
    {
        /// <summary>
        /// 是否为真
        /// </summary>
        public static void IsTrue(bool val)
        {
            throw new System.NotImplementedException();
        }

        public static void IsTrue(bool val, string message)
        {
            throw new System.NotImplementedException();            
        }

        public static void IsFalse(bool val)
        {
            throw new System.NotImplementedException();
        }

        public static void IsFalse(bool val,string message)
        {
            throw new System.NotImplementedException();
        }

        public void IsNull()
        {
            throw new System.NotImplementedException();
        }

        public void IsNotNull()
        {
            throw new System.NotImplementedException();
        }

        public void NotNullOrEmpty()
        {
            throw new System.NotImplementedException();
        }

        public void CanBeAssigned()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 如果参数为空，则抛出异常
        /// </summary>
        /// <param name="argValue"></param>
        /// <param name="argName"></param>
        public static void ThrowIfNull(object argValue, string argName)
        {

        }

        /// <summary>
        /// 字符型的参数为空，则抛出异常
        /// </summary>
        /// <param name="argValue"></param>
        /// <param name="argName"></param>
        public static void ThrowIfNullOrEmpty(string argValue, string argName)
        {

        }
    }
}
