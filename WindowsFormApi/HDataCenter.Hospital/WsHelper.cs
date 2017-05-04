using Microsoft.CSharp;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Web.Services.Description;

namespace HDataCenter.Hospital
{
    public class WSHelper
    {
        #region 单例

        private static WSHelper instance;
        private static object lockobj = new object();
        private WSHelper()
        {

        }

        public static WSHelper Instance
        {
            get
            {
                lock (lockobj)
                {
                    if (instance == null)
                    {
                        instance = new WSHelper();
                    }
                    return instance;
                }
            }
        }

        #endregion

        private const string @namespace = "EnterpriseServerBase.WebService.DynamicWebCalling";

        private string url = AppConfig.AddrHisService;

        private Assembly asm;
        public Assembly Asm
        {
            get
            {
                if (asm == null)
                    asm = CreateAsm(url, @namespace);

                return asm;
            }

        }


        /// < summary>           
        /// 动态调用web服务         
        /// < /summary>          
        /// < param name="url">WSDL服务地址< /param> 
        /// < param name="methodname">方法名< /param>           
        /// < param name="args">参数< /param>           
        /// < returns>< /returns>          
        public object InvokeWebService(string url, string methodname, object[] args)
        {
            return InvokeWebService(methodname, args);
        }
        /// < summary>          
        /// 动态调用web服务 
        /// < /summary>          
        /// < param name="url">WSDL服务地址< /param>
        /// < param name="classname">类名< /param>  
        /// < param name="methodname">方法名< /param>  
        /// < param name="args">参数< /param> 
        /// < returns>< /returns>
        public object InvokeWebService(string methodname, object[] args)
        {
            try
            {
                Type t = this.Asm.GetType(@namespace + "." + this.GetWsClassName(this.url), true, true);
                object obj = Activator.CreateInstance(t);
                System.Reflection.MethodInfo mi = t.GetMethod(methodname);
                return mi.Invoke(obj, args);
                // PropertyInfo propertyInfo = type.GetProperty(propertyname);     
                //return propertyInfo.GetValue(obj, null); 
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message, new Exception(ex.InnerException.StackTrace));
            }
        }

        private Assembly CreateAsm(string url, string @namespace)
        {
            WebClient wc = new WebClient();
            Stream stream = wc.OpenRead(url + "?WSDL");
            ServiceDescription sd = ServiceDescription.Read(stream);
            ServiceDescriptionImporter sdi = new ServiceDescriptionImporter();
            sdi.AddServiceDescription(sd, "", "");
            CodeNamespace cn = new CodeNamespace(@namespace);
            //生成客户端代理类代码          
            CodeCompileUnit ccu = new CodeCompileUnit();
            ccu.Namespaces.Add(cn);
            sdi.Import(cn, ccu);
            CSharpCodeProvider icc = new CSharpCodeProvider();
            //设定编译参数                 
            CompilerParameters cplist = new CompilerParameters();
            cplist.GenerateExecutable = false;
            cplist.GenerateInMemory = true;
            cplist.ReferencedAssemblies.Add("System.dll");
            cplist.ReferencedAssemblies.Add("System.XML.dll");
            cplist.ReferencedAssemblies.Add("System.Web.Services.dll");
            cplist.ReferencedAssemblies.Add("System.Data.dll");
            //编译代理类                 
            CompilerResults cr = icc.CompileAssemblyFromDom(cplist, ccu);
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
            //生成代理实例，并调用方法   
            System.Reflection.Assembly assembly = cr.CompiledAssembly;
            return assembly;
        }
        private string GetWsClassName(string wsUrl)
        {
            string[] parts = wsUrl.Split('/');
            string[] pps = parts[parts.Length - 1].Split('.');
            return pps[0];
        }
    }
}
