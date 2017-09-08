using System;

namespace JulMar.Smpp.Elements
{
	/// <summary>
	/// The dl_name parameter is used to identify a distribution list provisioned on the SMSC.
	/// Distribution list names are defined by mutual agreement between the SMSC ad ESME.
	/// </summary>
	public class dl_name : SmppCOctetString
	{
		// Constants
		private const int MAX_LENGTH = 21;

		/// <summary>
		/// Default constructor
		/// </summary>
		public dl_name() : base("")
		{
		}

		/// <summary>
		/// Parameterized constructor
		/// </summary>
		/// <param name="name">dl_name</param>
		public dl_name(string name) : base(name)
		{
		}

		/// <summary>
		/// Data validation support
		/// </summary>
		protected override void ValidateData()
		{
			if (Value.Length > MAX_LENGTH)
				throw new System.ArgumentOutOfRangeException("The dl_name must be between 0 and " + MAX_LENGTH.ToString() + " characters.");
		}

		/// <summary>
		/// Override of object.ToString
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return string.Format("dl_name=\"{0}\"", Value);
		}


	}
}
