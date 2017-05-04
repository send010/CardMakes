/*
 *  接口方法的参数只能是字符串或类  
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;

namespace HDataCenter.IBll
{
    public class JsonObject
    {
        public string code{get;set;} //成功 返回 A00000  接口错误：B00003
        public string msg { get; set; }
        public object data { get; set; }

    }
}
