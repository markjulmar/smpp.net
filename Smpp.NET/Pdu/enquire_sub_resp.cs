using System;
using JulMar.Smpp;
using JulMar.Smpp.Utility;
using JulMar.Smpp.Elements;

namespace JulMar.Smpp.Pdu {
	/// <summary>
	/// The enquire_sub_resp PDU is used to return the customer provisioning
	/// record for an enquire_sub request.
	/// </summary>
	public class enquire_sub_resp : SmppResponse {
		// Class data - required parameters
		private smsc_provisioning_record rec_ = new smsc_provisioning_record();

		/// <summary>
		/// Default constructor
		/// </summary>
		public enquire_sub_resp()
			: base(Commands.QUERY_SUB_RESP) {
		}

		/// <summary>
		/// Default constructor
		/// </summary>
		/// <param name="sequenceNumber">Sequence number</param>
		public enquire_sub_resp(int sequenceNumber)
			:
			 base(Commands.QUERY_SUB_RESP, StatusCodes.ESME_ROK, sequenceNumber) {
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
			return string.Format("enquire_sub_resp: {0},{1}{2}",
				 base.ToString(), rec_.ToString(), base.DumpOptionalParams());
		}
	}
}