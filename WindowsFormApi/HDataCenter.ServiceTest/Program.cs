using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace HDataCenter.ServiceTest
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
       
            HDataCenter.Service.AppConfig.ListenerAddr = Properties.Settings.Default.ListenerAddr;
            HDataCenter.Hospital.AppConfig.AddrHisService = Properties.Settings.Default.HisService;
            
            Application.Run(new Form1());     
        }
    }
}
