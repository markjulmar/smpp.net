using System;

namespace JulMar.Smpp.Elements
{
	/// <summary>
	/// The system_id parameter is used to identify an ESME or MC at bind time.  An ESME
	/// system_id identifies the ESME or ESME agent to the MC.  The MC system_id provides an 
	/// </summary>
	public class system_id : SmppCOctetString
	{
		// Constants
		private const int MAX_LENGTH = 16;

		/// <summary>
		/// Default constructor
		/// </summary>
		public system_id() : base("Smpp.NET")
		{
		}

		/// <summary>
		/// Parameterized constructor
		/// </summary>
		/// <param name="sid">System id</param>
		public system_id(string sid) : base(sid)
		{
		}

		/// <summary>
		/// Data validation support
		/// </summary>
		protected override void ValidateData()
		{
			if (Value.Length == 0 || Value.Length > MAX_LENGTH)
				throw new System.ArgumentOutOfRangeException("The system_id must be between 1 and " + MAX_LENGTH.ToString() + " characters.");
		}
	}
}
