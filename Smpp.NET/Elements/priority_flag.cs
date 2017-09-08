using System;
using System.Text;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Elements
{
	/// <summary>
	/// The message priority indicates the requested priority from the ESME to the SMSC.
	/// </summary>
	public enum MessagePriority : byte
	{
		/// <summary>
		/// Non-priority (bulk)
		/// </summary>
		NORMAL		= 0,
		/// <summary>
		/// Interactive
		/// </summary>
		INTERACTIVE = 1,
		/// <summary>
		/// Urgent
		/// </summary>
		URGENT		= 2,
		/// <summary>
		/// Emergency (very urgent)
		/// </summary>
		EMERGENCY	= 3,
        /// <summary>
        /// Background priority (GSM only)
        /// </summary>
        BACKGROUND  = 4
	}

	/// <summary>
	/// The priority_flag parameter allows the originating SME to assign a priority level
	/// to the short message.
	/// </summary>
	public class priority_flag : SmppByte
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		public priority_flag() : base(0)
		{
		}

		/// <summary>
		/// Parameterized constructor
		/// </summary>
		public priority_flag(MessagePriority priority) : base((byte)priority)
		{
		}

		/// <summary>
		/// Data validation support
		/// </summary>
		protected override void ValidateData()
		{
			if (Value > MessagePriority.EMERGENCY)
				throw new System.ArgumentOutOfRangeException("The priority_flag is not valid.");
		}

		/// <summary>
		/// Override of property value
		/// </summary>
		public new MessagePriority Value
		{
			get { return (MessagePriority) base.Value; }
			set { base.Value = (byte) value; }
		}

		/// <summary>
		/// Override of the object.ToString method.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendFormat("priority_flag={0:X} ", Value);
			switch ((MessagePriority)Value)
			{
				case MessagePriority.NORMAL:
					sb.Append("Normal");
					break;
				case MessagePriority.INTERACTIVE:
					sb.Append("Interactive");
					break;
				case MessagePriority.URGENT:
					sb.Append("Urgent");
					break;
				case MessagePriority.EMERGENCY:
					sb.Append("Emergency");
					break;
				default:
					break;
			}
			return sb.ToString();
		}
	}
}
