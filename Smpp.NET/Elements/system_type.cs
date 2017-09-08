using System;

namespace JulMar.Smpp.Elements
{
	/// <summary>
	/// The system_type parameter is used to categorize the type of ESME that is binding to
	/// the MC.  Examples include "VMS" (voicemail system) and "OTA" (over-the-air-activation).
	/// Some MC's might not require the system_type - in these cases it may be null.
	/// </summary>
	public class system_type : SmppCOctetString
	{
		// Constants
		private const int MAX_LENGTH = 13;

		/// <summary>
		/// Default constructor
		/// </summary>
		public system_type() : base("")
		{
		}

		/// <summary>
		/// Parameterized constructor
		/// </summary>
		/// <param name="systemType">System Type</param>
		public system_type(string systemType) : base(systemType)
		{
		}

		/// <summary>
		/// Data validation support
		/// </summary>
		protected override void ValidateData()
		{
			if (Value.Length > MAX_LENGTH)
				throw new System.ArgumentOutOfRangeException("The system_type must be between 0 and " + MAX_LENGTH.ToString() + " characters.");
		}

	}
}
