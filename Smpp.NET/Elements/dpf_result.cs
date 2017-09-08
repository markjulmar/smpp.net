using System;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Elements
{
	/// <summary>
	/// The dpf_result parameter is used in the data_sm_resp PDU to indicate if delivery pending
	/// flag (DPF) was set for a delivery failure of the short message.  If the dpf_result element
	/// is not included in the data_sm_resp PDU, the ESME should assume that the DPF was not set.
	/// Currently, this parameter is only applicable for the Transaction message mode.
	/// </summary>
	public class dpf_result : TlvByte
	{
		/// <summary>
		/// SMPP element tag
		/// </summary>
		public const short TlvTag = ParameterTags.TAG_DPF_RESULT;

		/// <summary>
		/// Default constructor
		/// </summary>
		public dpf_result() : base(TlvTag)
		{
		}

		/// <summary>
		/// Parameterized constructor
		/// </summary>
		public dpf_result(bool on) : base(TlvTag)
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
