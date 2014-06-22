using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLDMO;

namespace HPWorkUtility
{
    public class DBBackupHelper
    {
        // Methods
        public static void DbBackup(string serverName, string user, string password, string dbName, string backupFileName)
        {
            _Backup backUp = new BackupClass();
            SQLServer sqlServer = new SQLServerClass();
            try
            {
                sqlServer.LoginSecure = false;
                sqlServer.Connect(serverName, user, password);
                backUp.Action = SQLDMO_BACKUP_TYPE.SQLDMOBackup_Database;
                backUp.Database = dbName;
                backUp.Files = backupFileName;
                backUp.BackupSetName = dbName;
                backUp.BackupSetDescription = dbName;
                backUp.Initialize = true;
                backUp.SQLBackup(sqlServer);
            }
            catch
            {
                throw;
            }
            finally
            {
                sqlServer.DisConnect();
            }
        }

        public static void DbRestore1(string serverName, string user, string password, string dbName, string backupFileName)
        {
            SQLServer sqlServer = new SQLServerClass
            {
                LoginSecure = false
            };
            sqlServer.Connect(serverName, user, password);
            //QueryResults qr = oSQLServer.EnumProcesses(-1);
            //int iColPIDNum = -1;
            //int iColDbName = -1;
            //for (int i = 1; i <= qr.Columns; i++)
            //{
            //    string strName = qr.get_ColumnName(i);
            //    if (strName.ToUpper().Trim() == "SPID")
            //    {
            //        iColPIDNum = i;
            //    }
            //    else if (strName.ToUpper().Trim() == "DBNAME")
            //    {
            //        iColDbName = i;
            //    }
            //    if ((iColPIDNum != -1) && (iColDbName != -1))
            //    {
            //        break;
            //    }
            //}
            //for (int i = 1; i <= qr.Rows; i++)
            //{
            //    int lPID = qr.GetColumnLong(i, iColPIDNum);
            //    if (qr.GetColumnString(i, iColDbName).ToUpper() == dbName.ToUpper())
            //    {
            //        oSQLServer.KillProcess(lPID);
            //    }
            //}
            try
            {
                ((_Restore)new RestoreClass { Action = SQLDMO_RESTORE_TYPE.SQLDMORestore_Database, Database = dbName, Files = backupFileName, FileNumber = 1, ReplaceDatabase = true }).SQLRestore(sqlServer);
            }
            catch
            {
                throw;
            }
            finally
            {
                sqlServer.DisConnect();
            }
        }


        public static void DbRestore(string serverName, string user, string password, string dbName, string backupFileName)
        {
            SQLServer oSQLServer = new SQLServerClass();
            oSQLServer.LoginSecure = false;
            oSQLServer.Connect(serverName, user, password);
            QueryResults qr = oSQLServer.EnumProcesses(-1);
            int iColPIDNum = -1;
            int iColDbName = -1;
            for (int i = 1; i <= qr.Columns; i++)
            {
                string strName = qr.get_ColumnName(i);
                if (strName.ToUpper().Trim() == "SPID")
                {
                    iColPIDNum = i;
                }
                else if (strName.ToUpper().Trim() == "DBNAME")
                {
                    iColDbName = i;
                }
                if ((iColPIDNum != -1) && (iColDbName != -1))
                {
                    break;
                }
            }
            for (int i = 1; i <= qr.Rows; i++)
            {
                int lPID = qr.GetColumnLong(i, iColPIDNum);
                if (qr.GetColumnString(i, iColDbName).ToUpper() == dbName.ToUpper())
                {
                    oSQLServer.KillProcess(lPID);
                }
            }
            try
            {
                Restore oRestore = new RestoreClass();
                oRestore.Action = SQLDMO_RESTORE_TYPE.SQLDMORestore_Database;
                oRestore.Database = dbName;
                oRestore.Files = backupFileName;
                oRestore.FileNumber = 1;
                oRestore.ReplaceDatabase = true;
                oRestore.SQLRestore(oSQLServer);
            }
            catch
            {
                throw;
            }
            finally
            {
                oSQLServer.DisConnect();
            }
        }

    }
}
