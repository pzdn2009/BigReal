using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BigReal.Utility
{
    public class RegisterHelper
    {
        public static void WriteRegister(string subKey, string key, string value)
        {
            RegistryKey currentKey = Registry.CurrentUser.CreateSubKey(subKey);
            currentKey.SetValue(key, new EncryptionService().EncryptText(value));
            currentKey.Close();
        }

        public static string ReadRegister(string subKey, string key, string defaultVal = "-1")
        {
            RegistryKey currentKey = Registry.CurrentUser.OpenSubKey(subKey);
            if (currentKey == null)
            {
                return defaultVal;
            }

            var keyVal = currentKey.GetValue(key, defaultVal).ToString();
            currentKey.Close();
            if (keyVal != defaultVal)
            {
                return new EncryptionService().DecryptText(keyVal);
            }

            return defaultVal;
        }
    }
}
