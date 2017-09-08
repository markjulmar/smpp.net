using System;
using System.Collections.Generic;
using JulMar.Smpp;
using JulMar.Smpp.Elements;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Pdu {
	/// <summary>
	/// The view_dl_resp PDU is used to return the members of a distribution lists "owned"
	/// by a particular source ESME address.  It is the response to the view_dl PDU.
	/// </summary>
	public class view_dl_resp : SmppResponse {
		private List<dl_member_details> distList_ = new List<dl_member_details>();

		/// <summary>
		/// Default constructor
		/// </summary>
		public view_dl_resp()
			: base(Commands.VIEW_DL_RESP) {
		}

		/// <summary>
		/// Default constructor
		/// </summary>
		/// <param name="sequenceNumber">Sequence number</param>
		public view_dl_resp(int sequenceNumber)
			:
			 base(Commands.VIEW_DL_RESP, StatusCodes.ESME_ROK, sequenceNumber) {
		}

		/// <summary>
		/// This provides access to the list.
		/// </summary>
		[CLSCompliant(false)]
		public List<dl_member_details> Members {
			get { return distList_; }
			set {
				if (value != null)
					distList_ = value;
				else
					distList_.Clear();
			}
		}

		/// <summary>
		/// This method implements the ISupportSmppByteStream.AddToStream
		/// method so that the PDU can serialize itself to the data stream.
		/// </summary>
		/// <param name="writer">StreamWriter</param>
		public override void AddToStream(SmppWriter writer) {
			writer.Add((byte)distList_.Count);
			foreach (dl_member_details dtls in distList_)
				writer.Add(dtls);
		}

		/// <summary>
		/// This method implements the ISupportSmppByteStream.GetFromStream
		/// method so that the PDU can serialize itself from the data stream.
		/// </summary>
		/// <param name="reader">StreamReader</param>
		public override void GetFromStream(SmppReader reader) {
			distList_.Clear();
			int count = reader.ReadByte();
			for (int i = 0; i < count; i++) {
				dl_member_details dtls = new dl_member_details();
				dtls.GetFromStream(reader);
				distList_.Add(dtls);
			}
		}

		/// <summary>
		/// Override of the ToString method for debugging
		/// </summary>
		/// <returns>string</returns>
		public override string ToString() {
			return string.Format("view_dl_resp: {0},{1}", base.ToString(), distList_);
		}
	}
}