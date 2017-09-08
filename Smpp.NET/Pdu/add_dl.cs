using System;
using System.Text;
using JulMar.Smpp.Elements;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Pdu {
	/// <summary>
	/// The add_dl operation is sent by the ESME to the SMSC to add a new
	/// distribution list to the system. Distribution lists added by an ESME are 
	/// not system-wide.
	/// </summary>
	public class add_dl : SmppRequest {
		// Class data - required parameters
		private address sourceAddr_ = new address();
		private SmppCOctetString dlname_ = new SmppCOctetString();

		/// <summary>
		/// Default constructor
		/// </summary>
		public add_dl()
			: base(Commands.ADD_DL) {
		}

		/// <summary>
		/// Primary constructor for the add_dl PDU
		/// </summary>
		/// <param name="source">ESME source address</param>
		/// <param name="dl_name">Distribution list name</param>
		public add_dl(address source, string dl_name)
			: this() {
			this.Address = source;
			this.DistributionListName = dl_name;
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
			return string.Format("add_dl: {0},addr={1},dl_name={2}{3}",
				 base.ToString(), sourceAddr_.ToString(), dlname_.ToString(),
				 base.DumpOptionalParams());
		}
	}
}
