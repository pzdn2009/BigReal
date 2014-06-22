using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BigReal.Utility
{
    public class MD5Calculator
    {
        public string CalculateMd5Hash(string path)
        {
            try
            {
                var file = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                var getMd5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] hash_byte = getMd5.ComputeHash(file);
                string result = System.BitConverter.ToString(hash_byte);
                result = result.Replace("-", "");
                return result;
            }

            catch (Exception e)
            {
                return e.ToString();
            }
        }
    }
}
