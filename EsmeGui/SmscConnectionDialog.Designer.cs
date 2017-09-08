namespace EsmeGui
{
    partial class SmscConnectionDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSmscAddress = new System.Windows.Forms.TextBox();
            this.udPort = new System.Windows.Forms.NumericUpDown();
            this.txtSystemId = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.cbSmppVersion = new System.Windows.Forms.ComboBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.udEnquireSeconds = new System.Windows.Forms.NumericUpDown();
            this.txtSystemType = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.udPort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udEnquireSeconds)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Smsc Adderss:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Smsc Port:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "System Id:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Smpp Version:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 127);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Password:";
            // 
            // txtSmscAddress
            // 
            this.txtSmscAddress.Location = new System.Drawing.Point(96, 19);
            this.txtSmscAddress.Name = "txtSmscAddress";
            this.txtSmscAddress.Size = new System.Drawing.Size(184, 20);
            this.txtSmscAddress.TabIndex = 0;
            // 
            // udPort
            // 
            this.udPort.Location = new System.Drawing.Point(96, 45);
            this.udPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.udPort.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.udPort.Name = "udPort";
            this.udPort.Size = new System.Drawing.Size(87, 20);
            this.udPort.TabIndex = 1;
            this.udPort.Value = new decimal(new int[] {
            8080,
            0,
            0,
            0});
            // 
            // txtSystemId
            // 
            this.txtSystemId.Location = new System.Drawing.Point(96, 98);
            this.txtSystemId.Name = "txtSystemId";
            this.txtSystemId.Size = new System.Drawing.Size(184, 20);
            this.txtSystemId.TabIndex = 3;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(96, 124);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(184, 20);
            this.txtPassword.TabIndex = 4;
            // 
            // cbSmppVersion
            // 
            this.cbSmppVersion.FormattingEnabled = true;
            this.cbSmppVersion.Items.AddRange(new object[] {
            "3.3",
            "3.4",
            "5.0"});
            this.cbSmppVersion.Location = new System.Drawing.Point(96, 72);
            this.cbSmppVersion.Name = "cbSmppVersion";
            this.cbSmppVersion.Size = new System.Drawing.Size(87, 21);
            this.cbSmppVersion.TabIndex = 2;
            this.cbSmppVersion.Text = "3.4";
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(45, 205);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "Ok";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(179, 205);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 179);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(123, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Send Enquire Link every";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(191, 179);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "seconds";
            // 
            // udEnquireSeconds
            // 
            this.udEnquireSeconds.Location = new System.Drawing.Point(141, 176);
            this.udEnquireSeconds.Name = "udEnquireSeconds";
            this.udEnquireSeconds.Size = new System.Drawing.Size(42, 20);
            this.udEnquireSeconds.TabIndex = 6;
            this.udEnquireSeconds.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // txtSystemType
            // 
            this.txtSystemType.Location = new System.Drawing.Point(96, 150);
            this.txtSystemType.Name = "txtSystemType";
            this.txtSystemType.Size = new System.Drawing.Size(184, 20);
            this.txtSystemType.TabIndex = 5;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 153);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(71, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "System Type:";
            // 
            // SmscConnectionDialog
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 236);
            this.Controls.Add(this.txtSystemType);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.udEnquireSeconds);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.cbSmppVersion);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtSystemId);
            this.Controls.Add(this.udPort);
            this.Controls.Add(this.txtSmscAddress);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SmscConnectionDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Smsc Connection Parameters";
            ((System.ComponentModel.ISupportInitialize)(this.udPort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udEnquireSeconds)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSmscAddress;
        private System.Windows.Forms.NumericUpDown udPort;
        private System.Windows.Forms.TextBox txtSystemId;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.ComboBox cbSmppVersion;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown udEnquireSeconds;
        private System.Windows.Forms.TextBox txtSystemType;
        private System.Windows.Forms.Label label8;
    }
}