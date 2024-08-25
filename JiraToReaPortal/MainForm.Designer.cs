namespace JiraToReaPortal
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            lblGithub = new LinkLabel();
            grbJira = new GroupBox();
            btnJiraLogin = new Button();
            chkJiraRememberMe = new CheckBox();
            txtJiraAPIToken = new TextBox();
            lblJiraAPIToken = new Label();
            txtJiraUsername = new TextBox();
            lblJiraUsername = new Label();
            grbRea = new GroupBox();
            cmbProjectName = new ComboBox();
            lblProjectName = new Label();
            btnReaLogin = new Button();
            chkReaRememberMe = new CheckBox();
            txtReaPassword = new TextBox();
            lblReaPassword = new Label();
            txtReaUsername = new TextBox();
            lblReaUsername = new Label();
            grbWorklog = new GroupBox();
            lblSelectedRowCount = new Label();
            btnImport = new Button();
            dtpEndDate = new DateTimePicker();
            dgWorklog = new DataGridView();
            ColumnSelected = new DataGridViewCheckBoxColumn();
            Key = new DataGridViewTextBoxColumn();
            Summary = new DataGridViewTextBoxColumn();
            Comment = new DataGridViewTextBoxColumn();
            StartDate = new DataGridViewTextBoxColumn();
            EndDate = new DataGridViewTextBoxColumn();
            TimeSpent = new DataGridViewTextBoxColumn();
            TimeSpentHours = new DataGridViewTextBoxColumn();
            btnFind = new Button();
            dtpStartTime = new DateTimePicker();
            lblEndDate = new Label();
            lblStartDate = new Label();
            grbJira.SuspendLayout();
            grbRea.SuspendLayout();
            grbWorklog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgWorklog).BeginInit();
            SuspendLayout();
            // 
            // lblGithub
            // 
            lblGithub.ActiveLinkColor = Color.SlateGray;
            lblGithub.AutoSize = true;
            lblGithub.LinkArea = new LinkArea(7, 14);
            lblGithub.LinkBehavior = LinkBehavior.NeverUnderline;
            lblGithub.LinkColor = Color.Black;
            lblGithub.Location = new Point(791, 367);
            lblGithub.Name = "lblGithub";
            lblGithub.Size = new Size(0, 18);
            lblGithub.TabIndex = 0;
            lblGithub.UseCompatibleTextRendering = true;
            lblGithub.VisitedLinkColor = Color.Black;
            lblGithub.LinkClicked += lblGithub_LinkClicked;
            // 
            // grbJira
            // 
            grbJira.Controls.Add(btnJiraLogin);
            grbJira.Controls.Add(chkJiraRememberMe);
            grbJira.Controls.Add(txtJiraAPIToken);
            grbJira.Controls.Add(lblJiraAPIToken);
            grbJira.Controls.Add(txtJiraUsername);
            grbJira.Controls.Add(lblJiraUsername);
            grbJira.Location = new Point(12, 211);
            grbJira.Name = "grbJira";
            grbJira.Size = new Size(302, 153);
            grbJira.TabIndex = 2;
            grbJira.TabStop = false;
            grbJira.Text = "Jira";
            // 
            // btnJiraLogin
            // 
            btnJiraLogin.Image = Properties.Resources.login;
            btnJiraLogin.Location = new Point(140, 103);
            btnJiraLogin.Name = "btnJiraLogin";
            btnJiraLogin.Size = new Size(125, 35);
            btnJiraLogin.TabIndex = 9;
            btnJiraLogin.Text = "Login";
            btnJiraLogin.TextAlign = ContentAlignment.MiddleRight;
            btnJiraLogin.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnJiraLogin.UseVisualStyleBackColor = true;
            btnJiraLogin.Click += btnJiraLogin_Click;
            // 
            // chkJiraRememberMe
            // 
            chkJiraRememberMe.AutoSize = true;
            chkJiraRememberMe.Location = new Point(30, 112);
            chkJiraRememberMe.Name = "chkJiraRememberMe";
            chkJiraRememberMe.Size = new Size(104, 19);
            chkJiraRememberMe.TabIndex = 8;
            chkJiraRememberMe.Text = "Remember Me";
            chkJiraRememberMe.UseVisualStyleBackColor = true;
            // 
            // txtJiraAPIToken
            // 
            txtJiraAPIToken.Location = new Point(104, 65);
            txtJiraAPIToken.Name = "txtJiraAPIToken";
            txtJiraAPIToken.Size = new Size(179, 23);
            txtJiraAPIToken.TabIndex = 7;
            txtJiraAPIToken.Enter += txtJiraAPIToken_Enter;
            txtJiraAPIToken.Leave += txtJiraAPIToken_Leave;
            // 
            // lblJiraAPIToken
            // 
            lblJiraAPIToken.AutoSize = true;
            lblJiraAPIToken.Location = new Point(16, 68);
            lblJiraAPIToken.Name = "lblJiraAPIToken";
            lblJiraAPIToken.Size = new Size(59, 15);
            lblJiraAPIToken.TabIndex = 0;
            lblJiraAPIToken.Text = "API Token";
            // 
            // txtJiraUsername
            // 
            txtJiraUsername.Location = new Point(104, 27);
            txtJiraUsername.Name = "txtJiraUsername";
            txtJiraUsername.Size = new Size(179, 23);
            txtJiraUsername.TabIndex = 6;
            // 
            // lblJiraUsername
            // 
            lblJiraUsername.AutoSize = true;
            lblJiraUsername.Location = new Point(16, 30);
            lblJiraUsername.Name = "lblJiraUsername";
            lblJiraUsername.Size = new Size(60, 15);
            lblJiraUsername.TabIndex = 0;
            lblJiraUsername.Text = "Username";
            // 
            // grbRea
            // 
            grbRea.Controls.Add(cmbProjectName);
            grbRea.Controls.Add(lblProjectName);
            grbRea.Controls.Add(btnReaLogin);
            grbRea.Controls.Add(chkReaRememberMe);
            grbRea.Controls.Add(txtReaPassword);
            grbRea.Controls.Add(lblReaPassword);
            grbRea.Controls.Add(txtReaUsername);
            grbRea.Controls.Add(lblReaUsername);
            grbRea.Location = new Point(12, 12);
            grbRea.Name = "grbRea";
            grbRea.Size = new Size(302, 193);
            grbRea.TabIndex = 1;
            grbRea.TabStop = false;
            grbRea.Text = "Rea Portal";
            // 
            // cmbProjectName
            // 
            cmbProjectName.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbProjectName.Enabled = false;
            cmbProjectName.FormattingEnabled = true;
            cmbProjectName.Location = new Point(104, 155);
            cmbProjectName.Name = "cmbProjectName";
            cmbProjectName.Size = new Size(179, 23);
            cmbProjectName.TabIndex = 5;
            // 
            // lblProjectName
            // 
            lblProjectName.AutoSize = true;
            lblProjectName.Enabled = false;
            lblProjectName.Location = new Point(16, 158);
            lblProjectName.Name = "lblProjectName";
            lblProjectName.Size = new Size(79, 15);
            lblProjectName.TabIndex = 0;
            lblProjectName.Text = "Project Name";
            // 
            // btnReaLogin
            // 
            btnReaLogin.Image = Properties.Resources.login;
            btnReaLogin.Location = new Point(140, 105);
            btnReaLogin.Name = "btnReaLogin";
            btnReaLogin.Size = new Size(125, 35);
            btnReaLogin.TabIndex = 4;
            btnReaLogin.Text = "Login";
            btnReaLogin.TextAlign = ContentAlignment.MiddleRight;
            btnReaLogin.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnReaLogin.UseVisualStyleBackColor = true;
            btnReaLogin.Click += btnReaLogin_Click;
            // 
            // chkReaRememberMe
            // 
            chkReaRememberMe.AutoSize = true;
            chkReaRememberMe.Location = new Point(30, 114);
            chkReaRememberMe.Name = "chkReaRememberMe";
            chkReaRememberMe.Size = new Size(104, 19);
            chkReaRememberMe.TabIndex = 3;
            chkReaRememberMe.Text = "Remember Me";
            chkReaRememberMe.UseVisualStyleBackColor = true;
            // 
            // txtReaPassword
            // 
            txtReaPassword.Location = new Point(104, 66);
            txtReaPassword.Name = "txtReaPassword";
            txtReaPassword.Size = new Size(179, 23);
            txtReaPassword.TabIndex = 2;
            txtReaPassword.Enter += txtReaPassword_Enter;
            txtReaPassword.Leave += txtReaPassword_Leave;
            // 
            // lblReaPassword
            // 
            lblReaPassword.AutoSize = true;
            lblReaPassword.Location = new Point(16, 69);
            lblReaPassword.Name = "lblReaPassword";
            lblReaPassword.Size = new Size(57, 15);
            lblReaPassword.TabIndex = 0;
            lblReaPassword.Text = "Password";
            // 
            // txtReaUsername
            // 
            txtReaUsername.Location = new Point(104, 27);
            txtReaUsername.Name = "txtReaUsername";
            txtReaUsername.Size = new Size(179, 23);
            txtReaUsername.TabIndex = 1;
            // 
            // lblReaUsername
            // 
            lblReaUsername.AutoSize = true;
            lblReaUsername.Location = new Point(16, 30);
            lblReaUsername.Name = "lblReaUsername";
            lblReaUsername.Size = new Size(60, 15);
            lblReaUsername.TabIndex = 0;
            lblReaUsername.Text = "Username";
            // 
            // grbWorklog
            // 
            grbWorklog.Controls.Add(lblSelectedRowCount);
            grbWorklog.Controls.Add(btnImport);
            grbWorklog.Controls.Add(dtpEndDate);
            grbWorklog.Controls.Add(dgWorklog);
            grbWorklog.Controls.Add(btnFind);
            grbWorklog.Controls.Add(dtpStartTime);
            grbWorklog.Controls.Add(lblEndDate);
            grbWorklog.Controls.Add(lblStartDate);
            grbWorklog.Enabled = false;
            grbWorklog.Location = new Point(320, 12);
            grbWorklog.Name = "grbWorklog";
            grbWorklog.Size = new Size(705, 352);
            grbWorklog.TabIndex = 3;
            grbWorklog.TabStop = false;
            grbWorklog.Text = "Jira Worklog";
            // 
            // lblSelectedRowCount
            // 
            lblSelectedRowCount.AutoSize = true;
            lblSelectedRowCount.Location = new Point(15, 322);
            lblSelectedRowCount.Name = "lblSelectedRowCount";
            lblSelectedRowCount.Size = new Size(128, 15);
            lblSelectedRowCount.TabIndex = 0;
            lblSelectedRowCount.Text = "Selected rows count:  0";
            // 
            // btnImport
            // 
            btnImport.Enabled = false;
            btnImport.Image = Properties.Resources.import;
            btnImport.Location = new Point(258, 311);
            btnImport.Name = "btnImport";
            btnImport.Size = new Size(193, 35);
            btnImport.TabIndex = 14;
            btnImport.Text = "Import To Rea Portal";
            btnImport.TextAlign = ContentAlignment.MiddleRight;
            btnImport.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnImport.UseVisualStyleBackColor = true;
            btnImport.Click += btnImport_Click;
            // 
            // dtpEndDate
            // 
            dtpEndDate.CustomFormat = "yyyy-MM-dd";
            dtpEndDate.Format = DateTimePickerFormat.Custom;
            dtpEndDate.Location = new Point(285, 28);
            dtpEndDate.Name = "dtpEndDate";
            dtpEndDate.Size = new Size(109, 23);
            dtpEndDate.TabIndex = 11;
            // 
            // dgWorklog
            // 
            dgWorklog.AllowUserToAddRows = false;
            dgWorklog.AllowUserToDeleteRows = false;
            dgWorklog.AllowUserToResizeRows = false;
            dgWorklog.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgWorklog.Columns.AddRange(new DataGridViewColumn[] { ColumnSelected, Key, Summary, Comment, StartDate, EndDate, TimeSpent, TimeSpentHours });
            dgWorklog.Enabled = false;
            dgWorklog.Location = new Point(15, 57);
            dgWorklog.Name = "dgWorklog";
            dgWorklog.RowHeadersVisible = false;
            dgWorklog.Size = new Size(674, 248);
            dgWorklog.TabIndex = 13;
            dgWorklog.CellValueChanged += dgWorklog_CellValueChanged;
            dgWorklog.CurrentCellDirtyStateChanged += dgWorklog_CurrentCellDirtyStateChanged;
            // 
            // ColumnSelected
            // 
            ColumnSelected.HeaderText = "";
            ColumnSelected.Name = "ColumnSelected";
            ColumnSelected.Resizable = DataGridViewTriState.False;
            ColumnSelected.Width = 30;
            // 
            // Key
            // 
            Key.HeaderText = "Key";
            Key.Name = "Key";
            Key.ReadOnly = true;
            Key.Resizable = DataGridViewTriState.False;
            // 
            // Summary
            // 
            Summary.HeaderText = "Name";
            Summary.Name = "Summary";
            Summary.ReadOnly = true;
            Summary.Width = 423;
            // 
            // Comment
            // 
            Comment.HeaderText = "Comment";
            Comment.Name = "Comment";
            Comment.Visible = false;
            // 
            // StartDate
            // 
            StartDate.HeaderText = "StartDate";
            StartDate.Name = "StartDate";
            StartDate.Visible = false;
            // 
            // EndDate
            // 
            EndDate.HeaderText = "EndDate";
            EndDate.Name = "EndDate";
            EndDate.Visible = false;
            // 
            // TimeSpent
            // 
            TimeSpent.HeaderText = "TimeSpent";
            TimeSpent.Name = "TimeSpent";
            TimeSpent.ReadOnly = true;
            TimeSpent.Resizable = DataGridViewTriState.False;
            // 
            // TimeSpentHours
            // 
            TimeSpentHours.HeaderText = "TimeSpent (H)";
            TimeSpentHours.Name = "TimeSpentHours";
            TimeSpentHours.Visible = false;
            // 
            // btnFind
            // 
            btnFind.Image = Properties.Resources.find;
            btnFind.Location = new Point(614, 23);
            btnFind.Name = "btnFind";
            btnFind.Size = new Size(75, 29);
            btnFind.TabIndex = 12;
            btnFind.Text = "Find";
            btnFind.TextAlign = ContentAlignment.MiddleRight;
            btnFind.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnFind.UseVisualStyleBackColor = true;
            btnFind.Click += btnFind_Click;
            // 
            // dtpStartTime
            // 
            dtpStartTime.CustomFormat = "yyyy-MM-dd";
            dtpStartTime.Format = DateTimePickerFormat.Custom;
            dtpStartTime.Location = new Point(91, 27);
            dtpStartTime.Name = "dtpStartTime";
            dtpStartTime.Size = new Size(109, 23);
            dtpStartTime.TabIndex = 10;
            // 
            // lblEndDate
            // 
            lblEndDate.AutoSize = true;
            lblEndDate.Location = new Point(216, 31);
            lblEndDate.Name = "lblEndDate";
            lblEndDate.Size = new Size(54, 15);
            lblEndDate.TabIndex = 0;
            lblEndDate.Text = "End Date";
            // 
            // lblStartDate
            // 
            lblStartDate.AutoSize = true;
            lblStartDate.Location = new Point(15, 30);
            lblStartDate.Name = "lblStartDate";
            lblStartDate.Size = new Size(58, 15);
            lblStartDate.TabIndex = 0;
            lblStartDate.Text = "Start Date";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1036, 388);
            Controls.Add(grbWorklog);
            Controls.Add(grbRea);
            Controls.Add(grbJira);
            Controls.Add(lblGithub);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Jira To Rea Portal";
            grbJira.ResumeLayout(false);
            grbJira.PerformLayout();
            grbRea.ResumeLayout(false);
            grbRea.PerformLayout();
            grbWorklog.ResumeLayout(false);
            grbWorklog.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgWorklog).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private LinkLabel lblGithub;
        private GroupBox grbJira;
        private TextBox txtJiraAPIToken;
        private Label lblJiraAPIToken;
        private TextBox txtJiraUsername;
        private Label lblJiraUsername;
        private GroupBox grbRea;
        private TextBox txtReaPassword;
        private Label lblReaPassword;
        private TextBox txtReaUsername;
        private Label lblReaUsername;
        private CheckBox chkJiraRememberMe;
        private Button btnReaLogin;
        private CheckBox chkReaRememberMe;
        private ComboBox cmbProjectName;
        private Label lblProjectName;
        private Button btnJiraLogin;
        private GroupBox grbWorklog;
        private DateTimePicker dtpStartTime;
        private Label lblEndDate;
        private Label lblStartDate;
        private Button btnFind;
        private DataGridView dgWorklog;
        private DateTimePicker dtpEndDate;
        private Button btnImport;
        private Label lblSelectedRowCount;
        private DataGridViewCheckBoxColumn ColumnSelected;
        private DataGridViewTextBoxColumn Key;
        private DataGridViewTextBoxColumn Summary;
        private DataGridViewTextBoxColumn Comment;
        private DataGridViewTextBoxColumn StartDate;
        private DataGridViewTextBoxColumn EndDate;
        private DataGridViewTextBoxColumn TimeSpent;
        private DataGridViewTextBoxColumn TimeSpentHours;
    }
}
