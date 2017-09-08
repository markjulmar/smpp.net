using System;
using System.Text;
using JulMar.Smpp.Elements;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Pdu {
	/// <summary>
	/// The mod_sub operation is sent by the ESME to the SMSC to modify an existing
	/// subscriber/customer in the SMSC database.
	/// </summary>
	public class mod_sub : SmppRequest {
		// Class data - required parameters
		private smsc_provisioning_record rec_ = new smsc_provisioning_record();

		/// <summary>
		/// Default constructor
		/// </summary>
		public mod_sub()
			: base(Commands.MOD_SUB) {
		}

		/// <summary>
		/// Primary constructor for the mod_sub PDU
		/// </summary>
		public mod_sub(smsc_provisioning_record rec)
			: this() {
			this.Customer = rec;
		}

		/// <summary>
		/// The provisioning record for the customer
		/// </summary>
		public smsc_provisioning_record Customer {
			get { return rec_; }
			set { rec_ = value; }
		}

		/// <summary>
		/// This method implements the ISupportSmppByteStream.AddToStream
		/// method so that the PDU can serialize itself to the data stream.
		/// </summary>
		/// <param name="writer">StreamWriter</param>
		public override void AddToStream(SmppWriter writer) {
			writer.Add(rec_);
		}

		/// <summary>
		/// This method implements the ISupportSmppByteStream.GetFromStream
		/// method so that the PDU can serialize itself from the data stream.
		/// </summary>
		/// <param name="reader">StreamReader</param>
		public override void GetFromStream(SmppReader reader) {
			reader.ReadObject(rec_);
		}

		/// <summary>
		/// Override of the ToString method for debugging
		/// </summary>
		/// <returns>string</returns>
		public override string ToString() {
			return string.Format("mod_sub: {0},{1}{2}",
				 base.ToString(), rec_.ToString(), base.DumpOptionalParams());
		}
	}
}
