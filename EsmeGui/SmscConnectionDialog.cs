using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JulMar.Smpp;

namespace EsmeGui
{
    public partial class SmscConnectionDialog : Form
    {
        public string SmscAddress
        {
            get { return txtSmscAddress.Text; }
            set { txtSmscAddress.Text = value; }
        }

        public int SmscPort
        {
            get { return (int) udPort.Value; }
            set { udPort.Value = (decimal) value; }
        }

        public int SmppInterfaceVersion
        {
            get
            {
                string ver = cbSmppVersion.Text;
                switch (ver)
                {
                    case "3.3": return SmppVersion.SMPP_V33;
                    case "3.4": return SmppVersion.SMPP_V34;
                    default: return SmppVersion.SMPP_V50;
                }
            }
        }

        public string SystemID
        {
            get { return txtSystemId.Text; }
            set { txtSystemId.Text = value; }
        }

        public string Password
        {
            get { return txtPassword.Text; }
            set { txtPassword.Text = value; }
        }

        public string SystemType
        {
            get { return txtSystemType.Text; }
            set { txtSystemType.Text = value; }
        }

        public int EnquireLinkSeconds
        {
            get { return (int)udEnquireSeconds.Value; }
            set { udEnquireSeconds.Value = (decimal)value; }
        }

        public SmscConnectionDialog()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            // Assure all required fields are non-blank
        }
    }
}
