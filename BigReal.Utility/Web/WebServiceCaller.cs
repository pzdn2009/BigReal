using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.CodeDom.Compiler;
using System.CodeDom;
using Microsoft.CSharp;
using System.Reflection;
using System.Xml;
using System.Web.Services.Description;

namespace HPCustoms.Web.Common
{
    public class WebServiceCaller
    {
        private const string WSCns = "WebServiceAccessor";

        private WebServiceCaller() { }

        public static WebServiceCaller Instance
        {
            get { return new WebServiceCaller(); }
        }
        #region Methdood

        public object InvokeWebServiceMethod(string url, string methodName, object[] args)
        {
            return InvokeWebServiceMethod(url, methodName, args);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url">服务地址</param>
        /// <param name="className">服务的类名</param>
        /// <param name="methodName">方法名</param>
        /// <param name="args">参数</param>
        /// <returns></returns>
        public object InvokeWebServiceMethod(string url, string className, string methodName, object[] args)
        {
            if (string.IsNullOrEmpty(className))
            {
                className = GetWSClassName(url);
            }
            ///生成程序集
            Assembly asm = this.AssemblyFromWsdl(this.GetWsdl(url));
            if (asm == null)
            {
                throw new Exception("生成程序集出错");
            }
            ///获取类型
            Type type = asm.GetType(string.Format("{0}.{1}", WSCns, className));
            if (type == null)
            {
                throw new Exception(string.Format("找不到指定的类型{0}", className));
            }

            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            ///创建实例
            object obj2 = Activator.CreateInstance(type);


            if ((args == null) || (args.Length == 0))
            {
                return this.Invoke(obj2, methodName, new object[0]);
            }
            return this.Invoke(obj2, methodName, args);
        }

        #endregion

        #region Private Methods

        private object Invoke(object obj, string methodName, params object[] args)
        {
            return obj.GetType().GetMethod(methodName).Invoke(obj, args);
        }

        private Assembly AssemblyFromWsdl(string strWsdl)
        {
            StringReader input = new StringReader(strWsdl);
            XmlTextReader reader2 = new XmlTextReader(input);
            ServiceDescription description = ServiceDescription.Read(reader2);
            reader2.Close();

            ServiceDescriptionImporter sdi = new ServiceDescriptionImporter();
            sdi.AddServiceDescription(description, "", "");

            //命名空间
            CodeNamespace cn = new CodeNamespace(WSCns);
            //代码编译单元
            CodeCompileUnit ccu = new CodeCompileUnit();
            ccu.Namespaces.Add(cn);
            sdi.Import(cn, ccu);

            CodeDomProvider csc = new CSharpCodeProvider();

            //设定编译参数
            CompilerParameters cplist = new CompilerParameters();
            cplist.GenerateExecutable = false;
            cplist.GenerateInMemory = true;
            cplist.ReferencedAssemblies.Add("System.dll");
            cplist.ReferencedAssemblies.Add("System.XML.dll");
            cplist.ReferencedAssemblies.Add("System.Web.Services.dll");
            cplist.ReferencedAssemblies.Add("System.Data.dll");

            //编译代理类
            CompilerResults cr = csc.CompileAssemblyFromDom(cplist, ccu);
            if (true == cr.Errors.HasErrors)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                foreach (System.CodeDom.Compiler.CompilerError ce in cr.Errors)
                {
                    sb.Append(ce.ToString());
                    sb.Append(System.Environment.NewLine);
                }
                throw new Exception(sb.ToString());
            }

            return cr.CompiledAssembly;
        }

        private string GetWsdl(string source)
        {
            if (source.StartsWith("<?xml version"))
            {
                return source;
            }
            if (source.StartsWith("http://"))
            {
                return this.WsdlFromUrl(source);
            }
            return this.WsdlFromFile(source);
        }


        private string WsdlFromFile(string fileFullPathName)
        {
            FileInfo info = new FileInfo(fileFullPathName);
            if (info.Extension != "wsdl")
            {
                throw new Exception("This is no a wsdl file");
            }
            FileStream stream = new FileStream(fileFullPathName, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);
            char[] buffer = new char[(int)stream.Length];
            reader.ReadBlock(buffer, 0, (int)stream.Length);
            return new string(buffer);
        }


        private string WsdlFromUrl(string url)
        {
            Stream stream = null;
            WebResponse response = WebRequest.Create(string.Format("{0}?WSDL", url)).GetResponse();
            if (response == null)
            {
                throw new Exception("url没有响应");
            }
            stream = response.GetResponseStream();
            if (stream == null)
            {
                throw new Exception("读取不到流");
            }
            Encoding encoder = Encoding.GetEncoding("utf-8");
            StreamReader reader = new StreamReader(stream, encoder);
            if (reader == null)
            {
                throw new Exception("读取器出错");
            }
            return reader.ReadToEnd();
        }

        private static string GetWSClassName(string wsUrl)
        {
            try
            {
                string[] parts = wsUrl.Split('/');
                string[] pps = parts[parts.Length - 1].Split('.');
                return pps[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
