using HDataCenter.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HDataCenter.WindowsService
{
    public partial class HDataCenterService : ServiceBase
    {
        public HDataCenterService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Thread t = new Thread(new ThreadStart(HDataService.Instance.start));
            t.Name = "ThreadHDataService";
            t.IsBackground = true;
            t.Start();
        }

        protected override void OnStop()
        {
            HDataService.Instance.stop();
            
        }
    }
}
