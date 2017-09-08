using System;
using System.Text;
using JulMar.Smpp.Elements;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Pdu {
	/// <summary>
	/// The enquire_sub operation is sent by the ESME to the SMSC to locate and return
	/// a specific subscriber/customer in the SMSC database.
	/// </summary>
	public class enquire_sub : SmppRequest {
		// Class data - required parameters
		private string id_ = "";

		/// <summary>
		/// Default constructor
		/// </summary>
		public enquire_sub()
			: base(Commands.QUERY_SUB) {
		}

		/// <summary>
		/// Primary constructor for the enquire_sub PDU
		/// </summary>
		public enquire_sub(string id)
			: this() {
			this.id_ = id;
		}

		/// <summary>
		/// The customer ID to delete
		/// </summary>
		public string CustomerID {
			get { return id_; }
			set { id_ = value; }
		}

		/// <summary>
		/// This method implements the ISupportSmppByteStream.AddToStream
		/// method so that the PDU can serialize itself to the data stream.
		/// </summary>
		/// <param name="writer">StreamWriter</param>
		public override void AddToStream(SmppWriter writer) {
			writer.Add(id_);
		}

		/// <summary>
		/// This method implements the ISupportSmppByteStream.GetFromStream
		/// method so that the PDU can serialize itself from the data stream.
		/// </summary>
		/// <param name="reader">StreamReader</param>
		public override void GetFromStream(SmppReader reader) {
			id_ = reader.ReadString();
		}

		/// <summary>
		/// Override of the ToString method for debugging
		/// </summary>
		/// <returns>string</returns>
		public override string ToString() {
			return string.Format("enquire_sub: {0},id={1}{2}",
				 base.ToString(), id_.ToString(), base.DumpOptionalParams());
		}
	}
}
