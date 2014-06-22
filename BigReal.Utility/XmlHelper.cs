using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Data;

namespace HPCustoms.Helper
{
    public class XmlHelper
    {
        // Methods
        private XmlHelper() { }

        #region Create Load

        /// <summary>
        /// 通过xml字符串创建xml文档对象
        /// memo：需要正确的xml节
        /// </summary>
        /// <param name="strXML">xml字符串</param>
        /// <returns>xml文档对象</returns>
        public static XmlDocument CreateDocument(string strXML)
        {
            var document = new XmlDocument();
            document.LoadXml(strXML);
            return document;
        }

        /// <summary>
        /// 通过xml文件地址创建xml文档对象
        /// </summary>
        /// <param name="strPath">文档所在的路径</param>
        /// <returns>xml文档对象</returns>
        public static XmlDocument LoadDocument(string strPath)
        {
            using (FileStream stream = File.Open(strPath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                XmlDocument document = new XmlDocument();
                document.Load(stream);
                return document;
            }
        }

        #endregion

        #region GetDocument From DataSet

        public static XmlDocument GetDocument(DataSet dataSet)
        {
            return GetDocument(dataSet, null, false, false);
        }

        public static XmlDocument GetDocument(DataSet dataSet, XmlDocument doc)
        {
            return GetDocument(dataSet, doc, false, false);
        }

        public static XmlDocument GetDocument(DataSet dataSet, XmlDocument doc, bool bCDataSection)
        {
            return GetDocument(dataSet, doc, bCDataSection, false);
        }

        /// <summary>
        /// 根据DataSet生成Xml文档。
        /// CData节，属性，节点，三选一。
        /// </summary>
        /// <param name="dataSet">ds</param>
        /// <param name="doc">传入的文档对象</param>
        /// <param name="bCDataSection">是否为CData节</param>
        /// <param name="bAttribute">是否为属性</param>
        /// <returns>新的xml文档对象</returns>
        public static XmlDocument GetDocument(DataSet dataSet, XmlDocument doc, bool bCDataSection, bool bAttribute)
        {
            DataSet ds = dataSet.Copy();
            XmlDocument document = null;
            if (doc == null)
            {
                document = new XmlDocument();
                document.LoadXml("<DataSet/>");
            }
            else
            {
                document = (XmlDocument)doc.Clone();
            }
            XmlNode root = document.DocumentElement;   //Insert at Root Element
            foreach (DataTable table in ds.Tables)  //Every table 
            {
                foreach (DataRow row in table.Rows)  //Every Row
                {
                    XmlNode node = AppendNode(root, table.TableName);  //a table as a node.
                    foreach (DataColumn column in table.Columns)
                    {
                        string str = DBValueToString(row[column]);
                        if (bAttribute)   //Set attr 
                        {
                            ((XmlElement)node).SetAttribute(column.ColumnName, str);
                        }
                        else
                        {
                            if (bCDataSection)   //Set CDataSection
                            {
                                AppendCDataNode(node, column.ColumnName, str);
                                continue;
                            }
                            AppendNode(node, column.ColumnName, str);  // append subNode
                        }
                    }
                }
            }
            return document;
        }
        #endregion

        #region GetDocument From DataTable

        public static XmlDocument GetDocument(DataTable dataTable)
        {
            return GetDocument(dataTable, null, false, false);
        }

        public static XmlDocument GetDocument(DataTable dataTable, XmlDocument doc)
        {
            return GetDocument(dataTable, doc, false, false);
        }

        public static XmlDocument GetDocument(DataTable dataTable, XmlDocument doc, bool bCDataSection)
        {
            return GetDocument(dataTable, doc, bCDataSection);
        }

        public static XmlDocument GetDocument(DataTable dataTable, XmlDocument doc, bool bCDataSection, bool bAttribute)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(dataTable.Copy());  //构造一个DataSet的副本
            return GetDocument(ds, doc, bCDataSection, bAttribute);
        }

        #endregion

        #region GetDocument From IDataReader

        public static XmlDocument GetDocument(IDataReader dataReader)
        {
            return GetDocument(dataReader, null, false, false);
        }

