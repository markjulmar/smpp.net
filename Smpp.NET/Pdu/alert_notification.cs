using System;
using System.Text;
using JulMar.Smpp.Elements;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Pdu {
	/// <summary>
	/// The alert_notification operation is sent by the SMSC to the ESME when the SMSC has
	/// detected that a particular mobile subscriber has become available and a delivery pending
	/// flag had been set for that subscriber by the data_sm PDU.  This may be used to trigger
	/// a data content 'push' to the subscriber from a WAP proxy server.
	/// </summary>
	public class alert_notification : SmppRequest {
		// Class data - required parameters
		private address saddr_ = new address();
		private address esmeaddr_ = new address();
		// Optional parameters
		private ms_availability_status msavail_ = new ms_availability_status();

		/// <summary>
		/// Default constructor
		/// </summary>
		public alert_notification()
			: base(Commands.ALERT_NOTIFICATION) {
		}

		/// <summary>
		/// Primary constructor for the alert_notification PDU
		/// </summary>
		/// <param name="saddr">Address of the MS which has changed state.</param>
		/// <param name="esmeaddr">Address of the ESME which requested an alert.</param>
		/// <param name="status">Status of the MS</param>
		public alert_notification(address saddr, address esmeaddr, MSStatus status)
			: this() {
			this.SourceAddress = saddr;
			this.ESMEAddress = esmeaddr;
			this.MSAvailability = status;
		}

		/// <summary>
		/// This propery returns whether a response is required for this PDU.
		/// </summary>
		/// <value>True/False</value>
		public override bool RequiresResponse {
			get { return false; }
		}

		/// <summary>
		/// Address of the MS which has become available.
		/// </summary>
		public address SourceAddress {
			get { return saddr_; }
			set { saddr_ = value; }
		}

		/// <summary>
		/// Address of the ESME which requested an alert on the particular MS
		/// becoming available.
		/// </summary>
		public address ESMEAddress {
			get { return esmeaddr_; }
			set { esmeaddr_ = value; }
		}

		/// <summary>
		/// Returns whether the MS availability is present.
		/// </summary>
		public bool HasMSAvailability {
			get { return msavail_.HasValue; }
		}

		/// <summary>
		/// The status of the mobile station.
		/// </summary>
		public MSStatus MSAvailability {
			get { return msavail_.Value; }
			set { msavail_.Value = value; }
		}

		/// <summary>
		/// This method implements the ISupportSmppByteStream.AddToStream
		/// method so that the PDU can serialize itself to the data stream.
		/// </summary>
		/// <param name="writer">StreamWriter</param>
		public override void AddToStream(SmppWriter writer) {
			writer.Add(saddr_);
			writer.Add(esmeaddr_);
		}

		/// <summary>
		/// This method implements the ISupportSmppByteStream.GetFromStream
		/// method so that the PDU can serialize itself from the data stream.
		/// </summary>
		/// <param name="reader">StreamReader</param>
		public override void GetFromStream(SmppReader reader) {
			reader.ReadObject(saddr_);
			reader.ReadObject(esmeaddr_);
		}

		/// <summary>
		/// Override of the ToString method for debugging
		/// </summary>
		/// <returns>string</returns>
		public override string ToString() {
			return string.Format("alert_notification: {0},src_{1},esme_{2}{3}",
				base.ToString(), saddr_, esmeaddr_, base.DumpOptionalParams());
		}
	}
}
