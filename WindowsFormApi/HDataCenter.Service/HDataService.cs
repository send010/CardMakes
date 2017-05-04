
/*
 此程序必须以管理员身份运行
 */
using HDataCenter.IBll;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Web;
using HDataCenter.Hospital;
using System.Windows.Forms;

namespace HDataCenter.Service
{
    public class HDataService
    {
        #region 单例

        private static HDataService _HDataService;      
        public static HDataService Instance
        {
            get
            {
                if (_HDataService == null)
                {
                    _HDataService = new HDataService();
                }

                return _HDataService;

            }
        }

        #endregion
        
        private  HttpListener listener;

        private Accessor accessor;

        public EventHandler<EventArgs_Request> RequestReceived  ;
       
        public EventHandler<EventArgs_Response> AfterResponsed;

        #region 属性
        public bool IsSysSupported
        {
            get
            {
                return HttpListener.IsSupported;
            }
        }
        public bool RunFlag{get;set;}

        #endregion

        #region 构造函数
        private HDataService()
        {           
            this.listener = new HttpListener();
            this.listener.Prefixes.Add(AppConfig.ListenerAddr);
            this.accessor = new Accessor();
        }

        #endregion

        #region 方法
        public void start()
        {
            if (!this.IsSysSupported)
            {
                WriteLog("使用 HttpListener 必须为 Windows XP SP2 或 Server 2003 以上系统！");
                return;
            }

            this.listener.Start();
            RunFlag = true;

            try
            {
                while (RunFlag)
                {
                    IAsyncResult result = listener.BeginGetContext(new AsyncCallback(ListenerCallback), listener);                  
                    result.AsyncWaitHandle.WaitOne();               
                    Thread.Sleep(10);
                }
            }
            catch(Exception ex)
            {
                WriteLog(string.Format("服务start方法调用错误:{0}", ex.Message));
            }
            finally
            {
                
            }   
        }
        public void stop()
        {
            this.listener.Stop();
            this.RunFlag = false;
        }
        public  void ListenerCallback(IAsyncResult result)
        {
            if (!this.RunFlag)
                return;

            HttpListener listener = (HttpListener)result.AsyncState;
            HttpListenerContext context = listener.EndGetContext(result);
            try
            {
                HttpListenerRequest request = context.Request;
                string tempResult = this.DealRequest(request);

                if (tempResult == null)
                    return;

                ReponseRequest(context, tempResult);

                if (this.RequestReceived != null)
                {
                    this.RequestReceived(this, new EventArgs_Request(request));
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
                ReponseRequest(context, ex.Message); //有可能崩溃
            }
           
        }
        private  void ReponseRequest(HttpListenerContext context, string tempResult)
        {
            try
            {
                HttpListenerResponse response = context.Response;
                response.ContentType = "text/html; charset=UTF-8";
                response.AddHeader("Access-Control-Allow-Origin", "*");
                string responseString = tempResult;
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                response.ContentLength64 = buffer.Length;
                System.IO.Stream output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                output.Close();

                if (this.AfterResponsed != null)
                {
                    this.AfterResponsed(this, new EventArgs_Response(tempResult));
                }
            }
            catch(Exception ex)
            {
                WriteLog(ex.Message);
            }          
        }

        /// <summary>
        /// 处理请求，返回字符串
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private string DealRequest(HttpListenerRequest request)
        {
            Dictionary<string, string> dic = this.GetPostRequestParam(request);
            Newtonsoft.Json.JsonSerializer js = new Newtonsoft.Json.JsonSerializer();

            string methodname = request.RawUrl.Length == 0 ? string.Empty : request.RawUrl.Substring(1);

            if (request.HttpMethod.ToLower().Equals("get"))
            {
                int IndexA = methodname.IndexOf('?');
                if(IndexA>0)
                    methodname = methodname.Substring(0, IndexA);
            }

            if (methodname == "favicon.ico")
                return null;

            object tempResult = null;

            Type t = typeof(Accessor);
            MethodInfo mi = t.GetMethod(methodname);
            if (mi == null)
                throw new Exception(string.Format("不存在的接口方法调用->方法名称:{0}",methodname));

            List<object> listParam = new List<object>();
            foreach (ParameterInfo param in mi.GetParameters())
            {
                if (param.ParameterType == typeof(string))
                {
                    if (dic.ContainsKey(param.Name))
                        listParam.Add(dic[param.Name]);
                }
            }

            tempResult = mi.Invoke(this.accessor, listParam.ToArray());
            return Newtonsoft.Json.JsonConvert.SerializeObject(tempResult);
        }
        private void WriteLog(string msg)
        {
            //Logger.Error(msg);
        }

        private Dictionary<string,string> GetPostRequestParam(HttpListenerRequest request)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            
            if (request.HttpMethod.ToLower().Equals("get") && request.QueryString.Count>0)
            {
                int queryStart = request.RawUrl.IndexOf('?');
                if (queryStart != -1)
                {
                    NameValueCollection ss = HttpUtility.ParseQueryString(request.RawUrl.Substring(queryStart));
                    foreach (string key in ss.Keys)
                    {
                        dic.Add(HttpUtility.UrlDecode(key), HttpUtility.UrlDecode(ss[key]));
                    }
                }
            }
            else if (request.HttpMethod.ToLower().Equals("post"))
            {

                string temp = string.Empty;
                Stream SourceStream = request.InputStream;
                
                using(StreamReader sdr= new StreamReader(request.InputStream,request.ContentEncoding))
                {
                    temp=sdr.ReadToEnd();
                }

                temp =temp.Replace("", "");
                temp = System.Web.HttpUtility.UrlDecode(temp);
                foreach (var item in temp.Split('&'))
                {
                    string[] arr = item.Split('=');
                    if (arr.Length == 1)
                        dic.Add(arr[0], null);
                    else
                        dic.Add(arr[0], arr[1]);
                }
            }
            return dic;
        }

        #endregion
    }
    public class EventArgs_Request : EventArgs
    {
        private HttpListenerRequest _request;
        public EventArgs_Request(HttpListenerRequest request)
        {
            this._request = request;
        }
        public HttpListenerRequest request
        {
            get { return this._request; }
        }
    }
    public class EventArgs_Response : EventArgs
    {
        private string _responsestring;
        public EventArgs_Response(string responsestring)
        {
            this._responsestring = responsestring;
        }
        public string responsestring
        {
            get { return this._responsestring; }
        }
    }
}
