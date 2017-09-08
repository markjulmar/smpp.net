using System;
using System.Text;
using JulMar.Smpp.Elements;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Pdu {
	/// <summary>
	/// The del_dl operation is sent by the ESME to the SMSC to delete an existing
	/// distribution list in the system. 
	/// </summary>
	public class del_dl : SmppRequest {
		// Class data - required parameters
		private address sourceAddr_ = new address();
		private SmppCOctetString dlname_ = new SmppCOctetString();

		/// <summary>
		/// Default constructor
		/// </summary>
		public del_dl()
			: base(Commands.DEL_DL) {
		}

		/// <summary>
		/// Primary constructor for the del_dl PDU
		/// </summary>
		/// <param name="source">ESME source address</param>
		/// <param name="dl_name">Distribution list name</param>
		public del_dl(address source, string dl_name)
			: this() {
			sourceAddr_ = source;
			dlname_.Value = dl_name;

			if (sourceAddr_ == null)
				throw new ArgumentNullException("source", "The source address is a required parameter and cannot be null.");
			if (dl_name.Length == 0)
				throw new ArgumentOutOfRangeException("dl_name", "The distribution list name is a required parameter and must be non-blank.");
		}

		/// <summary>
		/// The address of the source ESME which owns this distribution list.
		/// </summary>
		public address Address {
			get { return sourceAddr_; }
			set {
				if (value == null)
					throw new ArgumentNullException("Address", "The source address is a required parameter and cannot be null.");
				sourceAddr_ = value;
			}
		}

		/// <summary>
		/// The distribution list name
		/// </summary>
		public string DistributionListName {
			get { return dlname_.Value; }
			set {
				if (value.Length == 0)
					throw new ArgumentOutOfRangeException("DistributionListName", "The distribution list name is a required parameter and must be non-blank.");
				dlname_.Value = value;
			}
		}

		/// <summary>
		/// This method implements the ISupportSmppByteStream.AddToStream
		/// method so that the PDU can serialize itself to the data stream.
		/// </summary>
		/// <param name="writer">StreamWriter</param>
		public override void AddToStream(SmppWriter writer) {
			writer.Add(sourceAddr_);
			writer.Add(dlname_);
		}

		/// <summary>
		/// This method implements the ISupportSmppByteStream.GetFromStream
		/// method so that the PDU can serialize itself from the data stream.
		/// </summary>
		/// <param name="reader">StreamReader</param>
		public override void GetFromStream(SmppReader reader) {
			reader.ReadObject(sourceAddr_);
			reader.ReadObject(dlname_);
		}

		/// <summary>
		/// Override of the ToString method for debugging
		/// </summary>
		/// <returns>string</returns>
		public override string ToString() {
			return string.Format("del_dl: {0},addr={1},dl_name={2}{3}",
				 base.ToString(), sourceAddr_.ToString(), dlname_.ToString(),
				 base.DumpOptionalParams());
		}
	}
}
