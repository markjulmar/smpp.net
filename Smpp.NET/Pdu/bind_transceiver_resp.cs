using System;
using JulMar.Smpp.Utility;
using JulMar.Smpp.Elements;

namespace JulMar.Smpp.Pdu {
	/// <summary>
	/// The bind_transceiver_resp is sent in response to the bind_transceiver PDU.
	/// </summary>
	public class bind_transceiver_resp : SmppResponse {
		// Class data
		private system_id sid_ = new system_id();
		private sc_interface_version ver_ = new sc_interface_version(SmppVersion.SMPP_V50);

		/// <summary>
		/// Constructor for the bind transceiver response
		/// </summary>
		public bind_transceiver_resp()
			: base(Commands.BIND_TRANSCEIVER_RESP) {
			AddOptionalElements(ver_);
		}

		/// <summary>
		/// Constructor for the bind transceiver response
		/// </summary>
		public bind_transceiver_resp(int seqNum, int status)
			: base(Commands.BIND_TRANSCEIVER_RESP, status, seqNum) {
			AddOptionalElements(ver_);
		}

		/// <summary>
		/// Constructor for the bind transceiver response
		/// </summary>
		/// <param name="seqNum">Sequence Number</param>
		/// <param name="sid">System ID</param>
		public bind_transceiver_resp(int seqNum, string sid)
			: base(Commands.BIND_TRANSCEIVER_RESP, seqNum) {
			AddOptionalElements(ver_);
			SystemID = sid;
		}

		/// <summary>
		/// The SystemID represents the system identifier
		/// </summary>
		public string SystemID {
			get { return sid_.Value; }
			set { sid_.Value = value; }
		}

		/// <summary>
		/// This returns whether the SMPP interface version is included
		/// </summary>
		public bool HasInterfaceVersion {
			get { return ver_.HasValue; }
		}

		/// <summary>
		/// This returns the SMPP interface version
		/// </summary>
		public byte InterfaceVersion {
			get { return ver_.Value; }
			set { ver_.Value = value; }
		}

		/// <summary>
		/// This method implements the ISupportSmppByteStream.AddToStream
		/// method so that the PDU can serialize itself to the data stream.
		/// </summary>
		/// <param name="writer">StreamWriter</param>
		public override void AddToStream(SmppWriter writer) {
			writer.Add(sid_);
		}

		/// <summary>
		/// This method implements the ISupportSmppByteStream.GetFromStream
		/// method so that the PDU can serialize itself from the data stream.
		/// </summary>
		/// <param name="reader">StreamReader</param>
		public override void GetFromStream(SmppReader reader) {
			reader.ReadObject(sid_);
		}

		/// <summary>
		/// Override of the ToString method for debugging
		/// </summary>
		/// <returns>string</returns>
		public override string ToString() {
			return string.Format("bind_transceiver_resp: {0},sid={1},ver={2:X}{3}",
				base.ToString(), sid_, InterfaceVersion, base.DumpOptionalParams());
		}
	}
}