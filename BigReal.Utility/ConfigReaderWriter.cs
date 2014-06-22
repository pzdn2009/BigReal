using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web.Configuration;

namespace BigReal.Utility
{
    /// <summary>
    /// 配置文件读写器
    /// </summary>
    public class ConfigReaderWriter
    {
        /// <summary>
        /// 获取指定的AppSetting
        /// </summary>
        /// <param name="key">键的名称</param>
        /// <returns>键值</returns>
        public static string GetAppSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        /// <summary>
        /// 获取指定的AppSetting
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键的名称</param>
        /// <returns>键值</returns>
        public static T GetAppSetting<T>(string key)
        {
            var val = GetAppSetting(key);
            return (T)Convert.ChangeType(val, typeof(T));
        }

        /// <summary>
        /// 设置AppSetting节的值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="isWebConfig">是否是WebConfig文件</param>
        public static void SetAppSetting(string key, string value, bool isWebConfig)
        {
            //增加的内容写在appSettings段下 <add key="RegCode" value="0"/>
            Configuration config = null;
            if (isWebConfig)
            {
                config = WebConfigurationManager.OpenWebConfiguration("~");
            }
            else
            {
                config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            }
            if (config.AppSettings.Settings[key] == null)
            {
                config.AppSettings.Settings.Add(key, value);
            }
            else
            {
                config.AppSettings.Settings[key].Value = value;
            }
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");//重新加载新的配置文件 
        }

        /// <summary>
        /// 读取自定义配置节
        /// </summary>
        /// <param name="sectionName">配置节的名称</param>
        /// <returns>配置对象</returns>
        public static object GetCustomSection(string sectionName)
        {
            return ConfigurationManager.GetSection(sectionName);
        }

        /// <summary>
        /// 读取自定义配置节
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="sectionName">配置节的名称</param>
        /// <returns>配置对象</returns>
        public static T GetCustomSection<T>(string sectionName) where T : class
        {
            return GetCustomSection(sectionName) as T;
        }

        /// <summary>
        /// 读取连接字符串
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>连接字符串</returns>
        public static string GetConnectionString(string name)
        {
            return GetConnectionStringSetting(name).ConnectionString;
        }

        /// <summary>
        /// 读取连接字符串设置对象
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>设置对象</returns>
        public static ConnectionStringSettings GetConnectionStringSetting(string name)
        {
            return ConfigurationManager.ConnectionStrings[name];
        }
    }
}