        public static XmlDocument GetDocument(IDataReader dataReader, XmlDocument doc)
        {
            return GetDocument(dataReader, doc, false, false);
        }

        public static XmlDocument GetDocument(IDataReader dataReader, XmlDocument doc, bool bCDataSection)
        {
            return GetDocument(dataReader, doc, bCDataSection, false);
        }

        public static XmlDocument GetDocument(IDataReader dataReader, XmlDocument doc, bool bCDataSection, bool bAttribute)
        {
            CheckParam(dataReader);
            XmlDocument document = null;
            if (doc == null)
            {
                document = new XmlDocument();
                document.LoadXml("<DataSet/>");
            }
            else
            {
                document = (XmlDocument)doc.Clone();
            }
            XmlNode root = document.DocumentElement;
            AppendNode4Reader(dataReader, ref root, bCDataSection, bAttribute, string.Empty);
            while (dataReader.NextResult())
            {
                AppendNode4Reader(dataReader, ref root, bCDataSection, bAttribute, string.Empty);
            }
            return document;
        }

        public static XmlDocument GetDocument(IDataReader dataReader, XmlDocument doc, bool bCDataSection, bool bAttribute, string[] strTableNames)
        {
            XmlDocument document = null;
            if (doc == null)
            {
                document = new XmlDocument();
                document.LoadXml("<DataSet/>");
            }
            else
            {
                document = (XmlDocument)doc.Clone();
            }
            XmlNode root = document.DocumentElement;
            AppendNode4Reader(dataReader, ref root, bCDataSection, bAttribute, (strTableNames == null) ? string.Empty : strTableNames[0]);
            int index = 0;
            while (dataReader.NextResult())
            {
                index++;
                string strTableName = string.Empty;
                if ((strTableNames != null) && (index < strTableNames.Length))
                {
                    strTableName = strTableNames[index];
                }
                AppendNode4Reader(dataReader, ref root, bCDataSection, bAttribute, strTableName);
            }
            return document;
        }

        #endregion

        #region Append ———— Attr

        public static XmlAttribute AppendAttribute(XmlNode node, string attrName, string attrValue)
        {
            XmlAttribute attr = node.Attributes[attrName];
            if (attr == null)
            {
                attr = node.OwnerDocument.CreateAttribute(attrName);
                node.Attributes.SetNamedItem(attr);
            }
            attr.Value = attrValue;
            return attr;
        }

        #endregion

        #region Append ———— CData

        public static XmlNode AppendCDataNode(XmlNode node, string strNodeName, string strNodeText)
        {
            CheckParam(node);
            CheckParam(strNodeName);
            XmlNode node2 = AppendNode(node, strNodeName);
            XmlCDataSection newChild = node.OwnerDocument.CreateCDataSection(strNodeText);
            node2.AppendChild(newChild);
            return node2;
        }

        #endregion

        #region Append ———— Node

        public static XmlNode AppendNode(XmlNode node, string strNodeName)
        {
            var node2 = node.OwnerDocument.CreateNode(XmlNodeType.Element, strNodeName, String.Empty);
            node.AppendChild(node2);
            return node2;
        }

