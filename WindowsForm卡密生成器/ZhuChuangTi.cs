using Server卡密生成器;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using 卡密生成器;

namespace WindowsForm卡密生成器
{
    public partial class CardMake : Form
    {
        private List<string> list = new List<string>();
        private string connectionString = "";
        private string Error = "";
        bool isEnab = false;
        private DateTime make = new DateTime();
        public CardMake()
        {
            InitializeComponent();
        }

        private void btn_卡密生成_Click(object sender, EventArgs e)
        {
            if (this.tbx_前缀.Text.Length + this.tbx_后缀.Text.Length > 10)
            {
                MessageBox.Show("前缀加后缀的长度不能超过10个字节");
                return;
            }
            if ((int)num_生成数量.Value < 1)
            {
                MessageBox.Show("请输入卡密数量");
                return;
            }
            卡密生成 CardMake = new 卡密生成();
            list = CardMake.Make(this.tbx_前缀.Text, this.tbx_后缀.Text, (int)num_生成数量.Value);
            StringBuilder sb = new StringBuilder();
            foreach (var item in list)
            {
                sb.Append(item + "\r\n");
            }
            var TiKaQQ = this.tbx_TiKaQQ.Text;
            make = DateTime.Now;
            AddCardNum(this.cb_时间列表.SelectedValue.ToString(), this.cbx_DBBB.Checked, int.Parse(string.IsNullOrWhiteSpace(TiKaQQ) ? "999999999" : TiKaQQ));
            WirteTxt(sb.ToString());
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            SetupLog f2 = new SetupLog(ReadSetupLog("SetupLog.txt"));
            f2.ShowDialog();
            AllTime at = new AllTime();
            cb_时间列表.SelectedIndex = 0;
            connectionString = readTxt();
            if (!string.IsNullOrWhiteSpace(connectionString))
            {
                Error = DbHelperACE.IsOpenConn(connectionString);
                if (string.IsNullOrWhiteSpace(Error))
                {
                    isEnab = true;
                }
                else
                {
                    this.lb_Error.Text = Error;
                }
            }
            else
            {
                Error = "链接串不存在 请先右击选择  数据库文件";
            }
            this.lb_Error.Text = Error;
            List<AllTime> List = at.InitList();
            this.cb_时间列表.DisplayMember = "name";
            this.cb_时间列表.ValueMember = "times";
            this.cb_时间列表.DataSource = List;
            this.tbx_TiKaQQ.Text = "";

            InitGB();
        }
        private List<string> ReadSetupLog(string txtName)
        {
            string url = Environment.CurrentDirectory + "/" + txtName;
            return File.ReadLines(url).ToList();
        }
        private void AddCardNum(string time,bool isOldDB,int qq)
        {
            
            int nums = 0;
            List<string> sqllist = new List<string>();
            if (isOldDB)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    sqllist.Add("INSERT INTO [卡号] ([卡号],[卡号天数],[绑定机器],[生成时间],[使用时间],[到期时间],[备注],[上次解绑],[状态]) VALUES ('" + list[i] + "','" + time + "','','" + make.ToString("yyyy-MM-dd HH-mm") + "','','','','','未');");
                }
            }
            else
            {
                for (int i = 0; i < list.Count; i++)
                {
                    sqllist.Add("INSERT INTO[卡号]([卡号],[卡号天数],[绑定机器],[生成时间],[使用时间],[到期时间],[备注],[上次解绑],[状态],[代理编号]) VALUES ('" + list[i] + "','" + time + "','','" + DateTime.Now.ToString("yyyy-MM-dd HH-mm") + "','','','','','未'," + qq + ");");
                }
            }
;
            nums = DbHelperACE.InsertMultipleSQL(connectionString, sqllist);
            if (nums == list.Count)
            {
                MessageBox.Show("生成成功，已添加到数据库,一共"+nums+"条");
            }
        }
        public string readTxt()
        {
            var content = "";
            if (File.Exists(Environment.CurrentDirectory + "\\sqlAddRess.txt"))
            {
                content = File.ReadAllText(Environment.CurrentDirectory + "\\sqlAddRess.txt");
            }
            else
            {
                FileStream fi = File.Create(Environment.CurrentDirectory + "\\sqlAddRess.txt");
                fi.Close();
            };

            return content;

        }

        private void btn_deleteAllCard_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("你正在使用一键清卡功能，确定要清卡么。", "再次确定", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            };

            if (DbHelperACE.ExecuteSql(connectionString, "DELETE from [卡号]") > 0)
            {
                MessageBox.Show("删卡成功");
            }
            else {
                MessageBox.Show("删卡失败");
            };
        }

        private void btn_DelCard_Click(object sender, EventArgs e)
        {
            string sql = "delete from 卡号 where 卡号 in (";
            foreach (var item in this.tb_卡密列表.Lines)
            {
                sql += "'"+item+"',";
            }
            sql = sql.Substring(0, sql.Length-1) + ");";
            int i = DbHelperACE.ExecuteSql(connectionString, sql);
            if (i >= 0)
            {
                MessageBox.Show("删除成功");
            }
            else
            {
                MessageBox.Show("异常");
            }
        }

        private void 选择数据库位置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "ACCESS文件(*.mdb)|*.mdb";
            ofd.ValidateNames = true;
            ofd.CheckPathExists = true;
            ofd.CheckFileExists = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                CeShiMDB cs = new CeShiMDB(ofd.FileName);
                if (cs.ShowDialog() == DialogResult.OK)
                {
                    isEnab = true;
                    InitGB();
                    this.lb_Error.Text = "";
                    this.connectionString = cs.connectionString;
                    try
                    {
                        File.WriteAllText(Environment.CurrentDirectory + "\\sqlAddRess.txt", connectionString);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message,"截图联系QQ 122209722");
                    }
                    cs.Close();
                }
            }
        }
        private void InitGB()
        {
            this.gb_卡密区.Enabled = isEnab;
            this.gb_卡密设置区.Enabled = isEnab;
            this.gb_常用区.Enabled = isEnab;
            this.gb_慎用区.Enabled = isEnab;
            this.gb_卡密时间设置区.Enabled = isEnab;
        }
        private void WirteTxt(object info)
        {
            try
            {
                StreamWriter sw = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\" + DateTime.Now.ToString("yyyy年MM月dd日HH点mm分ss秒") +this.num_生成数量.Value+"张"+this.cb_时间列表.Text+ ".txt");
                sw.Write(info);
                sw.Flush();
                sw.Close();
                MessageBox.Show(this, "卡号保存在桌面", "提示对话框", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception e)
            {
                MessageBox.Show(this, "卡号保存异常:"+e.Message, "提示对话框", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_ZDYCardMake_Click(object sender, EventArgs e)
        {
            list = new List<string>(this.tb_卡密列表.Lines);
            StringBuilder sb = new StringBuilder();
            foreach (var item in list)
            {
                sb.Append(item + "\r\n");
            }
            var TiKaQQ = this.tbx_TiKaQQ.Text;
            make = DateTime.Now;
            AddCardNum(this.cb_时间列表.SelectedValue.ToString(), this.cbx_DBBB.Checked, int.Parse(string.IsNullOrWhiteSpace(TiKaQQ) ? "999999999" : TiKaQQ));
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void 查看更新日志ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Environment.CurrentDirectory + "\\SetupLog.txt");
        }
    }
    public class AllTime
    {
        public AllTime() { }
        public AllTime(string name, string times)
        {
            this.name = name;
            this.times = times;
        }
        public string name { get; set; }
        public string times { get; set; }
        public List<AllTime> InitList()
        {
            List<AllTime> all = new List<AllTime>();
            all.Add(new AllTime("1小时", "1-1"));
            all.Add(new AllTime("3小时", "3 - 3"));
            all.Add(new AllTime("10小时", "10 - 10"));
            all.Add(new AllTime("天卡", "1"));
            all.Add(new AllTime("周卡", "7"));
            all.Add(new AllTime("月卡", "30"));
            all.Add(new AllTime("季卡", "90"));
            all.Add(new AllTime("年卡", "365"));
            all.Add(new AllTime("永久卡", "999999"));
            return all;
        }
    }
}
