using System;
using JulMar.Smpp;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Elements
{
	/// <summary>
	/// Valid message states
	/// </summary>
	public enum MessageStatus : byte
	{
        /// <summary>
        /// The message is scheduled - delivery has not yet been
        /// initiated.
        /// </summary>
        SCHEDULED   = 0,
		/// <summary>
		/// Message is enroute - it is active within the MC.
		/// </summary>
		ENROUTE		= 1,
		/// <summary>
		/// Message has been delivered to the destination
		/// </summary>
		DELIVERED	= 2,
		/// <summary>
		/// Message validity period has expired.  The message
		/// failed to be delivered within its validity period and/or
		/// retry period.  No further attempts will be made.
		/// </summary>
		EXPIRED		= 3,
		/// <summary>
		/// Message has been deleted or canceled from the MC.
		/// </summary>
		DELETED		= 4,
		/// <summary>
		/// Message is undeliverable - it encountered a delivery
		/// error and is deemed permanently underliverable.  No
		/// further delivery attempts will be made.
		/// </summary>
		UNDELIVERABLE = 5,
		/// <summary>
		/// Message has been accepted by the SMSC.  This state
		/// generally indicates intervention on the MC side.
		/// </summary>
		ACCEPTED	= 6,
		/// <summary>
		/// Message is in an invalid state possibly due to some
		/// internal MC problem which may be intermediate or
		/// permenant.
		/// </summary>
		UNKNOWN		= 7,
		/// <summary>
		/// Message is in a rejected state.  The message has been
		/// rejected by a delivery interface.  No further delivery
		/// attempts will be made.
		/// </summary>
		REJECTED	= 8,
        /// <summary>
        /// Message was accepted but not transmitted or broadcast
        /// on the network.  It was deliberately ignored according
        /// to vendor or network-specific rules.  No further delivery
        /// attempts will be made.
        /// </summary>
        SKIPPED     = 9
	}

	/// <summary>
	/// The message_state parameter identifies the status for a short message.  This is returned
	/// from the SMSC to the ESME in the query_sm_resp PDU.
	/// </summary>
	public class message_state : SmppByte
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		public message_state() : this(MessageStatus.UNKNOWN)
		{
		}

		// Constants
		/// <summary>
		/// Parameterized constructor
		/// </summary>
		public message_state(MessageStatus state) : base((byte)state)
		{
		}

		/// <summary>
		/// Override of property value
		/// </summary>
		public new MessageStatus Value
		{
			get { return (MessageStatus) base.Value; }
			set { base.Value = (byte) value; }
		}

		/// <summary>
		/// Data validation support
		/// </summary>
		protected override void ValidateData()
		{
			if (Value < MessageStatus.ENROUTE || Value > MessageStatus.REJECTED)
				throw new System.ArgumentOutOfRangeException("The message_state is not valid.");
		}
	}

	/// <summary>
	/// The message_state optional parameter is used by the SMSC in the deliver_sm and data_sm
	/// PDUs to indicate to the ESME the final message state for an SMSC Delivery receipt.
	/// </summary>
	public class optional_message_state : TlvByte
	{
		/// <summary>
		/// SMPP element tag
		/// </summary>
		public const short TlvTag = ParameterTags.TAG_MESSAGE_STATE;

		/// <summary>
		/// Constructor
		/// </summary>
		public optional_message_state() : base(TlvTag)
		{
		}

		/// <summary>
		/// Parameterized constructor
		/// </summary>
		public optional_message_state(MessageStatus state) : base(TlvTag)
		{
			this.Value = state;
		}

		/// <summary>
		/// Data validation support
		/// </summary>
		protected override void ValidateData()
		{
			if (Value < MessageStatus.ENROUTE || Value > MessageStatus.REJECTED)
				throw new System.ArgumentOutOfRangeException("The message_state is not valid.");
		}

		/// <summary>
		/// Override of property value
		/// </summary>
		public new MessageStatus Value
		{
			get { return (MessageStatus) base.Value; }
			set { base.Value = (byte) value; }
		}

		/// <summary>
		/// Override of the object.ToString method
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			string s = (HasValue) ? Value.ToString() : "";
			return string.Format("{0}{1}", base.ToString(), s);
		}
	}
}
