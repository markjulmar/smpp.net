using System;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Elements {
	/// <summary>
	/// The dest_addr_np_country parameter is used to carry E.164
	/// information related to the operator country code.
	/// </summary>
	public class dest_addr_np_country : TlvCOctetString {
		// Constants
		private const int MAX_LENGTH = 5;

		/// <summary>
		/// SMPP element tag
		/// </summary>
		public const short TlvTag = ParameterTags.TAG_DEST_ADDR_NP_COUNTRY;

		/// <summary>
		/// Default constructor
		/// </summary>
		public dest_addr_np_country()
			: base(TlvTag) {
		}

		/// <summary>
		/// Parameterized constructor
		/// </summary>
		/// <param name="countryCode">Country code of the origination operator</param>
		public dest_addr_np_country(string countryCode)
			: base(TlvTag) {
			int value;
			if (Int32.TryParse(countryCode, out value) == false)
				throw new ArgumentOutOfRangeException("The countryCode parameter must be numeric.");
			this.Value = countryCode;
		}

		/// <summary>
		/// Data validation support
		/// </summary>
		protected override void ValidateData() {
			if (Value.Length > MAX_LENGTH)
				throw new System.ArgumentOutOfRangeException("The dest_addr_np_country cannot exceed " + MAX_LENGTH.ToString() + " characters.");
			else {
				int value;
				if (Int32.TryParse(Value, out value) == false)
					throw new ArgumentOutOfRangeException("The dest_addr_np_country parameter must be numeric.");
			}
		}
	}
}
