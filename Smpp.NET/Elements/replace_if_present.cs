using System;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Elements
{
	/// <summary>
	/// The replace_if_present parameter is used to indicate the version of the SMPP
	/// protocol supported.
	/// </summary>
	public class replace_if_present : SmppBool
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		public replace_if_present() : base(false)
		{
		}

		/// <summary>
		/// Parameterized constructor
		/// </summary>
		public replace_if_present(bool b) : base(b)
		{
		}
	}
}
