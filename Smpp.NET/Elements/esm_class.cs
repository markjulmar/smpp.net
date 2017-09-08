using System;
using System.Text;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Elements
{
		
	/// <summary>
	/// The message mode indicates the mode that the SMSC should use to submit the message.
	/// </summary>
	public enum MessagingMode : byte
	{
		/// <summary>
		/// Default SMSC Mode (e.g. store and forward).
		/// </summary>
		DEFAULT_MODE		= 0x0,
		/// <summary>
		/// Datagram mode.
		/// </summary>
		DATAGRAM_MODE		= 0x1,
		/// <summary>
		/// Forward (i.e. Transaction) mode.
		/// </summary>
		FORWARD_MODE		= 0x2,
		/// <summary>
		/// Store and Forward mode (used to select Store and Forward mode if Default SMSC mode
		/// is not store and forward).
		/// </summary>
		STORE_FORWARD_MODE	= 0x3
	}

	/// <summary>
	/// The message type indicator.
	/// </summary>
	public enum MessageType : byte
	{
		/// <summary>
		/// Default message type (i.e. normal message).
		/// </summary>
		DEFAULT_MSG_TYPE	= 0x0,
		/// <summary>
		/// Short message contains SMSC Delivery Receipt
		/// </summary>
		SMSC_DELIVERY_RCPT	= 0x4,
		/// <summary>
		/// Short message contains ESME Delivery acknowledgement
		/// </summary>
		ESME_DELIVERY_ACK	= 0x8,
		/// <summary>
		/// Short message contains ESME Manual/User acknowledgement
		/// </summary>
		ESME_USER_ACK		= 0x10,
		/// <summary>
		/// Short message contains Conversion Abort (Korean CDMA)
		/// </summary>
		CONVERSION_ABORT	= 0x18,
		/// <summary>
		/// Short message contains Intermediate Delivery Notification
		/// </summary>
		INT_DELIVERY_NOTIFY	= 0x20
	}

	/// <summary>
	/// The GSM network specific features.
	/// </summary>
	public enum GSMNetworkFeatures : byte
	{
		/// <summary>
		/// No specific features selected
		/// </summary>
		NO_FEATURES_SEL		= 0x0,
		/// <summary>
		/// UDHI Indicator (only relevant for MT short messages)
		/// </summary>
		UDHI_INDICATOR		= 0x40,
		/// <summary>
		/// Set Reply Path (only relevant for GSM networks)
		/// </summary>
		SET_REPLY_PATH		= 0x80,
		/// <summary>
		/// Set UDHI and Reply Path (only relevant for GSM networks)
		/// </summary>
		UDHI_REPLY_PATH		= 0xC0,
	}

	/// <summary>
	/// The esm_class parameter is used to indicate the special message attributes associated 
	/// with a short message.
	/// </summary>
	public class esm_class : SmppByte
	{
		// Constants
		private const byte MESSAGE_MODE_MASK	= 0x3;	// [xxxxxxNN]
		private const byte MESSAGE_TYPE_MASK	= 0x3C;	// [xxNNNNxx]
		private const byte GSM_FEATURES_MASK	= 0xC0;	// [NNxxxxxx]

		/// <summary>
		/// Default constructor
		/// </summary>
		public esm_class() : base(0)
		{
		}

		/// <summary>
		/// Parameterized constructor
		/// </summary>
		public esm_class(MessagingMode mode, MessageType type) : this(mode,type,GSMNetworkFeatures.NO_FEATURES_SEL)
		{
		}

		/// <summary>
		/// Parameterized constructor
		/// </summary>
		public esm_class(MessagingMode mode, MessageType type, GSMNetworkFeatures features) : base((byte)((int)mode|(int)type|(int)features))
		{
		}

		/// <summary>
		/// This property returns the message mode portion of the class.
		/// </summary>
		public MessagingMode MessageMode
		{
			get { return (MessagingMode) (Value & MESSAGE_MODE_MASK); }
			set 
			{ 
				int inData = (int) value;
				if ((inData & (int)MESSAGE_MODE_MASK) != inData)
					throw new ArgumentOutOfRangeException("esm_class.MessageMode not valid.");

				int data = Value;
				data &= ~((int)MESSAGE_MODE_MASK);
				data |= (inData & (int)MESSAGE_MODE_MASK);
				Value = (byte) (data & 0xff);
			}
		}

		/// <summary>
		/// This property returns the message type portion of the class.
		/// </summary>
		public MessageType MessageType
		{
			get { return (MessageType) (Value & MESSAGE_TYPE_MASK); }
			set 
			{ 
				int inData = (int) value;
				if ((inData & (int)MESSAGE_TYPE_MASK) != inData)
					throw new ArgumentOutOfRangeException("esm_class.MessageType not valid.");

				int data = Value;
				data &= ~((int)MESSAGE_TYPE_MASK);
				data |= (inData & (int)MESSAGE_TYPE_MASK);
				Value = (byte) (data & 0xff);
			}
		}

		/// <summary>
		/// This property returns the message features portion of the class.
		/// </summary>
		public GSMNetworkFeatures MessageFeatures
		{
			get { return (GSMNetworkFeatures) (Value & GSM_FEATURES_MASK); }
			set 
			{ 
				int inData = (int) value;
				if ((inData & (int)GSM_FEATURES_MASK) != inData)
					throw new ArgumentOutOfRangeException("esm_class.MessageFeatures not valid.");

				int data = Value;
				data &= ~((int)GSM_FEATURES_MASK);
				data |= (inData & (int)GSM_FEATURES_MASK);
				Value = (byte) (data & 0xff);
			}
		}
	
		/// <summary>
		/// Override of the object.ToString method.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendFormat("esm_class={0:X} Mode=", Value);
			switch (MessageMode)
			{
				case MessagingMode.DATAGRAM_MODE:
					sb.Append("Datagram");
					break;
				case MessagingMode.FORWARD_MODE:
					sb.Append("Forward (Transaction)");
					break;
				default:
					sb.Append("Store/Forward");
					break;
			}
			sb.Append(",Type=");
			switch (MessageType)
			{
				case MessageType.DEFAULT_MSG_TYPE:
					sb.Append("Normal");
					break;
				case MessageType.SMSC_DELIVERY_RCPT:
					sb.Append("Delivery Receipt");
					break;
				case MessageType.ESME_DELIVERY_ACK:
					sb.Append("Delivery Ack");
					break;
				case MessageType.ESME_USER_ACK:
					sb.Append("User Ack");
					break;
				case MessageType.CONVERSION_ABORT:
					sb.Append("Conversion Abort");
					break;
				case MessageType.INT_DELIVERY_NOTIFY:
					sb.Append("Intermediate Deliver");
					break;
				default:
					sb.AppendFormat("Unknown ({0:X})", MessageType);
					break;
			}
			sb.Append(",Features=");
			switch (MessageFeatures)
			{
				case GSMNetworkFeatures.NO_FEATURES_SEL:
					sb.Append("None");
					break;
				case GSMNetworkFeatures.UDHI_INDICATOR:
					sb.Append("UDHI");
					break;
				case GSMNetworkFeatures.SET_REPLY_PATH:
					sb.Append("Set Reply Path");
					break;
				case GSMNetworkFeatures.UDHI_REPLY_PATH:
					sb.Append("UDHI, Set Reply Path");
					break;
			}
			return sb.ToString();
		}
	}
}
