using Server卡密生成器;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsForm卡密生成器
{
    public partial class CeShiMDB : Form
    {

        public string connectionString = "";
        private string mdbpath = "";
        private string mdbpassword = "";
        public CeShiMDB()
        {
            InitializeComponent();
        }

        public CeShiMDB(string mdbpaht)
        {
            InitializeComponent();
            this.mdbpath = mdbpaht;
        }
        private void CeShiMDB_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.mdbpassword = this.textBox1.Text;
            this.connectionString = GetConnectionString();
            string Error = DbHelperACE.IsOpenConn(connectionString);
            if (string.IsNullOrWhiteSpace(Error))
            {
                MessageBox.Show("链接成功","数据库链接状态");
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show(Error, "数据库链接状态");
            }

        }
        private string GetConnectionString()
        {
            return string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source='{0}';Jet OLEDB:Database Password={1}", mdbpath, mdbpassword);
        }
    }
}
