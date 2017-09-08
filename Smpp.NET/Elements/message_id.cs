using System;

namespace JulMar.Smpp.Elements
{
	/// <summary>
	/// The message_id parameter is assigned by the SMSC to each submitted short message.
	/// It is an opaque value and is set according to the SMSC implementation.  It is returned by
	/// the SMSC in the submit_sm_resp, submit_multi_resp, deliver_sm_resp, and data_sm_resp PDUs
	/// and may be used by the ESME in subsequent SMPP operations related to the short message.
	/// </summary>
	public class message_id : SmppCOctetString
	{
		// Constants
		private const int MAX_LENGTH = 65;

		/// <summary>
		/// Default constructor
		/// </summary>
		public message_id() : base("")
		{
		}

		/// <summary>
		/// Parameterized constructor
		/// </summary>
		/// <param name="id">message_id</param>
		public message_id(string id) : base(id)
		{
		}

		/// <summary>
		/// Data validation support
		/// </summary>
		protected override void ValidateData()
		{
			if (Value.Length > MAX_LENGTH)
				throw new System.ArgumentOutOfRangeException("The message_id must be between 0 and " + MAX_LENGTH.ToString() + " characters.");
		}
	}
}
