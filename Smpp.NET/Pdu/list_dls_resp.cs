using System;
using System.Collections.Generic;
using JulMar.Smpp;
using JulMar.Smpp.Elements;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Pdu {
	/// <summary>
	/// The list_dls_resp PDU is used to return a list of distribution lists "owned"
	/// by a particular source ESME address.  It is the response to the list_dls PDU.
	/// </summary>
	public class list_dls_resp : SmppResponse {
		private List<string> distList_ = new List<string>();

		/// <summary>
		/// Default constructor
		/// </summary>
		public list_dls_resp()
			: base(Commands.LIST_DLS_RESP) {
		}

		/// <summary>
		/// Default constructor
		/// </summary>
		/// <param name="sequenceNumber">Sequence number</param>
		public list_dls_resp(int sequenceNumber)
			:
			 base(Commands.LIST_DLS_RESP, StatusCodes.ESME_ROK, sequenceNumber) {
		}

		/// <summary>
		/// This provides access to the list.
		/// </summary>
		[CLSCompliant(false)]
		public List<string> DistributionLists {
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
			foreach (string str in distList_)
				writer.Add(new SmppCOctetString(str));
		}

		/// <summary>
		/// This method implements the ISupportSmppByteStream.GetFromStream
		/// method so that the PDU can serialize itself from the data stream.
		/// </summary>
		/// <param name="reader">StreamReader</param>
		public override void GetFromStream(SmppReader reader) {
			distList_ = new List<string>();
			int count = (int)reader.ReadByte();
			for (int i = 0; i < count; i++)
				distList_.Add(reader.ReadString());
		}

		/// <summary>
		/// Override of the ToString method for debugging
		/// </summary>
		/// <returns>string</returns>
		public override string ToString() {
			return string.Format("list_dls_resp: {0},{1}", base.ToString(), distList_);
		}
	}
}