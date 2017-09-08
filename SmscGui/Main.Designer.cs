namespace SmscGui
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.lvMessages = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miSucceededResponse = new System.Windows.Forms.ToolStripMenuItem();
            this.miFailedResponse = new System.Windows.Forms.ToolStripMenuItem();
            this.lvSessions = new System.Windows.Forms.ListView();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader9 = new System.Windows.Forms.ColumnHeader();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.miRejectedResponse = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tscbMessageResponseType = new System.Windows.Forms.ToolStripComboBox();
            this.btnGenerateSms = new System.Windows.Forms.ToolStripButton();
            this.btnEnableEnquire = new System.Windows.Forms.ToolStripButton();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.contextMenuStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvMessages
            // 
            this.lvMessages.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.lvMessages.ContextMenuStrip = this.contextMenuStrip1;
            this.lvMessages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvMessages.FullRowSelect = true;
            this.lvMessages.GridLines = true;
            this.lvMessages.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvMessages.HideSelection = false;
            this.lvMessages.Location = new System.Drawing.Point(0, 25);
            this.lvMessages.Name = "lvMessages";
            this.lvMessages.Size = new System.Drawing.Size(735, 247);
            this.lvMessages.TabIndex = 0;
            this.lvMessages.UseCompatibleStateImageBehavior = false;
            this.lvMessages.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "ID";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Source Adr";
            this.columnHeader2.Width = 83;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Dest Adr";
            this.columnHeader3.Width = 72;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Submit Time";
            this.columnHeader4.Width = 120;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Delivery Time";
            this.columnHeader5.Width = 120;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Result";
            this.columnHeader6.Width = 100;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.AllowDrop = true;
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miSucceededResponse,
            this.miFailedResponse});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Opacity = 0.8;
            this.contextMenuStrip1.Size = new System.Drawing.Size(138, 48);
            // 
            // miSucceededResponse
            // 
            this.miSucceededResponse.Name = "miSucceededResponse";
            this.miSucceededResponse.Size = new System.Drawing.Size(137, 22);
            this.miSucceededResponse.Text = "Succeeded";
            this.miSucceededResponse.Click += new System.EventHandler(this.OnSendSuccessResponse);
            // 
            // miFailedResponse
            // 
            this.miFailedResponse.Name = "miFailedResponse";
            this.miFailedResponse.Size = new System.Drawing.Size(137, 22);
            this.miFailedResponse.Text = "Failed";
            this.miFailedResponse.Click += new System.EventHandler(this.OnSendFailResponse);
            // 
            // lvSessions
            // 
            this.lvSessions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9});
            this.lvSessions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvSessions.Location = new System.Drawing.Point(0, 0);
            this.lvSessions.Name = "lvSessions";
            this.lvSessions.Size = new System.Drawing.Size(360, 268);
            this.lvSessions.TabIndex = 0;
            this.lvSessions.UseCompatibleStateImageBehavior = false;
            this.lvSessions.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "SID";
            this.columnHeader7.Width = 100;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Address";
            this.columnHeader8.Width = 100;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Type";
            this.columnHeader9.Width = 100;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(0, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(371, 268);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // miRejectedResponse
            // 
            this.miRejectedResponse.Name = "miRejectedResponse";
            this.miRejectedResponse.Size = new System.Drawing.Size(32, 19);
            this.miRejectedResponse.Text = "Rejected";
            this.miRejectedResponse.Click += new System.EventHandler(this.OnSendRejectResponse);
            // 
            // timer1
            // 
            this.timer1.Interval = 30000;
            this.timer1.Tick += new System.EventHandler(this.OnSendEnquire);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lvMessages);
            this.splitContainer1.Panel1.Controls.Add(this.toolStrip1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(735, 544);
            this.splitContainer1.SplitterDistance = 272;
            this.splitContainer1.TabIndex = 1;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tscbMessageResponseType,
            this.btnGenerateSms,
            this.btnEnableEnquire});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip1.Size = new System.Drawing.Size(735, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tscbMessageResponseType
            // 
            this.tscbMessageResponseType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscbMessageResponseType.Items.AddRange(new object[] {
            "Random",
            "Always Succeed",
            "Always Fail",
            "Manual Response"});
            this.tscbMessageResponseType.Name = "tscbMessageResponseType";
            this.tscbMessageResponseType.Size = new System.Drawing.Size(121, 25);
            // 
            // btnGenerateSms
            // 
            this.btnGenerateSms.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGenerateSms.Image = global::SmscGui.Properties.Resources.gensms;
            this.btnGenerateSms.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGenerateSms.Name = "btnGenerateSms";
            this.btnGenerateSms.Size = new System.Drawing.Size(23, 22);
            this.btnGenerateSms.Text = "toolStripButton1";
            this.btnGenerateSms.ToolTipText = "Generate SMS";
            this.btnGenerateSms.Click += new System.EventHandler(this.OnGenerateSms);
            // 
            // btnEnableEnquire
            // 
            this.btnEnableEnquire.CheckOnClick = true;
            this.btnEnableEnquire.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEnableEnquire.Image = global::SmscGui.Properties.Resources.enquire;
            this.btnEnableEnquire.ImageTransparentColor = System.Drawing.Color.Lime;
            this.btnEnableEnquire.Name = "btnEnableEnquire";
            this.btnEnableEnquire.Size = new System.Drawing.Size(23, 22);
            this.btnEnableEnquire.Text = "toolStripButton2";
            this.btnEnableEnquire.ToolTipText = "Enable Enquire Pings";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.lvSessions);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.richTextBox1);
            this.splitContainer2.Size = new System.Drawing.Size(735, 268);
            this.splitContainer2.SplitterDistance = 360;
            this.splitContainer2.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(735, 544);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "SMSC Simulator 1.0";
            this.Load += new System.EventHandler(this.OnLoad);
            this.contextMenuStrip1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvMessages;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.ListView lvSessions;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem miSucceededResponse;
        private System.Windows.Forms.ToolStripMenuItem miFailedResponse;
        private System.Windows.Forms.ToolStripMenuItem miRejectedResponse;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ToolStripComboBox tscbMessageResponseType;
        private System.Windows.Forms.ToolStripButton btnGenerateSms;
        private System.Windows.Forms.ToolStripButton btnEnableEnquire;

    }
}