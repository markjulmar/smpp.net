using System;
using System.Text;
using JulMar.Smpp.Elements;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Pdu {
	/// <summary>
	/// The list_dls operation is sent by the ESME to the SMSC to retreive a list of
	/// all the distribution lists "owned" by a particular SME (source).  The SMSC
	/// will acknowledge the command with a list_dls_resp PDU.
	/// </summary>
	public class list_dls : SmppRequest {
		// Class data - required parameters
		private address sourceAddr_ = new address();

		/// <summary>
		/// Default constructor
		/// </summary>
		public list_dls()
			: base(Commands.LIST_DLS) {
		}

		/// <summary>
		/// Primary constructor for the list_dls PDU
		/// </summary>
		/// <param name="source">ESME source address</param>
		public list_dls(address source)
			: this() {
			this.Address = source;
		}

		/// <summary>
		/// The address of the source ESME which owns this distribution list.
		/// </summary>
		public address Address {
			get { return sourceAddr_; }
			set {
				sourceAddr_ = value;
				if (sourceAddr_ == null)
					throw new ArgumentNullException("Address", "The source address is a required parameter and cannot be null.");
			}
		}

		/// <summary>
		/// This method implements the ISupportSmppByteStream.AddToStream
		/// method so that the PDU can serialize itself to the data stream.
		/// </summary>
		/// <param name="writer">StreamWriter</param>
		public override void AddToStream(SmppWriter writer) {
			writer.Add(sourceAddr_);
		}

		/// <summary>
		/// This method implements the ISupportSmppByteStream.GetFromStream
		/// method so that the PDU can serialize itself from the data stream.
		/// </summary>
		/// <param name="reader">StreamReader</param>
		public override void GetFromStream(SmppReader reader) {
			reader.ReadObject(sourceAddr_);
		}

		/// <summary>
		/// Override of the ToString method for debugging
		/// </summary>
		/// <returns>string</returns>
		public override string ToString() {
			return string.Format("list_dls: {0},addr={1}{3}",
				 base.ToString(), sourceAddr_.ToString(), base.DumpOptionalParams());
		}
	}
}
