using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigReal.Utility.Extensions
{
    /// <summary>
    /// 目录文件扩展
    /// </summary>
    public static class DirectoryAndFileExtension
    {
        /// <summary>
        /// 复制目录
        /// </summary>
        /// <param name="sourceDir">源目录</param>
        /// <param name="targetDir">目标目录</param>
        /// <param name="onlyDirectory">是否只复制文件夹</param>
        public static void CopyTo(this DirectoryInfo sourceDir, DirectoryInfo targetDir, bool onlyDirectory)
        {
            var dirInfos = sourceDir.GetDirectories();
            foreach (var dir in dirInfos)
            {
                var subDir = targetDir.CreateSubdirectory(dir.Name);

                if (!onlyDirectory)
                {
                    var childrenFiles = sourceDir.GetFiles();
                    var targetFiles = targetDir.GetFiles();
                    foreach (FileInfo file in childrenFiles)
                    {
                        string path = Path.Combine(targetDir.FullName, file.Name);
                        if (!path.FileExist())
                        {
                            file.CopyTo(path);
                        }
                    }
                }

                CopyTo(dir, subDir, onlyDirectory);
            }
        }

        /// <summary>
        /// 文件夹是否存在
        /// </summary>
        /// <param name="str">目录地址</param>
        /// <returns>true：存在；false：不存在</returns>
        public static bool DirectoryExist(this string str)
        {
            return Directory.Exists(str);
        }

        /// <summary>
        /// 文件是否存在
        /// </summary>
        /// <param name="str">文件地址</param>
        /// <returns>true：存在；false：不存在</returns>
        public static bool FileExist(this string str)
        {
            return File.Exists(str);
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="str">文件路径</param>
        public static void Delete(this string str)
        {
            File.Delete(str);
        }

        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="str">目录地址</param>
        public static void CreateDirectory(this string str)
        {
            Directory.CreateDirectory(str);
        }
    }
}
