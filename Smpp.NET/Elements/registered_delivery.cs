using System;
using System.Text;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Elements
{
	/// <summary>
	/// The SMSC Delivery Receipt
	/// </summary>
	public enum DeliveryReceiptType : byte
	{
		/// <summary>
		/// No SMSC Delivery receipt requested (default).
		/// </summary>
		NONE						    = 0x0,
		/// <summary>
		/// SMSC delivery receipt requested on final delivery (success or fail).
		/// </summary>
		FINAL_DELIVERY_RECEIPT		    = 0x1,
		/// <summary>
		/// SMSC delivery receipt requested when delivery fails.
		/// </summary>
		FINAL_DELIVERY_FAIL_RECEIPT	    = 0x2,
        /// <summary>
        /// SMSC delivery receipt requested when delivery is succesful.
        /// </summary>
        FINAL_DELIVERY_SUCCESS_RECEITP  = 0x3
	}

	/// <summary>
	/// The SME originated Acknowledgement.
	/// </summary>
	public enum AcknowledgementType : byte
	{
		/// <summary>
		/// No recipient SME acknowledgement requested (default).
		/// </summary>
		NONE						= 0x0,
		/// <summary>
		/// SME Delivery acknowledgement requested
		/// </summary>
		DELIVERY_ACK_REQUEST		= 0x4,
		/// <summary>
		/// SME Manual/User acknowledgement requested
		/// </summary>
		USER_ACK_REQUEST			= 0x8,
		/// <summary>
		/// Both Delivery and Manual/User acknowledgement requested
		/// </summary>
		DELIVERY_USER_ACK_REQUEST	= 0xC
	}

	/// <summary>
	/// The registered_delivery parameter is used to request an SMSC delivery receipt and/or SME
	/// originated acknowledgements.
	/// </summary>
	public class registered_delivery : SmppByte
	{
		// Constants
		private const byte DELIVERY_RECEIPT_MASK	= 0x3;	// [xxxxxxNN]
		private const byte ORIGINATION_ACK_MASK		= 0xC;	// [xxxxNNxx]
		private const byte INTERMEDIATE_ACK_MASK	= 0x10;	// [xxxNxxxx]

		/// <summary>
		/// Default constructor
		/// </summary>
		public registered_delivery() : this(DeliveryReceiptType.NONE, AcknowledgementType.NONE, false)
		{
		}

		/// <summary>
		/// Parameterized constructor
		/// </summary>
		/// <param name="dtype">Delivery receipt type</param>
		/// <param name="atype">Acknowledgement receipt type</param>
		/// <param name="intermediateNotification">Whether intermediate notification is requested.</param>
		public registered_delivery(DeliveryReceiptType dtype, AcknowledgementType atype, bool intermediateNotification) : base(0)
		{
			this.DeliveryReceipt = dtype;
			this.OriginationAcknowledgement = atype;
			this.IntermediateNotification = intermediateNotification;
		}

		/// <summary>
		/// This property returns the delivery receipt portion of the class.
		/// </summary>
		public DeliveryReceiptType DeliveryReceipt
		{
			get { return (DeliveryReceiptType) (Value & DELIVERY_RECEIPT_MASK); }
			set 
			{ 
				int inData = (int) value;
				if ((inData & (int)DELIVERY_RECEIPT_MASK) != inData)
					throw new ArgumentOutOfRangeException("registered_delivery.DeliveryReceipt not valid.");

				int data = Value;
				data &= ~((int)DELIVERY_RECEIPT_MASK);
				data |= (inData & (int)DELIVERY_RECEIPT_MASK);
				Value = (byte) (data & 0xff);
			}
		}

		/// <summary>
		/// This property returns the message features portion of the class.
		/// </summary>
		public AcknowledgementType OriginationAcknowledgement
		{
			get { return (AcknowledgementType) (Value & ORIGINATION_ACK_MASK); }
			set 
			{ 
				int inData = (int) value;
				if ((inData & (int)ORIGINATION_ACK_MASK) != inData)
					throw new ArgumentOutOfRangeException("registered_delivery.OriginationAcknowledgement not valid.");

				int data = Value;
				data &= ~((int)ORIGINATION_ACK_MASK);
				data |= (inData & (int)ORIGINATION_ACK_MASK);
				Value = (byte) (data & 0xff);
			}
		}

		/// <summary>
		/// This property returns the message features portion of the class.
		/// </summary>
		public bool IntermediateNotification
		{
			get { return ((Value & INTERMEDIATE_ACK_MASK) == INTERMEDIATE_ACK_MASK) ? true : false; }
			set 
			{ 
				if (value == true)
					Value |= (int)INTERMEDIATE_ACK_MASK;
				else
				{
					int thisValue = Value;
					thisValue &= ~((int)INTERMEDIATE_ACK_MASK);
					Value = (byte) (thisValue & 0xff);
				}
			}
		}

		/// <summary>
		/// Override of the object.ToString method.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendFormat("registered_delivery={0:X} DeliveryReceipt=", Value);

			switch (this.DeliveryReceipt)
			{
				case DeliveryReceiptType.NONE:
					sb.Append("None");
					break;
				case DeliveryReceiptType.FINAL_DELIVERY_RECEIPT:
					sb.Append("Always");
					break;
				case DeliveryReceiptType.FINAL_DELIVERY_FAIL_RECEIPT:
					sb.Append("OnFailure");
					break;
			}

			sb.Append(",OriginationAck=");
			switch (this.OriginationAcknowledgement)
			{
				case AcknowledgementType.NONE:
					sb.Append("None");
					break;
				case AcknowledgementType.DELIVERY_ACK_REQUEST:
					sb.Append("Delivery");
					break;
				case AcknowledgementType.USER_ACK_REQUEST:
					sb.Append("Manual/User");
					break;
				case AcknowledgementType.DELIVERY_USER_ACK_REQUEST:
					sb.Append("Delivery & Manual/User");
					break;
			}

			sb.AppendFormat(",IntermediateNotification={0}", this.IntermediateNotification);
			return sb.ToString();
		}
	}
}
