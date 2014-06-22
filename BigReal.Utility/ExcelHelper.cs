using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BigReal.Utility
{
    /// <summary>
    /// Excel助手
    /// </summary>
    public class ExcelHelper
    {
        #region 读取表头
        /// <summary>
        /// 读取表头，返回Sheet1，且表头在第0行
        /// </summary>
        /// <param name="filePath">文件名</param>
        /// <param name="sheetName">指定sheet名</param>
        /// <returns></returns>
        public static DataTable GetHeader(string filePath, string sheetName)
        {
            return GetHeader(filePath, sheetName, 0);
        }

        public static DataTable GetHeader(string filePath, int sheetIndex)
        {
            return GetHeader(filePath, sheetIndex, 0);
        }

        public static DataTable GetHeader(string filePath, string sheetName, int headerIndex)
        {
            return GetHeader(filePath, sheetName, null, headerIndex);
        }

        public static DataTable GetHeader(string filePath, int sheetIndex, int headerIndex)
        {
            return GetHeader(filePath, null, sheetIndex, headerIndex);
        }

        private static DataTable GetHeader(string filePath, string sheetName, int? sheetIndex, int headerIndex)
        {
            var workbook = GetWorkbook(filePath);
            ISheet sheet;
            if (sheetIndex.HasValue)
            {
                sheet = workbook.GetSheetAt(sheetIndex.Value);
            }
            else
            {
                sheet = workbook.GetSheet(sheetName);
            }
            //Guard.IsNull(sheet);

            var row = sheet.GetRow(headerIndex);
            //Guard.IsNotNull(row);
            if (row == null)
            {
                throw new Exception(string.Format("Excel文件{0}中第{1}行没有标题", filePath, headerIndex));
            }

            DataTable dt = new DataTable();

            for (int i = 0; i < row.LastCellNum; i++)
            {
                ICell cell = row.GetCell(i);
                DataColumn column = new DataColumn(cell.ToString());
                dt.Columns.Add(column);
            }

            return dt;
        }

        #endregion

        #region 导入数据

        #endregion

        public static DataTable GetContent(string filePath, int sheetIndex)
        {
            IWorkbook workbook = GetWorkbook(filePath);
            ISheet sheet = workbook.GetSheetAt(sheetIndex);
            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

            DataTable dt = new DataTable();

            int t = 0;
            while (rows.MoveNext())
            {
                IRow row = (IRow)rows.Current;
                int cellCount = row.LastCellNum;

                DataRow dr = dt.NewRow();

                for (int i = 0; i < row.LastCellNum; i++)
                {
                    ICell cell = row.GetCell(i);

                    if (t == 0)
                    {
                        DataColumn column = new DataColumn(cell.ToString());
                        dt.Columns.Add(column);
                    }
                    if (cell == null)
                    {
                        dr[i] = null;
                    }
                    else
                    {
                        dr[i] = cell.ToString();
                    }
                }
                dt.Rows.Add(dr);
                t++;
            }

            return dt;
        }

        public static IWorkbook GetWorkbook(string filePath)
        {
            IWorkbook workbook;
            using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                if (filePath.Contains(".xlsx"))
                {
                    workbook = new XSSFWorkbook(file);
                }
                else
                {
                    workbook = new HSSFWorkbook(file);
                }
            }
            return workbook;
        }

        /// <summary>
        /// 将数据导出至Excel中
        /// </summary>
        /// <param name="caption">标题</param>
        /// <param name="heading">行标题</param>
        /// <param name="dtSource">要导出的DataTable</param>
        /// <param name="targetPath">保存的路径(含文件名)</param>
        public static void DataTableToExcel(string caption, string[] heading, DataTable dtSource, string targetPath)
        {
            IWorkbook workbook;
            ISheet sheet;
            ICellStyle headStyle;
            IFont font;
            FileInfo fTargetPath = new FileInfo(targetPath);
            if (fTargetPath.Extension != ".xls" && fTargetPath.Extension != ".xlsx")
            {
                targetPath = targetPath + ".xls";

            }

            if (targetPath.EndsWith(".xlsx"))
            {
                workbook = new XSSFWorkbook();
                sheet = (XSSFSheet)workbook.CreateSheet();
                headStyle = (XSSFCellStyle)workbook.CreateCellStyle();
                font = (XSSFFont)workbook.CreateFont();
            }
            else
            {
                workbook = new NPOI.HSSF.UserModel.HSSFWorkbook();
                sheet = (HSSFSheet)workbook.CreateSheet();
                headStyle = (HSSFCellStyle)workbook.CreateCellStyle();
                font = (HSSFFont)workbook.CreateFont();
            }

            try
            {
                int startRow = 0;
                IRow rowCaption = sheet.CreateRow(startRow);
                if (!string.IsNullOrEmpty(caption))
                {
                    int cellIndex = (int)dtSource.Columns.Count / 2;

                    rowCaption = sheet.CreateRow(0);
                    rowCaption.HeightInPoints = 25;
                    rowCaption.CreateCell(cellIndex).SetCellValue(caption);

                    font.FontHeightInPoints = 20;
                    font.Boldweight = 700;
                    headStyle.SetFont(font);
                    rowCaption.GetCell(cellIndex).CellStyle = headStyle;

                    startRow++;
                }

                IRow rowHead = sheet.CreateRow(startRow);
                startRow++;
                if (heading == null)
                {
                    DataColumnCollection cols = dtSource.Columns;
                    for (int i = 0; i < cols.Count; i++)
                    {
                        rowHead.CreateCell(i, CellType.String).SetCellValue(cols[i].Caption.ToString());
                    }
                }
                else
                {
                    for (int i = 0; i < heading.Length; i++)
                    {
                        rowHead.CreateCell(i, CellType.String).SetCellValue(heading[i].ToString());
                    }
                }

                for (int r = 0; r < dtSource.Rows.Count; r++)
                {
                    IRow row = sheet.CreateRow(r + startRow);
                    for (int c = 0; c < dtSource.Columns.Count; c++)
                    {
                        row.CreateCell(c, CellType.String).SetCellValue(dtSource.Rows[r][c].ToString());

                    }
                }

                using (FileStream fs = new FileStream(targetPath, FileMode.Create, FileAccess.Write))
                {
                    workbook.Write(fs);
                    fs.Close();
                }

                GC.Collect();
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex);
                throw ex;
            }
        }

        public static void DataTableToExcel(DataTable dtSource, string targetPath)
        {
            DataTableToExcel(null, null, dtSource, targetPath);
        }

        public static DataTable ListToDataTable<T>(IEnumerable<T> list)
        {
            DataTable dt = new DataTable();
            if (list.Count() == 0)
            {
                return dt;
            }
            object o = list.ToList()[0];
            List<PropertyInfo> pList = new List<PropertyInfo>();
            Type type = o.GetType();
            Array.ForEach<PropertyInfo>(type.GetProperties(), p => { pList.Add(p); dt.Columns.Add(p.Name); });
            foreach (var item in list)
            {
                DataRow row = dt.NewRow();
                pList.ForEach(p => row[p.Name] = p.GetValue(item, null));
                dt.Rows.Add(row);
            }
            return dt;
        }
    }
}
