using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Linq.Expressions;

namespace HPTeam.Data.Core.Utility
{
    /// <summary>
    /// DataTable 小工具
    /// DataTable和指定实体的转换
    /// DataTable的查询
    /// </summary>
    public static class DataTableUtility
    {
        #region DataTable Convertor Public

        /// <summary>
        /// 将实体转换为DataTable，默认为当前数据库实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static DataTable EntityToDataTable<T>(T entity) where T : class,new()
        {
            return null;
        }

        /// <summary>
        /// 实体集合转换为DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static DataTable EntityToDataTable<T>(List<T> entities) where T : class ,new()
        {
            return null;
        }

        /// <summary>
        /// DataTable 转换为实体集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<T> DataTableToEntities<T>(DataTable dt) where T : class,new()
        {
            return null;
        }

        #endregion

        #region DataTable Convertor Private

        #endregion

        #region DataTable Query Public

        /// <summary>
        /// 获取指定列名的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dr"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static T Get<T>(this DataRow dr, LambdaExpression propertyName)
        {
            return default(T);
        }

        /// <summary>
        /// 获取指定列名的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="row"></param>
        /// <param name="field"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T Get<T>(this DataRow row, string field, T defaultValue)
        {
            return default(T);
        }

        /// <summary>
        /// 按照条件查询指定的实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataTable"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static List<T> GetEntities<T>(this DataTable dataTable,Expression expression)
        {
            return null;
        }

        #endregion

        #region DataTable Query Private

        #endregion

    }
}
