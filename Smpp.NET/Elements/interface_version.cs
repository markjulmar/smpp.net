using System;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Elements
{
	/// <summary>
	/// The interface_version parameter is used to indicate the version of the SMPP
	/// protocol supported.
	/// </summary>
	public class interface_version : SmppByte
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		public interface_version() : base(SmppVersion.SMPP_V34)
		{
		}

		/// <summary>
		/// Parameterized constructor
		/// </summary>
		public interface_version(byte ver) : base(ver)
		{
		}

		/// <summary>
		/// Data validation support
		/// </summary>
		protected override void ValidateData()
		{
			if (Value > SmppVersion.SMPP_V33 && Value != SmppVersion.SMPP_V34 && Value != SmppVersion.SMPP_V50)
				throw new System.ArgumentOutOfRangeException("The interface_version is not valid.");
		}
	}
}
