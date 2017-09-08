using System;
using System.Net;
using System.Collections.Generic;
using System.Collections;
using JulMar.SocketBase;
using Ninetech.Communication.Smpp.Pdu;

namespace Ninetech.Communication.Smpp.Esme {
	public class EsmeClient {
		private SocketClient socket = null;
		private EsmeSession session = null;
		
		public event EventHandler<SmppConnectEventArgs> Connected;
		public event EventHandler<SmppEventArgs> ConnectionFailed;

		public EsmeSession Session {
			get { return this.session; }
		}

		public EsmeClient() {
		}

		public void Connect(string address, int port) {
			this.socket = SocketManager.Connect(address, port);
			this.socket.OnStartSession += socket_OnStartSession;
			this.socket.OnFailure += socket_OnFailure;
		}

		private void socket_OnFailure(object sender, SocketEventArgs args) {
			if (ConnectionFailed != null) {
				ConnectionFailed(this, new SmppEventArgs());
			}
		}

		private void socket_OnStartSession(object sender, SocketEventArgs args) {
			this.socket = ((ConnectEventArgs)args).Client;
			this.session = new EsmeSession(this, this.socket);
			this.socket.Tag = this.session;
			if (Connected != null) {
				Connected(this, new SmppConnectEventArgs(this.socket.Address, this.session));
			}
		}
	}
}
