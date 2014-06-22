using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinForm = System.Windows.Forms;

namespace BigReal.Utility
{
    public class FileDialogHelper
    {

        #region 打开一个文件
        /// <summary>
        /// 打开一个文件
        /// </summary>
        /// <returns></returns>
        public static string OpenFile()
        {
            return OpenFile(null);
        }

        public static string OpenFile(string filter)
        {
            var openFD = new WinForm.OpenFileDialog();

            if (!String.IsNullOrEmpty(filter))
            {
                openFD.Filter = filter;
            }
            var diaResult = openFD.ShowDialog();
            var fileName = openFD.FileName;
            if (diaResult != WinForm.DialogResult.OK || string.IsNullOrEmpty(fileName))
            {
                return string.Empty;
            }

            return fileName;
        }
        #endregion

        #region 保存一个文件
        public static string SaveFile()
        {
            WinForm.SaveFileDialog saveFD = new WinForm.SaveFileDialog();
            var dialogResult = saveFD.ShowDialog();
            if (dialogResult == WinForm.DialogResult.OK)
            {
                return saveFD.FileName;
            }
            return string.Empty;
        }
        #endregion
    }
}
