using HDataCenter.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Windows.Forms;

namespace HDataCenter.ServiceTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.toolStripStatusLabel1.Text = string.Empty;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HDataService.Instance.RequestReceived = this.HdDataCenter_RequestReceived;
            HDataService.Instance.AfterResponsed = this.HdDataCenter_AfterResponsed;

            Thread t = new Thread(new ThreadStart(HDataService.Instance.start));
            t.IsBackground = true;
            t.Start();

            Regex reg = new Regex(@"http:\s*//\s*\+:(.*)/");
            var match = reg.Match(Properties.Settings.Default.ListenerAddr);
            this.toolStripStatusLabel1.Text =string.Format("正在监听{0}端口...",match.Groups[1].Value.Trim());
            //HDataService.Instance.start();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            HDataService.Instance.stop();
            this.toolStripStatusLabel1.Text = "监听已停止";
        }


        private delegate void deleAddItem(string msg);
        private void AddItem(string msg)
        {
            if (!this.listBoxRequest.InvokeRequired)
            {
                this.listBoxRequest.Items.Add(msg);
            }
            else
            {
                deleAddItem dele = new deleAddItem(this.AddItem);
                this.BeginInvoke(dele, msg);
            }
        }

        private void AddItem1(string msg)
        {
            if (!this.listBoxResponse.InvokeRequired)
            {
                this.listBoxResponse.Items.Add(msg);
            }
            else
            {
                deleAddItem dele = new deleAddItem(this.AddItem1);
                this.BeginInvoke(dele, msg);
            }
        }

        private void HdDataCenter_RequestReceived(object sender, EventArgs_Request e)
        {
            IPEndPoint d = e.request.RemoteEndPoint;
            string msg = string.Format("来自{0}的{1}的服务方法请求", d.Address.ToString(), HttpUtility.UrlDecode(e.request.RawUrl.Substring(1)));
            this.AddItem(msg);
        }
        private void HdDataCenter_AfterResponsed(object sender, EventArgs_Response e)
        {
            this.AddItem1(e.responsestring);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.listBoxRequest.Items.Clear();
            this.listBoxResponse.Items.Clear();
        }

      
    }
}
