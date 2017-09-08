using System;
using JulMar.Smpp.Utility;
using JulMar.Smpp.Elements;

namespace JulMar.Smpp.Pdu {
	/// <summary>
	/// The outbind PDU is used by the SMSC to signal an ESME to originate a bind_receiver
	/// request to the SMSC.
	/// </summary>
	public class outbind : SmppRequest {
		// Class data
		private system_id sid_ = new system_id();
		private password pwd_ = new password();

		/// <summary>
		/// Constructor for the bind receiver
		/// </summary>
		public outbind()
			: base(Commands.OUTBIND) {
		}

		/// <summary>
		/// Constructor for the bind receiver
		/// </summary>
		public outbind(string sid, string pwd)
			:
			base(Commands.OUTBIND) {
			this.SystemID = sid;
			this.Password = pwd;
		}

		/// <summary>
		/// This propery returns whether a response is required for this PDU.
		/// </summary>
		/// <value>True/False</value>
		public override bool RequiresResponse {
			get { return false; }
		}

		/// <summary>
		/// The SystemID represents the system identifier
		/// </summary>
		public string SystemID {
			get { return sid_.Value; }
			set { sid_.Value = value; }
		}

		/// <summary>
		/// Password for the receiver; may be blank.
		/// </summary>
		public string Password {
			get { return pwd_.Value; }
			set { pwd_.Value = value; }
		}

		/// <summary>
		/// This method implements the ISupportSmppByteStream.AddToStream
		/// method so that the PDU can serialize itself to the data stream.
		/// </summary>
		/// <param name="writer">StreamWriter</param>
		public override void AddToStream(SmppWriter writer) {
			writer.Add(sid_);
			writer.Add(pwd_);
		}

		/// <summary>
		/// This method implements the ISupportSmppByteStream.GetFromStream
		/// method so that the PDU can serialize itself from the data stream.
		/// </summary>
		/// <param name="reader">StreamReader</param>
		public override void GetFromStream(SmppReader reader) {
			reader.ReadObject(sid_);
			reader.ReadObject(pwd_);
		}

		/// <summary>
		/// Override of the ToString method for debugging
		/// </summary>
		/// <returns>string</returns>
		public override string ToString() {
			return string.Format("outbind: {0},sid={1},pwd={2}{3}",
				base.ToString(), sid_, pwd_, base.DumpOptionalParams());
		}
	}
}