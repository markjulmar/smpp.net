#region Using directives

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Smpp;

#endregion

namespace SmscGui
{
    partial class GenerateSms : Form
    {
        static string _sourceAddr = "";
        static string _destAddr = "";
        static string _svcType = "";
        static string _message = "";

        public GenerateSms(SmppServer server)
        {
            InitializeComponent();

            // Init fields to last value.
            tbSource.Text = _sourceAddr;
            tbDest.Text = _destAddr;
            tbServiceType.Text = _svcType;
            tbMessage.Text = _message;

            // Load our sessions combo box.
            foreach (SmppSession session in server.CurrentSessions)
                cbSessions.Items.Add(session);
            if (cbSessions.Items.Count > 0)
                cbSessions.SelectedIndex = 0;
        }

        public SmppSession Session
        {
            get { return (SmppSession) cbSessions.SelectedItem; }
        }

        public string SourceAddress
        {
            get 
            { 
                string s = tbSource.Text; 
                _sourceAddr = s.Replace("(","").Replace(")","").Replace(" ","").Replace("-","");
                return _sourceAddr;
            }
        }

        public string DestinationAddress
        {
            get
            {
                string s = tbDest.Text;
                _destAddr = s.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "");
                return _destAddr;
            }
        }

        public string ServiceType
        {
            get 
            {
                _svcType = tbServiceType.Text;
                return _svcType; 
            }
        }

        public string ShortMessage
        {
            get 
            {
                _message = tbMessage.Text;
                return _message; 
            }
        }

    }
}