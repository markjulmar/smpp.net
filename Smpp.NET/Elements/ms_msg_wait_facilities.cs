using System;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Elements
{
	/// <summary>
	/// Message waiting types
	/// </summary>
	public enum MWTypes : byte
	{
		/// <summary>
		/// Voicemail message waiting
		/// </summary>
		VOICEMAIL	= 0,
		/// <summary>
		/// Fax message waiting
		/// </summary>
		FAX			= 1,
		/// <summary>
		/// Electronic mail message waiting
		/// </summary>
		EMSG		= 2,
		/// <summary>
		/// Other message waiting
		/// </summary>
		OTHER		= 3,
		/// <summary>
		/// Unknown
		/// </summary>
		UNKNOWN		= 0x7f
	}
	
	/// <summary>
	/// The ms_msg_wait_facilities parameter allows an indication to be provided to an MS that
	/// there are messages waiting for the subscriber on systems on the PLMN.  This indication
	/// can be an icon on the MS screen or some other MMI indication.  This parameter can also
	/// specify the type of messages associated with the message waiting indicator.
	/// </summary>
	public class ms_msg_wait_facilities : TlvByte
	{
		// Constants
		private const int MWI_ACTIVE_MASK = 0x80;

		/// <summary>
		/// SMPP element tag
		/// </summary>
		public const short TlvTag = ParameterTags.TAG_MS_MSG_WAIT_FACIL;

		/// <summary>
		/// Default constructor
		/// </summary>
		public ms_msg_wait_facilities() : base(TlvTag)
		{
		}

		/// <summary>
		/// Parameterized constructor
		/// </summary>
		public ms_msg_wait_facilities(MWTypes type) : base(TlvTag)
		{
			base.Value = (byte) ((byte)type | MWI_ACTIVE_MASK);
		}

		/// <summary>
		/// This property returns whether the Message Waiting Indicator (MWI) is turned on.
		/// </summary>
		public bool IsActive
		{
			get { return ((base.Value & MWI_ACTIVE_MASK) == MWI_ACTIVE_MASK); }
			set 
			{ 
				if (value == true)
					base.Value |= MWI_ACTIVE_MASK;
				else
				{
					int iValue = base.Value;
					iValue &= ~MWI_ACTIVE_MASK;
					base.Value = (byte) (iValue & 0xff);
				}
			}
		}

		/// <summary>
		/// This property returns the current message type.
		/// </summary>
		public new MWTypes Value
		{
			get	{ return (IsActive) ? (MWTypes) ((int)base.Value & ~MWI_ACTIVE_MASK) : MWTypes.UNKNOWN; }
			set { base.Value = (byte) ((int)value | MWI_ACTIVE_MASK);	}
		}
	}
}
