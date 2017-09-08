using System;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Elements
{
	/// <summary>
	/// The set_dpf parameter is used to request the setting of a delivery pending flag (DPF)
	/// for certain delivery failure scenerios.  The SMSC should response to such a request with an
	/// alert_notification PDU when it detects that the destination MS has become available.
	/// </summary>
	public class set_dpf : TlvByte
	{
		/// <summary>
		/// SMPP element tag
		/// </summary>
		public const short TlvTag = ParameterTags.TAG_SET_DPF;

		/// <summary>
		/// Default constructor
		/// </summary>
		public set_dpf() : base(TlvTag)
		{
		}

		/// <summary>
		/// Parameterized constructor
		/// </summary>
		public set_dpf(bool on) : base(TlvTag)
		{
			this.Value = on;
		}

		/// <summary>
		/// Returns the value as a boolean
		/// </summary>
		public new bool Value
		{
			get { return (base.Value == 1) ? true : false; }
			set { base.Value = (value == true) ? (byte)1 : (byte)0; }
		}
	}
}
