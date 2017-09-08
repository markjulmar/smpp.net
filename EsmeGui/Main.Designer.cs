namespace EsmeGui
{
    partial class mainForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainForm));
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.txtSourceNumber = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.lblSmscConnectionStatus = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.btnSendMessage = new System.Windows.Forms.Button();
			this.txtMessage = new System.Windows.Forms.TextBox();
			this.txtDestinationNumber = new System.Windows.Forms.MaskedTextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.btnClear = new System.Windows.Forms.Button();
			this.lvSentMsgs = new System.Windows.Forms.ListView();
			this.messageId = new System.Windows.Forms.ColumnHeader();
			this.phoneNumber = new System.Windows.Forms.ColumnHeader();
			this.messageText = new System.Windows.Forms.ColumnHeader();
			this.finalStatus = new System.Windows.Forms.ColumnHeader();
			this.deliveryDateTime = new System.Windows.Forms.ColumnHeader();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.txtSourceNumber);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.lblSmscConnectionStatus);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.btnSendMessage);
			this.groupBox1.Controls.Add(this.txtMessage);
			this.groupBox1.Controls.Add(this.txtDestinationNumber);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(606, 184);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Send a SMS Message";
			// 
			// txtSourceNumber
			// 
			this.txtSourceNumber.Location = new System.Drawing.Point(97, 22);
			this.txtSourceNumber.MaxLength = 10;
			this.txtSourceNumber.Name = "txtSourceNumber";
			this.txtSourceNumber.Size = new System.Drawing.Size(83, 20);
			this.txtSourceNumber.TabIndex = 1;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(6, 25);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(54, 13);
			this.label4.TabIndex = 0;
			this.label4.Text = "Source #:";
			// 
			// lblSmscConnectionStatus
			// 
			this.lblSmscConnectionStatus.AutoSize = true;
			this.lblSmscConnectionStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblSmscConnectionStatus.Location = new System.Drawing.Point(336, 25);
			this.lblSmscConnectionStatus.MaximumSize = new System.Drawing.Size(150, 0);
			this.lblSmscConnectionStatus.Name = "lblSmscConnectionStatus";
			this.lblSmscConnectionStatus.Size = new System.Drawing.Size(85, 13);
			this.lblSmscConnectionStatus.TabIndex = 7;
			this.lblSmscConnectionStatus.Text = "Disconnected";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(200, 25);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(130, 13);
			this.label3.TabIndex = 6;
			this.label3.Text = "SMSC Connection Status:";
			// 
			// btnSendMessage
			// 
			this.btnSendMessage.Enabled = false;
			this.btnSendMessage.Location = new System.Drawing.Point(254, 153);
			this.btnSendMessage.Name = "btnSendMessage";
			this.btnSendMessage.Size = new System.Drawing.Size(98, 23);
			this.btnSendMessage.TabIndex = 6;
			this.btnSendMessage.Text = "Send Message";
			this.btnSendMessage.UseVisualStyleBackColor = true;
			this.btnSendMessage.Click += new System.EventHandler(this.btnSendMessage_Click);
			// 
			// txtMessage
			// 
			this.txtMessage.Location = new System.Drawing.Point(97, 79);
			this.txtMessage.MaxLength = 160;
			this.txtMessage.Multiline = true;
			this.txtMessage.Name = "txtMessage";
			this.txtMessage.Size = new System.Drawing.Size(503, 68);
			this.txtMessage.TabIndex = 5;
			// 
			// txtDestinationNumber
			// 
			this.txtDestinationNumber.Location = new System.Drawing.Point(97, 50);
			this.txtDestinationNumber.Mask = "(999) 000-0000";
			this.txtDestinationNumber.Name = "txtDestinationNumber";
			this.txtDestinationNumber.Size = new System.Drawing.Size(83, 20);
			this.txtDestinationNumber.TabIndex = 3;
			this.txtDestinationNumber.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 79);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(71, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Text to Send:";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 53);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(73, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Destination #:";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.btnClear);
			this.groupBox2.Controls.Add(this.lvSentMsgs);
			this.groupBox2.Location = new System.Drawing.Point(12, 202);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(606, 207);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "SMS Messages Sent";
			// 
			// btnClear
			// 
			this.btnClear.Location = new System.Drawing.Point(254, 178);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(98, 23);
			this.btnClear.TabIndex = 8;
			this.btnClear.Text = "Clear Messages";
			this.btnClear.UseVisualStyleBackColor = true;
			this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
			// 
			// lvSentMsgs
			// 
			this.lvSentMsgs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.messageId,
            this.phoneNumber,
            this.messageText,
            this.finalStatus,
            this.deliveryDateTime});
			this.lvSentMsgs.Location = new System.Drawing.Point(7, 19);
			this.lvSentMsgs.Name = "lvSentMsgs";
			this.lvSentMsgs.Size = new System.Drawing.Size(593, 153);
			this.lvSentMsgs.TabIndex = 7;
			this.lvSentMsgs.UseCompatibleStateImageBehavior = false;
			this.lvSentMsgs.View = System.Windows.Forms.View.Details;
			// 
			// messageId
			// 
			this.messageId.Text = "Message ID";
			this.messageId.Width = 75;
			// 
			// phoneNumber
			// 
			this.phoneNumber.Text = "Phone Number";
			this.phoneNumber.Width = 91;
			// 
			// messageText
			// 
			this.messageText.Text = "Message Text";
			this.messageText.Width = 191;
			// 
			// finalStatus
			// 
			this.finalStatus.Text = "Final Status";
			this.finalStatus.Width = 99;
			// 
			// deliveryDateTime
			// 
			this.deliveryDateTime.Text = "Delivery Date/Time";
			this.deliveryDateTime.Width = 117;
			// 
			// mainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(630, 423);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "mainForm";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Esme SMS Message Tester";
			this.Shown += new System.EventHandler(this.mainForm_Shown);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.MaskedTextBox txtDestinationNumber;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnSendMessage;
        private System.Windows.Forms.ListView lvSentMsgs;
        private System.Windows.Forms.ColumnHeader messageId;
        private System.Windows.Forms.ColumnHeader phoneNumber;
        private System.Windows.Forms.ColumnHeader messageText;
        private System.Windows.Forms.ColumnHeader finalStatus;
        private System.Windows.Forms.ColumnHeader deliveryDateTime;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label lblSmscConnectionStatus;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSourceNumber;
        private System.Windows.Forms.Label label4;
    }
}

