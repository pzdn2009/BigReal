using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BigReal.Utility
{
    public class ErrorResultDataTable
    {
        private DataTable m_ErrorDataTable = null;
        public DataTable ErrorDataTable
        {
            get { return m_ErrorDataTable; }
        }
        private const string CURRENT_ROW_ERROR = "当前行失败原因";
        private const string LAST_ERROR_ROW = "最终失败原因";

        public ErrorResultDataTable()
        {

        }

        public void InitErrorTable(DataTable sourceTable)
        {
            m_ErrorDataTable = sourceTable.Clone();

            if (!m_ErrorDataTable.Columns.Contains(CURRENT_ROW_ERROR))
            {
                m_ErrorDataTable.Columns.Add(CURRENT_ROW_ERROR, typeof(string));
            }
        }

        public void SetErrorData(DataRow sourceRow, string message)
        {
            var errorRow = m_ErrorDataTable.NewRow();
            errorRow.ItemArray = sourceRow.ItemArray;
            errorRow[CURRENT_ROW_ERROR] = message;
            m_ErrorDataTable.Rows.Add(errorRow);
        }

        public void AddLastErrorRow(string message)
        {
            if (!m_ErrorDataTable.Columns.Contains(LAST_ERROR_ROW))
            {
                m_ErrorDataTable.Columns.Add(LAST_ERROR_ROW);
            }
            var errorRow = m_ErrorDataTable.NewRow();
            errorRow[LAST_ERROR_ROW] = message;
            m_ErrorDataTable.Rows.Add(errorRow);
        }
    }
}
