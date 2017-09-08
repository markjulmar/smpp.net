using System;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Elements
{
	/// <summary>
	/// Enumerations for delivery failure types.
	/// </summary>
	public enum DeliveryFailure : byte
	{
		/// <summary>
		/// Delivery failed because the destination could not be reached.
		/// </summary>
		DESTINATION_UNAVAILABLE = 0x0,
		/// <summary>
		/// Delivery failed because the destination was not valid.
		/// </summary>
		DESTINATION_INVALID		= 0x1,
		/// <summary>
		/// A permanent network error occurred during delivery.
		/// </summary>
		PERMANENT_NETWORK_ERROR = 0x2,
		/// <summary>
		/// A temporary network error occurred during delivery.
		/// </summary>
		TEMPORARY_NETWORK_ERROR = 0x3
	}

	/// <summary>
	/// The delivery_failure_reason parameter is used to indicate the outcome of a message
	/// delivery attempt.  It will not be included if the attempt was succesful.
	/// </summary>
	public class delivery_failure_reason : TlvByte
	{
		/// <summary>
		/// SMPP element tag
		/// </summary>
		public const short TlvTag = ParameterTags.TAG_DELIVERY_FAIL_REASON;

		/// <summary>
		/// Default constructor
		/// </summary>
		public delivery_failure_reason() : base(TlvTag)
		{
		}

		/// <summary>
		/// Parameterized constructor
		/// </summary>
		public delivery_failure_reason(DeliveryFailure code) : base(TlvTag)
		{
			this.Value = code;
		}

		/// <summary>
		/// Returns the delivery failure
		/// </summary>
		public new DeliveryFailure Value
		{
			get { return (DeliveryFailure) base.Value; }
			set { base.Value = (byte) value; }
		}

		/// <summary>
		/// Data validation support
		/// </summary>
		protected override void ValidateData()
		{
			if (Value < DeliveryFailure.DESTINATION_UNAVAILABLE || Value > DeliveryFailure.TEMPORARY_NETWORK_ERROR)
				throw new System.ArgumentOutOfRangeException("The delivery_failure_reason is not valid.");
		}

		/// <summary>
		/// Override of the object.ToString method.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return (HasValue) ? 
				string.Format("{0}{1}", base.ToString(), Value) : 
				base.ToString();
		}
	}
}
