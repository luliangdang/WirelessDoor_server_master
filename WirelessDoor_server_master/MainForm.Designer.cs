namespace WirelessDoor_server_master
{
    partial class MainForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.开启服务器ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.会议室管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.会议室管理ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rtMessage = new System.Windows.Forms.RichTextBox();
            this.roomName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.netState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.roomState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.userNmane = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.startTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.endTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.reason = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lbOnline = new System.Windows.Forms.ListBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.开启服务器ToolStripMenuItem,
            this.会议室管理ToolStripMenuItem,
            this.会议室管理ToolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(673, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 开启服务器ToolStripMenuItem
            // 
            this.开启服务器ToolStripMenuItem.Name = "开启服务器ToolStripMenuItem";
            this.开启服务器ToolStripMenuItem.Size = new System.Drawing.Size(80, 21);
            this.开启服务器ToolStripMenuItem.Text = "开启服务器";
            this.开启服务器ToolStripMenuItem.Click += new System.EventHandler(this.开启服务器ToolStripMenuItem_Click);
            // 
            // 会议室管理ToolStripMenuItem
            // 
            this.会议室管理ToolStripMenuItem.Name = "会议室管理ToolStripMenuItem";
            this.会议室管理ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.会议室管理ToolStripMenuItem.Text = "人员管理";
            this.会议室管理ToolStripMenuItem.Click += new System.EventHandler(this.人员管理ToolStripMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dataGridView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Size = new System.Drawing.Size(673, 409);
            this.splitContainer1.SplitterDistance = 193;
            this.splitContainer1.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.roomName,
            this.netState,
            this.roomState,
            this.userNmane,
            this.startTime,
            this.endTime,
            this.reason});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(673, 193);
            this.dataGridView1.TabIndex = 0;
            // 
            // 会议室管理ToolStripMenuItem1
            // 
            this.会议室管理ToolStripMenuItem1.Name = "会议室管理ToolStripMenuItem1";
            this.会议室管理ToolStripMenuItem1.Size = new System.Drawing.Size(80, 21);
            this.会议室管理ToolStripMenuItem1.Text = "会议室管理";
            this.会议室管理ToolStripMenuItem1.Click += new System.EventHandler(this.会议室管理ToolStripMenuItem1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rtMessage);
            this.groupBox1.Location = new System.Drawing.Point(461, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 193);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "日志";
            // 
            // rtMessage
            // 
            this.rtMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtMessage.Location = new System.Drawing.Point(3, 17);
            this.rtMessage.Name = "rtMessage";
            this.rtMessage.Size = new System.Drawing.Size(194, 173);
            this.rtMessage.TabIndex = 0;
            this.rtMessage.Text = "";
            // 
            // roomName
            // 
            this.roomName.HeaderText = "会议室名称";
            this.roomName.Name = "roomName";
            this.roomName.ReadOnly = true;
            // 
            // netState
            // 
            this.netState.HeaderText = "在线状态";
            this.netState.Name = "netState";
            this.netState.ReadOnly = true;
            // 
            // roomState
            // 
            this.roomState.HeaderText = "状态";
            this.roomState.Name = "roomState";
            this.roomState.ReadOnly = true;
            // 
            // userNmane
            // 
            this.userNmane.HeaderText = "预约人";
            this.userNmane.Name = "userNmane";
            this.userNmane.ReadOnly = true;
            // 
            // startTime
            // 
            this.startTime.HeaderText = "开始时间";
            this.startTime.Name = "startTime";
            this.startTime.ReadOnly = true;
            // 
            // endTime
            // 
            this.endTime.HeaderText = "结束时间";
            this.endTime.Name = "endTime";
            this.endTime.ReadOnly = true;
            // 
            // reason
            // 
            this.reason.HeaderText = "申请理由";
            this.reason.Name = "reason";
            this.reason.ReadOnly = true;
            // 
            // lbOnline
            // 
            this.lbOnline.FormattingEnabled = true;
            this.lbOnline.ItemHeight = 12;
            this.lbOnline.Location = new System.Drawing.Point(541, 3);
            this.lbOnline.Name = "lbOnline";
            this.lbOnline.Size = new System.Drawing.Size(120, 16);
            this.lbOnline.TabIndex = 1;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(673, 434);
            this.Controls.Add(this.lbOnline);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "智能会议室预约系统";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 开启服务器ToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ToolStripMenuItem 会议室管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 会议室管理ToolStripMenuItem1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox rtMessage;
        private System.Windows.Forms.DataGridViewTextBoxColumn roomName;
        private System.Windows.Forms.DataGridViewTextBoxColumn netState;
        private System.Windows.Forms.DataGridViewTextBoxColumn roomState;
        private System.Windows.Forms.DataGridViewTextBoxColumn userNmane;
        private System.Windows.Forms.DataGridViewTextBoxColumn startTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn endTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn reason;
        private System.Windows.Forms.ListBox lbOnline;
    }
}

