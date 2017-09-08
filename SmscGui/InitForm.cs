#region Using directives

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Smpp;

#endregion

namespace SmscGui
{
    partial class InitForm : Form
    {
        public string SystemID
        {
            get { return txtSid.Text; }
        }

        public string Password
        {
            get { return txtPassword.Text; }
        }

        public int Port
        {
            get { return Convert.ToInt32(txtPort.Value); }
        }

        public int Version
        {
            get 
            {
                string ver = cbVersion.Text;
                switch (ver)
                {
                    case "3.3": return SmppVersion.SMPP_V33;
                    case "3.4": return SmppVersion.SMPP_V34;
                    default: return SmppVersion.SMPP_V50;
                }
            }
        }

        public InitForm()
        {
            InitializeComponent();
            txtSid.Text = Environment.MachineName;
            cbVersion.SelectedIndex = 1;
            txtPort.Value = 8080;
        }

        private void OnValidateSystemId(object sender, CancelEventArgs e)
        {
            string errText = null;
            if (txtSid.Text.Length == 0)
            {
                errText = "Please enter the SMSC system id.";
                e.Cancel = true;
            }
            errorProvider1.SetError((Control)sender, errText);
        }
    }
}