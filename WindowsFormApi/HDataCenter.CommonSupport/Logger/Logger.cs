using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;

namespace HDataCenter.CommonSupport
{
    public class Logger
    {
        private static readonly ILog log = LogManager.GetLogger("logApp");
        public static void Info(string msg)
        {
            log.Info(msg);
        }

        public static void Error(string msg)
        {
            log.Error(msg);
        }
    }
}
