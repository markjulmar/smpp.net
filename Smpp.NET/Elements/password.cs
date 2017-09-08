using System;

namespace JulMar.Smpp.Elements
{
	/// <summary>
	/// The password parameter is used by the MC to authenticate the identity of the binding ESME.
	/// The service provider may require ESME's to provide a password when binding to the MC; this
	/// password is generally issued by the system administrator.
	/// </summary>
	public class password : SmppCOctetString
	{
		// Constants
		private const int MAX_LENGTH = 9;

		/// <summary>
		/// Default constructor
		/// </summary>
		public password() : base("")
		{
		}

		/// <summary>
		/// Parameterized constructor
		/// </summary>
		/// <param name="pwd">password</param>
		public password(string pwd) : base(pwd)
		{
		}

		/// <summary>
		/// Data validation support
		/// </summary>
		protected override void ValidateData()
		{
			if (Value.Length > MAX_LENGTH)
				throw new System.ArgumentOutOfRangeException("The password must be between 0 and " + MAX_LENGTH.ToString() + " characters.");
		}

	}
}
