using System;
using JulMar.Smpp.Utility;
using JulMar.Smpp.Elements;

namespace JulMar.Smpp.Pdu {
	/// <summary>
	/// The bind_receiver PDU is used to bind a reception side connection from an ESME to a MC.
	/// </summary>
	public class bind_receiver : SmppRequest {
		// Class data
		private system_id sid_ = new system_id();
		private password pwd_ = new password();
		private system_type stype_ = new system_type();
		private interface_version ifver_ = new interface_version();
		private address_range addr_range_ = new address_range();

		/// <summary>
		/// Constructor for the bind receiver
		/// </summary>
		public bind_receiver()
			: base(Commands.BIND_RECEIVER) {
		}

		/// <summary>
		/// Constructor for the bind receiver
		/// </summary>
		public bind_receiver(string sid, string pwd, string stype, byte ifver, address_range arange)
			:
			base(Commands.BIND_RECEIVER) {
			this.SystemID = sid;
			this.Password = pwd;
			this.SystemType = stype;
			this.InterfaceVersion = ifver;
			this.AddressRange = arange;
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
		/// ESME type
		/// </summary>
		public string SystemType {
			get { return stype_.Value; }
			set { stype_.Value = value; }
		}

		/// <summary>
		/// SMPP interface version
		/// </summary>
		public byte InterfaceVersion {
			get { return ifver_.Value; }
			set { ifver_.Value = value; }
		}

		/// <summary>
		/// Address range
		/// </summary>
		public address_range AddressRange {
			get { return addr_range_; }
			set { addr_range_ = value; }
		}

		/// <summary>
		/// This method implements the ISupportSmppByteStream.AddToStream
		/// method so that the PDU can serialize itself to the data stream.
		/// </summary>
		/// <param name="writer">StreamWriter</param>
		public override void AddToStream(SmppWriter writer) {
			writer.Add(sid_);
			writer.Add(pwd_);
			writer.Add(stype_);
			writer.Add(ifver_);
			writer.Add(addr_range_);
		}

		/// <summary>
		/// This method implements the ISupportSmppByteStream.GetFromStream
		/// method so that the PDU can serialize itself from the data stream.
		/// </summary>
		/// <param name="reader">StreamReader</param>
		public override void GetFromStream(SmppReader reader) {
			reader.ReadObject(sid_);
			reader.ReadObject(pwd_);
			reader.ReadObject(stype_);
			reader.ReadObject(ifver_);
			reader.ReadObject(addr_range_);
		}

		/// <summary>
		/// Override of the ToString method for debugging
		/// </summary>
		/// <returns>string</returns>
		public override string ToString() {
			return string.Format("bind_receiver: {0},sid={1},pwd={2},type={3},ver={4},{5}{6}",
				base.ToString(), sid_, pwd_, stype_, ifver_, addr_range_, base.DumpOptionalParams());
		}
	}
}