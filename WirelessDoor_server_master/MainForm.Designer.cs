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
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.开启服务器ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.会议室管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.会议室管理ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.刷新申请信息ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.清除显示日志ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.刷新状态ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.dgvReservation = new System.Windows.Forms.DataGridView();
            this.roomName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.netState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.userNmane = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.startTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.finalTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.reason = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.rtReason = new System.Windows.Forms.RichTextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.dgvRoom = new System.Windows.Forms.DataGridView();
            this.roomID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.roomState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.netaddres = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rtMessage = new System.Windows.Forms.RichTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btCancel = new System.Windows.Forms.Button();
            this.btAgree = new System.Windows.Forms.Button();
            this.tbEndTime = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbBeginTime = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbUserName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbRoomName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rtLogo = new System.Windows.Forms.RichTextBox();
            this.lbOnline = new System.Windows.Forms.ListBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.cmsdgvRoom = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.刷新表格信息ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.刷新状态ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReservation)).BeginInit();
            this.groupBox6.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRoom)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.cmsdgvRoom.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.开启服务器ToolStripMenuItem,
            this.会议室管理ToolStripMenuItem,
            this.会议室管理ToolStripMenuItem1,
            this.刷新申请信息ToolStripMenuItem,
            this.清除显示日志ToolStripMenuItem,
            this.刷新状态ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(762, 25);
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
            // 会议室管理ToolStripMenuItem1
            // 
            this.会议室管理ToolStripMenuItem1.Name = "会议室管理ToolStripMenuItem1";
            this.会议室管理ToolStripMenuItem1.Size = new System.Drawing.Size(80, 21);
            this.会议室管理ToolStripMenuItem1.Text = "会议室管理";
            this.会议室管理ToolStripMenuItem1.Click += new System.EventHandler(this.会议室管理ToolStripMenuItem1_Click);
            // 
            // 刷新申请信息ToolStripMenuItem
            // 
            this.刷新申请信息ToolStripMenuItem.Name = "刷新申请信息ToolStripMenuItem";
            this.刷新申请信息ToolStripMenuItem.Size = new System.Drawing.Size(92, 21);
            this.刷新申请信息ToolStripMenuItem.Text = "刷新表格信息";
            this.刷新申请信息ToolStripMenuItem.Click += new System.EventHandler(this.刷新表格信息ToolStripMenuItem_Click);
            // 
            // 清除显示日志ToolStripMenuItem
            // 
            this.清除显示日志ToolStripMenuItem.Name = "清除显示日志ToolStripMenuItem";
            this.清除显示日志ToolStripMenuItem.Size = new System.Drawing.Size(92, 21);
            this.清除显示日志ToolStripMenuItem.Text = "清除显示日志";
            this.清除显示日志ToolStripMenuItem.Click += new System.EventHandler(this.清除显示日志ToolStripMenuItem_Click);
            // 
            // 刷新状态ToolStripMenuItem
            // 
            this.刷新状态ToolStripMenuItem.Name = "刷新状态ToolStripMenuItem";
            this.刷新状态ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.刷新状态ToolStripMenuItem.Text = "刷新状态";
            this.刷新状态ToolStripMenuItem.Click += new System.EventHandler(this.刷新状态ToolStripMenuItem_Click);
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
            this.splitContainer1.Panel1.Controls.Add(this.groupBox5);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox6);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox4);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox3);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Size = new System.Drawing.Size(762, 587);
            this.splitContainer1.SplitterDistance = 234;
            this.splitContainer1.TabIndex = 1;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.dgvReservation);
            this.groupBox5.Location = new System.Drawing.Point(4, 3);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(752, 228);
            this.groupBox5.TabIndex = 0;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "预约申请信息";
            // 
            // dgvReservation
            // 
            this.dgvReservation.AllowUserToAddRows = false;
            this.dgvReservation.AllowUserToDeleteRows = false;
            this.dgvReservation.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvReservation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReservation.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.roomName,
            this.netState,
            this.userNmane,
            this.startTime,
            this.finalTime,
            this.reason});
            this.dgvReservation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvReservation.Location = new System.Drawing.Point(3, 17);
            this.dgvReservation.Name = "dgvReservation";
            this.dgvReservation.ReadOnly = true;
            this.dgvReservation.RowTemplate.Height = 23;
            this.dgvReservation.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvReservation.Size = new System.Drawing.Size(746, 208);
            this.dgvReservation.TabIndex = 1;
            this.dgvReservation.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvReservation_CellDoubleClick);
            // 
            // roomName
            // 
            this.roomName.HeaderText = "会议室名称";
            this.roomName.Name = "roomName";
            this.roomName.ReadOnly = true;
            // 
            // netState
            // 
            this.netState.HeaderText = "处理状态";
            this.netState.Name = "netState";
            this.netState.ReadOnly = true;
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
            // finalTime
            // 
            this.finalTime.HeaderText = "结束时间";
            this.finalTime.Name = "finalTime";
            this.finalTime.ReadOnly = true;
            // 
            // reason
            // 
            this.reason.HeaderText = "申请理由";
            this.reason.Name = "reason";
            this.reason.ReadOnly = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.rtReason);
            this.groupBox6.Location = new System.Drawing.Point(630, 7);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(117, 100);
            this.groupBox6.TabIndex = 4;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "申请理由";
            // 
            // rtReason
            // 
            this.rtReason.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtReason.Location = new System.Drawing.Point(3, 17);
            this.rtReason.Name = "rtReason";
            this.rtReason.ReadOnly = true;
            this.rtReason.Size = new System.Drawing.Size(111, 80);
            this.rtReason.TabIndex = 11;
            this.rtReason.Text = "";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.dgvRoom);
            this.groupBox4.Location = new System.Drawing.Point(4, 7);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(416, 336);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "会议室信息";
            // 
            // dgvRoom
            // 
            this.dgvRoom.AllowUserToAddRows = false;
            this.dgvRoom.AllowUserToDeleteRows = false;
            this.dgvRoom.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRoom.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRoom.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.roomID,
            this.dataGridViewTextBoxColumn1,
            this.roomState,
            this.dataGridViewTextBoxColumn2,
            this.netaddres});
            this.dgvRoom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRoom.Location = new System.Drawing.Point(3, 17);
            this.dgvRoom.Name = "dgvRoom";
            this.dgvRoom.ReadOnly = true;
            this.dgvRoom.RowTemplate.Height = 23;
            this.dgvRoom.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRoom.Size = new System.Drawing.Size(410, 316);
            this.dgvRoom.TabIndex = 2;
            this.dgvRoom.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvRoom_CellMouseDown);
            // 
            // roomID
            // 
            this.roomID.HeaderText = "编号";
            this.roomID.Name = "roomID";
            this.roomID.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "会议室名称";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // roomState
            // 
            this.roomState.HeaderText = "状态";
            this.roomState.Name = "roomState";
            this.roomState.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "网络";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // netaddres
            // 
            this.netaddres.HeaderText = "IP";
            this.netaddres.Name = "netaddres";
            this.netaddres.ReadOnly = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rtMessage);
            this.groupBox3.Location = new System.Drawing.Point(633, 108);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(117, 92);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "处理意见";
            // 
            // rtMessage
            // 
            this.rtMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtMessage.Location = new System.Drawing.Point(3, 17);
            this.rtMessage.Name = "rtMessage";
            this.rtMessage.Size = new System.Drawing.Size(111, 72);
            this.rtMessage.TabIndex = 0;
            this.rtMessage.Text = "";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btCancel);
            this.groupBox2.Controls.Add(this.btAgree);
            this.groupBox2.Controls.Add(this.tbEndTime);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.tbBeginTime);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.tbUserName);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.tbRoomName);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(426, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(198, 196);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "预约信息";
            // 
            // btCancel
            // 
            this.btCancel.Location = new System.Drawing.Point(113, 77);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 9;
            this.btCancel.Text = "拒绝预约";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // btAgree
            // 
            this.btAgree.Location = new System.Drawing.Point(113, 33);
            this.btAgree.Name = "btAgree";
            this.btAgree.Size = new System.Drawing.Size(75, 23);
            this.btAgree.TabIndex = 8;
            this.btAgree.Text = "同意预约";
            this.btAgree.UseVisualStyleBackColor = true;
            this.btAgree.Click += new System.EventHandler(this.btAgree_Click);
            // 
            // tbEndTime
            // 
            this.tbEndTime.Location = new System.Drawing.Point(7, 159);
            this.tbEndTime.Name = "tbEndTime";
            this.tbEndTime.ReadOnly = true;
            this.tbEndTime.Size = new System.Drawing.Size(100, 21);
            this.tbEndTime.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 144);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "结束时间";
            // 
            // tbBeginTime
            // 
            this.tbBeginTime.Location = new System.Drawing.Point(7, 120);
            this.tbBeginTime.Name = "tbBeginTime";
            this.tbBeginTime.ReadOnly = true;
            this.tbBeginTime.Size = new System.Drawing.Size(100, 21);
            this.tbBeginTime.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "开始时间";
            // 
            // tbUserName
            // 
            this.tbUserName.Location = new System.Drawing.Point(7, 77);
            this.tbUserName.Name = "tbUserName";
            this.tbUserName.ReadOnly = true;
            this.tbUserName.Size = new System.Drawing.Size(100, 21);
            this.tbUserName.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "预约人";
            // 
            // tbRoomName
            // 
            this.tbRoomName.Location = new System.Drawing.Point(7, 33);
            this.tbRoomName.Name = "tbRoomName";
            this.tbRoomName.ReadOnly = true;
            this.tbRoomName.Size = new System.Drawing.Size(100, 21);
            this.tbRoomName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "预约会议室";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rtLogo);
            this.groupBox1.Location = new System.Drawing.Point(426, 203);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(333, 143);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "日志";
            // 
            // rtLogo
            // 
            this.rtLogo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtLogo.Location = new System.Drawing.Point(3, 17);
            this.rtLogo.Name = "rtLogo";
            this.rtLogo.ReadOnly = true;
            this.rtLogo.Size = new System.Drawing.Size(327, 123);
            this.rtLogo.TabIndex = 0;
            this.rtLogo.Text = "";
            // 
            // lbOnline
            // 
            this.lbOnline.FormattingEnabled = true;
            this.lbOnline.ItemHeight = 12;
            this.lbOnline.Location = new System.Drawing.Point(630, 3);
            this.lbOnline.Name = "lbOnline";
            this.lbOnline.Size = new System.Drawing.Size(120, 16);
            this.lbOnline.TabIndex = 1;
            // 
            // timer1
            // 
            this.timer1.Interval = 600000;
            // 
            // cmsdgvRoom
            // 
            this.cmsdgvRoom.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.刷新表格信息ToolStripMenuItem,
            this.刷新状态ToolStripMenuItem1});
            this.cmsdgvRoom.Name = "cmsdgvRoom";
            this.cmsdgvRoom.Size = new System.Drawing.Size(149, 48);
            // 
            // 刷新表格信息ToolStripMenuItem
            // 
            this.刷新表格信息ToolStripMenuItem.Name = "刷新表格信息ToolStripMenuItem";
            this.刷新表格信息ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.刷新表格信息ToolStripMenuItem.Text = "刷新表格信息";
            this.刷新表格信息ToolStripMenuItem.Click += new System.EventHandler(this.刷新表格信息ToolStripMenuItem_Click);
            // 
            // 刷新状态ToolStripMenuItem1
            // 
            this.刷新状态ToolStripMenuItem1.Name = "刷新状态ToolStripMenuItem1";
            this.刷新状态ToolStripMenuItem1.Size = new System.Drawing.Size(148, 22);
            this.刷新状态ToolStripMenuItem1.Text = "刷新状态";
            this.刷新状态ToolStripMenuItem1.Click += new System.EventHandler(this.刷新状态ToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(762, 612);
            this.Controls.Add(this.lbOnline);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "智能会议室预约系统";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReservation)).EndInit();
            this.groupBox6.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRoom)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.cmsdgvRoom.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 开启服务器ToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripMenuItem 会议室管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 会议室管理ToolStripMenuItem1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox rtLogo;
        private System.Windows.Forms.ListBox lbOnline;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RichTextBox rtMessage;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tbEndTime;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbBeginTime;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbUserName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbRoomName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btAgree;
        private System.Windows.Forms.ToolStripMenuItem 刷新申请信息ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 清除显示日志ToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.DataGridView dgvReservation;
        private System.Windows.Forms.DataGridViewTextBoxColumn roomName;
        private System.Windows.Forms.DataGridViewTextBoxColumn netState;
        private System.Windows.Forms.DataGridViewTextBoxColumn userNmane;
        private System.Windows.Forms.DataGridViewTextBoxColumn startTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn finalTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn reason;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DataGridView dgvRoom;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem 刷新状态ToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip cmsdgvRoom;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.RichTextBox rtReason;
        private System.Windows.Forms.DataGridViewTextBoxColumn roomID;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn roomState;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn netaddres;
        private System.Windows.Forms.ToolStripMenuItem 刷新表格信息ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 刷新状态ToolStripMenuItem1;
    }
}