        public static XmlNode AppendNode(XmlNode node, string strNodeName, string strNodeText)
        {
            var node2 = node.OwnerDocument.CreateNode(XmlNodeType.Element, strNodeName, String.Empty);
            node2.InnerText = strNodeText;
            node.AppendChild(node2);
            return node2;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="strNodeName"></param>
        /// <param name="nodeSrc">需要拷贝的节点</param>
        /// <returns></returns>
        public static XmlNode AppendNode(XmlNode node, string strNodeName, XmlNode nodeSrc)
        {
            var node2 = node.OwnerDocument.CreateNode(XmlNodeType.Element, strNodeName, String.Empty);
            node2.InnerXml = nodeSrc.InnerXml;
            return node2;
        }

        #endregion

        private static void AppendNode4Reader(IDataReader dataReader, ref XmlNode root, bool bCDataSection, bool bAttribute, string strTableName)
        {
            if ((strTableName == null) || (string.Empty == strTableName))
            {
                if (!root.HasChildNodes)
                {
                    strTableName = "Table0";
                }
                else
                {
                    strTableName = string.Format("Table{0}", int.Parse(root.LastChild.Name.Substring(5)) + 1);
                }
            }
            while (dataReader.Read())
            {
                XmlNode node = AppendNode(root, strTableName);
                for (int i = 0; i < dataReader.FieldCount; i++)
                {
                    string str = DBValueToString(dataReader[i]);
                    if (bAttribute)
                    {
                        ((XmlElement)node).SetAttribute(dataReader.GetName(i), str);
                    }
                    else if (bCDataSection)
                    {
                        AppendCDataNode(node, dataReader.GetName(i), str);
                    }
                    else
                    {
                        AppendNode(node, dataReader.GetName(i), str);
                    }
                }
            }

        }

        public static XmlNode AppendNotExistsNode(XmlNode node, string strNodeName)
        {
            return AppendNotExistsNode(node, strNodeName, string.Empty);

        }

        public static XmlNode AppendNotExistsNode(XmlNode node, string strNodeName, string strNodeText)
        {
            CheckParam(node);
            CheckParam(strNodeName);
            XmlNode node2 = node.SelectSingleNode(strNodeName);
            if (node2 == null)
            {
                node2 = AppendNode(node, strNodeName);
            }
            if (string.Empty == node2.InnerText)
            {
                node2.InnerText = (strNodeText == null) ? string.Empty : strNodeText;
            }
            return node2;

        }

        #region 检查参数

        private static void CheckParam(DataSet dataSet)
        {
            if (dataSet == null)
            {
                throw new ArgumentNullException("dataSet", "入口参数：数据集为空");
            }
        }

        private static void CheckParam(DataTable table)
        {
            if (table == null)
            {
                throw new ArgumentNullException("table", "入口参数：数据集为空");
            }
        }
        private static void CheckParam(IDataReader dataReader)
        {
            if (dataReader == null)
            {
                throw new ArgumentNullException("dataReader", "入口参数：数据集为空");
            }
        }

        private static void CheckParam(string str)
        {
            CheckParam(str, "子节点名", "strNodeName");
        }
        private static void CheckParam(XmlDocument doc)
        {
            if (doc == null)
            {
                throw new ArgumentNullException("doc", "入口参数：XML文档为空");
            }
        }
        private static void CheckParam(XmlNode node)
        {
            CheckParam(node, "根节点", "node");
        }
        private static void CheckParam(XmlNodeList nodeList)
        {
            if ((nodeList == null) || (nodeList.Count == 0))
            {
                throw new ArgumentNullException("nodeList", "入口参数：节点列表为空");
            }
        }

        private static void CheckParam(string str, string strMsg, string strParam)
        {
            if ((str == null) || (str.Trim().Length == 0))
            {
                throw new ArgumentException(string.Format("入口参数：{0}为空", strMsg), strParam);
            }
        }
        private static void CheckParam(XmlNode node, string strMsg, string strParam)
        {
            if (node == null)
            {
                throw new ArgumentNullException(strParam, string.Format("入口参数：{0}为空", strMsg));
            }
        }

        #endregion

        private static string DateTime2LongString(DateTime dt)
        {
            string str = null;
            if (dt > new DateTime(0x6d9, 1, 1, 0, 0, 0, 0))
            {
                str = string.Format("{0:yyyy-MM-dd HH:mm:ss}", dt);
            }
            return str;
        }

        private static string DBValueToString(object objValue)
        {
            string str = string.Empty;
            if (objValue == null)
            {
                return str;
            }
            if (objValue is DateTime)
            {
                return DateTime2LongString((DateTime)objValue);
            }
            return objValue.ToString();
        }

        #region 格式化

        public static string FormatXml(string xmlFile)
        {
            return FormatXml(LoadDocument(xmlFile));
        }

        public static string FormatXml(XmlDocument doc)
        {
            if (doc == null)
            {
                throw new ArgumentNullException("doc", "传入的XmlDocument是null");
            }
            StringBuilder sb = new StringBuilder();
            using (StringWriter writer = new StringWriter(sb))
            {
                XmlTextWriter w = new XmlTextWriter(writer)
                {
                    Indentation = 1,
                    IndentChar = '\t',
                    Formatting = Formatting.Indented
                };
                doc.Save(w);
                w.Close();
            }
            return sb.ToString();
        }

        #endregion

        #region 获取属性

        public static string GetAttribute(XmlNode node, string attributeName)
        {
            return GetAttribute(node, attributeName, String.Empty);
        }

        public static string GetAttribute(XmlNode node, string attributeName, string strDefaultValue)
        {
            var attr = node.Attributes[attributeName];
            string value = String.Empty;
            if (attr != null)
            {
                value = attr.Value;
            }
            return value;
        }

        #endregion

        #region 获取单个节点的文本

        public static string GetSingleNodeText(XmlNode node, string strXPath)
        {
            return GetSingleNodeText(node, strXPath, String.Empty);
        }

        public static string GetSingleNodeText(XmlNode node, string strXPath, string strDefault)
        {
            XmlNode node2 = node.SelectSingleNode(strXPath);
            if (node2 == null)
            {
                return strDefault;
            }
            return node2.InnerText;
        }

        #endregion

        #region 替换

        public static XmlNodeList ReplaceNodes(XmlNode node, string strNodeName, string strNodeText)
        {
            XmlNodeList list = node.SelectNodes(".//" + strNodeName);
            foreach (XmlNode node2 in list)
            {
                node2.InnerText = strNodeText;
            }
            return list;
        }

        public static XmlNode ReplaceSubNode(XmlNode node, string strNodeName, string strNodeText)
        {
            XmlNode node2 = node.SelectSingleNode(strNodeName);
            if (node2 == null)
            {
                node2 = AppendNode(node, strNodeName);
            }
            node2.InnerText = strNodeText;
            return node2;
        }

        #endregion

        #region 
        
        /// <summary>
        /// 把数据表转换成Excel格式的XML字符串[!1]
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="encodingName">编码方式</param>
        /// <returns></returns>
        private static string GetXMLForExcel(DataTable dt, string encodingName)
        {
            StringBuilder builder = new StringBuilder();
            StringWriter writer = new StringWriter(builder);
            string encoding = string.IsNullOrEmpty(encodingName) ? "utf-8" : encodingName;
            writer.Write("<?xml version=\"1.0\" encoding=\"" + encoding + "\"?>");
            writer.Write("<Workbook xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\" xmlns:o=\"urn:schemas-microsoft-com:office:office\" xmlns:x=\"urn:schemas-microsoft-com:office:excel\" xmlns:ss=\"urn:schemas-microsoft-com:office:spreadsheet\" xmlns:html=\"http://www.w3.org/TR/REC-html40\" xmlns:msxsl=\"urn:schemas-microsoft-com:xslt\" xmlns:user=\"urn:my-scripts\">");
            writer.Write("<Worksheet ss:Name=\"record\">");
            writer.Write("<Table x:FullColumns=\"1\" x:FullRows=\"1\">");
            int columnCount = dt.Columns.Count;
            writer.Write("<Row>");
            for (int i = 0; i < columnCount; i++)
            {
                writer.Write(string.Format("<Cell><Data ss:Type=\"String\">{0}</Data></Cell>", GetXmlString(dt.Columns[i].Caption)));
            }
            writer.Write("</Row>");
            foreach (DataRow row in dt.Rows)
            {
                writer.Write("<Row>");
                for (int i = 0; i < columnCount; i++)
                {
                    writer.Write(string.Format("<Cell><Data ss:Type=\"String\">{0}</Data></Cell>", GetXmlString(DBValueToString(row[i]))));
                }
                writer.Write("</Row>");
            }
            writer.Write("</Table>");
            writer.Write("</Worksheet>");
            writer.Write("</Workbook>");
            writer.Close();
            return builder.ToString();
        }

        /// <summary>
        /// 把数据表转换成Excel格式的XML字符串[!1]
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string GetXMLForExcel(DataTable dt)
        {
            return GetXMLForExcel(dt, "utf-8");
        }

        /// <summary>
        /// 获取经过XML转义后的字符串[!1]
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetXmlString(string value)
        {
            XmlDocument xml = new XmlDocument();
            return xml.CreateTextNode(value).OuterXml;
        }

        #endregion
    }
}
