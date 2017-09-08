using System;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Elements
{
	/// <summary>
	/// MS status
	/// </summary>
	public enum MSStatus
	{
		/// <summary>
		/// Available (Default)
		/// </summary>
		DEFAULT		= 0x0,
		/// <summary>
		/// MS is available
		/// </summary>
		AVAILABLE	= 0x0,
		/// <summary>
		/// MS is suspended, no SMS capability, etc.
		/// </summary>
		DENIED		= 0x1,
		/// <summary>
		/// MS is not available.
		/// </summary>
		UNAVAILABLE = 0x2
	}

	/// <summary>
	/// The ms_availability_status parameter is used in the alert_notification operation to 
	/// indicate the availability state of the MS to the ESME.  If the SMSC does not include this
	/// parameter, then the ESME should assume that the MS is in an "available" state.
	/// </summary>
	public class ms_availability_status : TlvByte
	{
		/// <summary>
		/// SMPP element tag
		/// </summary>
		public const short TlvTag = ParameterTags.TAG_SET_DPF;

		/// <summary>
		/// Default constructor
		/// </summary>
		public ms_availability_status() : base(TlvTag)
		{
		}

		/// <summary>
		/// Parameterized constructor
		/// </summary>
		public ms_availability_status(MSStatus state) : base(TlvTag)
		{
			this.Value = state;
		}

		/// <summary>
		/// Override of property value
		/// </summary>
		public new MSStatus Value
		{
			get { return (MSStatus) base.Value; }
			set { base.Value = (byte) value; }
		}

		/// <summary>
		/// Override of the object.ToString method.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			string s = (HasValue) ? Value.ToString() : "";
			return string.Format("{0}{1}", base.ToString(), s);
		}
	}
}
