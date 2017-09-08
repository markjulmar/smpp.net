using System;
using System.Text;
using JulMar.Smpp.Elements;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Pdu {
	/// <summary>
	/// The data_sm operation is used to transfer data between the SMSC and ESME.  It may be used
	/// by both the ESME and SMSC.  This command is an alternative to the submit_sm and deliver_sm
	/// commands.  It is introduced as a new command to be used by interactive applications such as
	/// those provided via a WAP framework.
	/// </summary>
	public class data_sm : SmppSubmitRequest {
		// Class data - required parameters
		private service_type stype_ = new service_type();
		private address saddr_ = new address();
		private address daddr_ = new address();
		private esm_class esmclass_ = new esm_class();
		private registered_delivery regDelivery_ = new registered_delivery();
		private data_coding dataCoding_ = new data_coding();

		/// <summary>
		/// Default constructor
		/// </summary>
		public data_sm()
			: base(Commands.DATA_SM) {
		}

		/// <summary>
		/// Primary constructor for the data_sm PDU
		/// </summary>
		/// <param name="stype">Service Type</param>
		/// <param name="saddr">Source Address</param>
		/// <param name="daddr">Destination Address</param>
		/// <param name="esm">ESM</param>
		/// <param name="regDelivery">Registered Delivery</param>
		/// <param name="dataCoding">Data Coding type</param>
		public data_sm(string stype, address saddr, address daddr, esm_class esm,
			registered_delivery regDelivery, DataEncoding dataCoding)
			: this() {
			this.ServiceType = stype;
			this.SourceAddress = saddr;
			this.DestinationAddress = daddr;
			this.EsmClass = esm;
			this.RegisteredDelivery = regDelivery;
			this.DataCoding = dataCoding;
		}

		/// <summary>
		/// Ths service_type parameter cna be used to indicate the SMS application service
		/// associated with the message.
		/// </summary>
		public string ServiceType {
			get { return stype_.Value; }
			set { stype_.Value = value; }
		}

		/// <summary>
		/// Address of SME which originated this message. If not known, set to null.
		/// </summary>
		public address SourceAddress {
			get { return saddr_; }
			set { saddr_ = value; }
		}

		/// <summary>
		/// Address of SME which will receive the message.
		/// </summary>
		public address DestinationAddress {
			get { return daddr_; }
			set { daddr_ = value; }
		}

		/// <summary>
		/// Indicates the message mode and type.
		/// </summary>
		public esm_class EsmClass {
			get { return esmclass_; }
			set { esmclass_ = value; }
		}

		/// <summary>
		/// Indicator to signify if an SMSC delivery receipt or SME acknowledgement is required.
		/// </summary>
		public registered_delivery RegisteredDelivery {
			get { return regDelivery_; }
			set { regDelivery_ = value; }
		}

		/// <summary>
		/// Defines the encoding scheme of the short message user data.
		/// </summary>
		public DataEncoding DataCoding {
			get { return dataCoding_.Value; }
			set { dataCoding_.Value = value; }
		}

		/// <summary>
		/// Up to 254 bytes of text message to send.
		/// </summary>
		public string Message {
			get { return msgPayload_.TextValue; }
			set { msgPayload_.TextValue = value; }
		}

		/// <summary>
		/// This retrieves the short message in a byte array
		/// </summary>
		public byte[] BinaryMessage {
			get { return msgPayload_.BinaryValue; }
			set { msgPayload_.BinaryValue = value; }
		}

		/// <summary>
		/// This method implements the ISupportSmppByteStream.AddToStream
		/// method so that the PDU can serialize itself to the data stream.
		/// </summary>
		/// <param name="writer">StreamWriter</param>
		public override void AddToStream(SmppWriter writer) {
			writer.Add(stype_);
			writer.Add(saddr_);
			writer.Add(daddr_);
			writer.Add(esmclass_);
			writer.Add(regDelivery_);
			writer.Add(dataCoding_);
		}

		/// <summary>
		/// This method implements the ISupportSmppByteStream.GetFromStream
		/// method so that the PDU can serialize itself from the data stream.
		/// </summary>
		/// <param name="reader">StreamReader</param>
		public override void GetFromStream(SmppReader reader) {
			reader.ReadObject(stype_);
			reader.ReadObject(saddr_);
			reader.ReadObject(daddr_);
			reader.ReadObject(esmclass_);
			reader.ReadObject(regDelivery_);
			reader.ReadObject(dataCoding_);
		}

		/// <summary>
		/// Override of the ToString method for debugging
		/// </summary>
		/// <returns>string</returns>
		public override string ToString() {
			return string.Format("data_sm: {0},svc_type={1},src_{2},dst_{3},{4},{5},{6}{7}",
				base.ToString(), stype_, saddr_, daddr_,
				esmclass_, regDelivery_, dataCoding_, base.DumpOptionalParams());
		}
	}
}
