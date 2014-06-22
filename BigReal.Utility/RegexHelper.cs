using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace HPYA.BasicInfrastructure
{
    public class RegexHelper
    {
        /// <summary>
        /// 返回匹配的集合。选项：单行，多行，编译，忽略模式空格
        /// </summary>
        /// <param name="patten"></param>
        /// <param name="plaintext"></param>
        /// <returns></returns>
        public static MatchCollection GetFullOptionsMatchCollection(string pattern, string plaintext)
        {
            return GetFullOptionNewRegex(pattern).Matches(plaintext);
        }

        /// <summary>
        /// 返回单个正则表达式匹配结果的集合，如<n><m> 则n1m1n2m2n3m3...
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="names"></param>
        /// <param name="plaintext"></param>
        /// <returns></returns>
        public static IList<string>
            GetFullOptionsSequnceNameCaptureGroupMathList(string
            pattern, string[] names, string plaintext)
        {
            IList<string> ret = new List<string>();
            //取得匹配的集合
            foreach (Match ma in GetFullOptionsMatchCollection(pattern, plaintext))
            {
                for (int j = 0; j < names.Length; j++)
                {
                    ret.Add(ma.Groups[names[j]].Value);
                }
            }
            return ret;
        }

        /// <summary>
        /// 返回一个完整的正则表达式
        /// </summary>
        /// <param name="patten"></param>
        /// <returns></returns>
        private static Regex GetFullOptionNewRegex(string pattern)
        {
            Regex rg = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Multiline | RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace);
            return rg;
        }

        /// <summary>
        /// 正则替换
        /// </summary>
        /// <param name="patten"></param>
        /// <param name="plaintext"></param>
        /// <param name="replacement"></param>
        /// <returns></returns>
        public static string ReplaceFullOptions(string patten, string plaintext, string replacement)
        {
            return Regex.Replace(plaintext, patten, replacement, RegexOptions.IgnoreCase
                                   | RegexOptions.Multiline
                                   | RegexOptions.Singleline
                                   | RegexOptions.IgnorePatternWhitespace
                                   | RegexOptions.Compiled
                                   | RegexOptions.IgnoreCase
                                   );
        }
    }
}
