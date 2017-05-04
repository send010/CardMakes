using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsForm卡密生成器
{
    public partial class SetupLog : Form
    {
        public SetupLog()
        {
            InitializeComponent();
        }
        public SetupLog(List<string> str)
        {
            InitializeComponent();
            this.textBox1.Lines = str.ToArray();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
