using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace HDataCenter.WindowsService
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main()
        {
            HDataCenter.Service.AppConfig.ListenerAddr = Properties.Settings.Default.ListenerAddr;
            HDataCenter.Hospital.AppConfig.AddrHisService = Properties.Settings.Default.HisService;

            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new HDataCenterService() 
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
