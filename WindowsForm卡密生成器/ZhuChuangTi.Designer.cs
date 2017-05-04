namespace WindowsForm卡密生成器
{
    partial class CardMake
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CardMake));
            this.tb_卡密列表 = new System.Windows.Forms.TextBox();
            this.btn_deleteAllCard = new System.Windows.Forms.Button();
            this.gb_慎用区 = new System.Windows.Forms.GroupBox();
            this.gb_常用区 = new System.Windows.Forms.GroupBox();
            this.btn_DelCard = new System.Windows.Forms.Button();
            this.btn_ZDYCardMake = new System.Windows.Forms.Button();
            this.btn_卡密生成 = new System.Windows.Forms.Button();
            this.num_生成数量 = new System.Windows.Forms.NumericUpDown();
            this.tbx_TiKaQQ = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbx_DBBB = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.gb_卡密设置区 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tbx_前缀 = new System.Windows.Forms.TextBox();
            this.tbx_后缀 = new System.Windows.Forms.TextBox();
            this.gb_卡密区 = new System.Windows.Forms.GroupBox();
            this.cNS_YJMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.选择数据库位置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.查看更新日志ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lb_Error = new System.Windows.Forms.Label();
            this.gb_卡密时间设置区 = new System.Windows.Forms.GroupBox();
            this.cb_时间列表 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gb_慎用区.SuspendLayout();
            this.gb_常用区.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_生成数量)).BeginInit();
            this.gb_卡密设置区.SuspendLayout();
            this.gb_卡密区.SuspendLayout();
            this.cNS_YJMenu.SuspendLayout();
            this.gb_卡密时间设置区.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tb_卡密列表
            // 
            this.tb_卡密列表.AllowDrop = true;
            this.tb_卡密列表.BackColor = System.Drawing.Color.White;
            this.tb_卡密列表.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tb_卡密列表.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_卡密列表.Location = new System.Drawing.Point(3, 21);
            this.tb_卡密列表.Multiline = true;
            this.tb_卡密列表.Name = "tb_卡密列表";
            this.tb_卡密列表.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_卡密列表.Size = new System.Drawing.Size(296, 527);
            this.tb_卡密列表.TabIndex = 1;
            // 
            // btn_deleteAllCard
            // 
            this.btn_deleteAllCard.Location = new System.Drawing.Point(21, 24);
            this.btn_deleteAllCard.Name = "btn_deleteAllCard";
            this.btn_deleteAllCard.Size = new System.Drawing.Size(116, 35);
            this.btn_deleteAllCard.TabIndex = 0;
            this.btn_deleteAllCard.Text = "一键清卡";
            this.btn_deleteAllCard.UseVisualStyleBackColor = true;
            this.btn_deleteAllCard.Click += new System.EventHandler(this.btn_deleteAllCard_Click);
            // 
            // gb_慎用区
            // 
            this.gb_慎用区.BackColor = System.Drawing.Color.Transparent;
            this.gb_慎用区.Controls.Add(this.btn_deleteAllCard);
            this.gb_慎用区.Enabled = false;
            this.gb_慎用区.ForeColor = System.Drawing.Color.Red;
            this.gb_慎用区.Location = new System.Drawing.Point(3, 475);
            this.gb_慎用区.Name = "gb_慎用区";
            this.gb_慎用区.Size = new System.Drawing.Size(352, 70);
            this.gb_慎用区.TabIndex = 8;
            this.gb_慎用区.TabStop = false;
            this.gb_慎用区.Text = "慎用区";
            // 
            // gb_常用区
            // 
            this.gb_常用区.BackColor = System.Drawing.Color.Transparent;
            this.gb_常用区.Controls.Add(this.btn_DelCard);
            this.gb_常用区.Controls.Add(this.btn_ZDYCardMake);
            this.gb_常用区.Controls.Add(this.btn_卡密生成);
            this.gb_常用区.Enabled = false;
            this.gb_常用区.Location = new System.Drawing.Point(3, 307);
            this.gb_常用区.Name = "gb_常用区";
            this.gb_常用区.Size = new System.Drawing.Size(352, 153);
            this.gb_常用区.TabIndex = 8;
            this.gb_常用区.TabStop = false;
            this.gb_常用区.Text = "常用区";
            // 
            // btn_DelCard
            // 
            this.btn_DelCard.Location = new System.Drawing.Point(197, 26);
            this.btn_DelCard.Name = "btn_DelCard";
            this.btn_DelCard.Size = new System.Drawing.Size(116, 35);
            this.btn_DelCard.TabIndex = 1;
            this.btn_DelCard.Text = "卡密删除";
            this.btn_DelCard.UseVisualStyleBackColor = true;
            this.btn_DelCard.Click += new System.EventHandler(this.btn_DelCard_Click);
            // 
            // btn_ZDYCardMake
            // 
            this.btn_ZDYCardMake.Location = new System.Drawing.Point(21, 92);
            this.btn_ZDYCardMake.Name = "btn_ZDYCardMake";
            this.btn_ZDYCardMake.Size = new System.Drawing.Size(169, 35);
            this.btn_ZDYCardMake.TabIndex = 1;
            this.btn_ZDYCardMake.Text = "自定义卡密生成";
            this.btn_ZDYCardMake.UseVisualStyleBackColor = true;
            this.btn_ZDYCardMake.Click += new System.EventHandler(this.btn_ZDYCardMake_Click);
            // 
            // btn_卡密生成
            // 
            this.btn_卡密生成.Location = new System.Drawing.Point(21, 26);
            this.btn_卡密生成.Name = "btn_卡密生成";
            this.btn_卡密生成.Size = new System.Drawing.Size(116, 35);
            this.btn_卡密生成.TabIndex = 1;
            this.btn_卡密生成.Text = "卡密生成";
            this.btn_卡密生成.UseVisualStyleBackColor = true;
            this.btn_卡密生成.Click += new System.EventHandler(this.btn_卡密生成_Click);
            // 
            // num_生成数量
            // 
            this.num_生成数量.Location = new System.Drawing.Point(107, 127);
            this.num_生成数量.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.num_生成数量.Name = "num_生成数量";
            this.num_生成数量.Size = new System.Drawing.Size(78, 25);
            this.num_生成数量.TabIndex = 4;
            // 
            // tbx_TiKaQQ
            // 
            this.tbx_TiKaQQ.Location = new System.Drawing.Point(107, 93);
            this.tbx_TiKaQQ.Name = "tbx_TiKaQQ";
            this.tbx_TiKaQQ.Size = new System.Drawing.Size(154, 25);
            this.tbx_TiKaQQ.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 129);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "生成数量：";
            // 
            // cbx_DBBB
            // 
            this.cbx_DBBB.AutoSize = true;
            this.cbx_DBBB.Checked = true;
            this.cbx_DBBB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbx_DBBB.Location = new System.Drawing.Point(22, 168);
            this.cbx_DBBB.Name = "cbx_DBBB";
            this.cbx_DBBB.Size = new System.Drawing.Size(119, 19);
            this.cbx_DBBB.TabIndex = 7;
            this.cbx_DBBB.Text = "无修改数据库";
            this.cbx_DBBB.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "提卡QQ：";
            // 
            // gb_卡密设置区
            // 
            this.gb_卡密设置区.BackColor = System.Drawing.Color.Transparent;
            this.gb_卡密设置区.Controls.Add(this.label7);
            this.gb_卡密设置区.Controls.Add(this.label6);
            this.gb_卡密设置区.Controls.Add(this.label3);
            this.gb_卡密设置区.Controls.Add(this.cbx_DBBB);
            this.gb_卡密设置区.Controls.Add(this.label2);
            this.gb_卡密设置区.Controls.Add(this.tbx_前缀);
            this.gb_卡密设置区.Controls.Add(this.tbx_后缀);
            this.gb_卡密设置区.Controls.Add(this.tbx_TiKaQQ);
            this.gb_卡密设置区.Controls.Add(this.num_生成数量);
            this.gb_卡密设置区.Enabled = false;
            this.gb_卡密设置区.Location = new System.Drawing.Point(3, 85);
            this.gb_卡密设置区.Name = "gb_卡密设置区";
            this.gb_卡密设置区.Size = new System.Drawing.Size(352, 203);
            this.gb_卡密设置区.TabIndex = 8;
            this.gb_卡密设置区.TabStop = false;
            this.gb_卡密设置区.Text = "卡密设置区";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(19, 31);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 15);
            this.label7.TabIndex = 2;
            this.label7.Text = "前缀：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(19, 62);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 15);
            this.label6.TabIndex = 2;
            this.label6.Text = "后缀：";
            // 
            // tbx_前缀
            // 
            this.tbx_前缀.Location = new System.Drawing.Point(107, 28);
            this.tbx_前缀.Name = "tbx_前缀";
            this.tbx_前缀.Size = new System.Drawing.Size(154, 25);
            this.tbx_前缀.TabIndex = 5;
            // 
            // tbx_后缀
            // 
            this.tbx_后缀.Location = new System.Drawing.Point(107, 59);
            this.tbx_后缀.Name = "tbx_后缀";
            this.tbx_后缀.Size = new System.Drawing.Size(154, 25);
            this.tbx_后缀.TabIndex = 5;
            // 
            // gb_卡密区
            // 
            this.gb_卡密区.BackColor = System.Drawing.Color.White;
            this.gb_卡密区.Controls.Add(this.tb_卡密列表);
            this.gb_卡密区.Enabled = false;
            this.gb_卡密区.Location = new System.Drawing.Point(12, 28);
            this.gb_卡密区.Name = "gb_卡密区";
            this.gb_卡密区.Size = new System.Drawing.Size(302, 551);
            this.gb_卡密区.TabIndex = 9;
            this.gb_卡密区.TabStop = false;
            this.gb_卡密区.Text = "自定义卡密区";
            // 
            // cNS_YJMenu
            // 
            this.cNS_YJMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cNS_YJMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.选择数据库位置ToolStripMenuItem,
            this.查看更新日志ToolStripMenuItem});
            this.cNS_YJMenu.Name = "cNS_YJMenu";
            this.cNS_YJMenu.Size = new System.Drawing.Size(190, 56);
            // 
            // 选择数据库位置ToolStripMenuItem
            // 
            this.选择数据库位置ToolStripMenuItem.Name = "选择数据库位置ToolStripMenuItem";
            this.选择数据库位置ToolStripMenuItem.Size = new System.Drawing.Size(189, 26);
            this.选择数据库位置ToolStripMenuItem.Text = "选择数据库位置";
            this.选择数据库位置ToolStripMenuItem.Click += new System.EventHandler(this.选择数据库位置ToolStripMenuItem_Click);
            // 
            // 查看更新日志ToolStripMenuItem
            // 
            this.查看更新日志ToolStripMenuItem.Name = "查看更新日志ToolStripMenuItem";
            this.查看更新日志ToolStripMenuItem.Size = new System.Drawing.Size(189, 26);
            this.查看更新日志ToolStripMenuItem.Text = "查看更新日志";
            this.查看更新日志ToolStripMenuItem.Visible = false;
            this.查看更新日志ToolStripMenuItem.Click += new System.EventHandler(this.查看更新日志ToolStripMenuItem_Click);
            // 
            // lb_Error
            // 
            this.lb_Error.AutoSize = true;
            this.lb_Error.ForeColor = System.Drawing.Color.Red;
            this.lb_Error.Location = new System.Drawing.Point(39, 9);
            this.lb_Error.Name = "lb_Error";
            this.lb_Error.Size = new System.Drawing.Size(0, 15);
            this.lb_Error.TabIndex = 11;
            // 
            // gb_卡密时间设置区
            // 
            this.gb_卡密时间设置区.BackColor = System.Drawing.Color.Transparent;
            this.gb_卡密时间设置区.Controls.Add(this.cb_时间列表);
            this.gb_卡密时间设置区.Controls.Add(this.label4);
            this.gb_卡密时间设置区.Location = new System.Drawing.Point(3, 3);
            this.gb_卡密时间设置区.Name = "gb_卡密时间设置区";
            this.gb_卡密时间设置区.Size = new System.Drawing.Size(352, 66);
            this.gb_卡密时间设置区.TabIndex = 12;
            this.gb_卡密时间设置区.TabStop = false;
            this.gb_卡密时间设置区.Text = "卡密时间设置区";
            // 
            // cb_时间列表
            // 
            this.cb_时间列表.FormattingEnabled = true;
            this.cb_时间列表.Items.AddRange(new object[] {
            "1小时",
            "3小时",
            "10小时",
            "天卡",
            "周卡",
            "月卡",
            "季卡",
            "年卡",
            "永久卡"});
            this.cb_时间列表.Location = new System.Drawing.Point(101, 29);
            this.cb_时间列表.Name = "cb_时间列表";
            this.cb_时间列表.Size = new System.Drawing.Size(160, 23);
            this.cb_时间列表.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 15);
            this.label4.TabIndex = 14;
            this.label4.Text = "卡密时间：";
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel1.Controls.Add(this.gb_卡密时间设置区);
            this.panel1.Controls.Add(this.gb_卡密设置区);
            this.panel1.Controls.Add(this.gb_常用区);
            this.panel1.Controls.Add(this.gb_慎用区);
            this.panel1.Location = new System.Drawing.Point(320, 31);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(368, 548);
            this.panel1.TabIndex = 13;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // CardMake
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(697, 588);
            this.ContextMenuStrip = this.cNS_YJMenu;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lb_Error);
            this.Controls.Add(this.gb_卡密区);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "CardMake";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "卡密生成器  ";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.gb_慎用区.ResumeLayout(false);
            this.gb_常用区.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.num_生成数量)).EndInit();
            this.gb_卡密设置区.ResumeLayout(false);
            this.gb_卡密设置区.PerformLayout();
            this.gb_卡密区.ResumeLayout(false);
            this.gb_卡密区.PerformLayout();
            this.cNS_YJMenu.ResumeLayout(false);
            this.gb_卡密时间设置区.ResumeLayout(false);
            this.gb_卡密时间设置区.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox tb_卡密列表;
        private System.Windows.Forms.Button btn_deleteAllCard;
        private System.Windows.Forms.GroupBox gb_慎用区;
        private System.Windows.Forms.GroupBox gb_常用区;
        private System.Windows.Forms.NumericUpDown num_生成数量;
        private System.Windows.Forms.TextBox tbx_TiKaQQ;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cbx_DBBB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox gb_卡密设置区;
        private System.Windows.Forms.Button btn_卡密生成;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbx_前缀;
        private System.Windows.Forms.TextBox tbx_后缀;
        private System.Windows.Forms.GroupBox gb_卡密区;
        private System.Windows.Forms.Button btn_DelCard;
        private System.Windows.Forms.ContextMenuStrip cNS_YJMenu;
        private System.Windows.Forms.ToolStripMenuItem 选择数据库位置ToolStripMenuItem;
        private System.Windows.Forms.Label lb_Error;
        private System.Windows.Forms.Button btn_ZDYCardMake;
        private System.Windows.Forms.GroupBox gb_卡密时间设置区;
        private System.Windows.Forms.ComboBox cb_时间列表;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem 查看更新日志ToolStripMenuItem;
    }
}

