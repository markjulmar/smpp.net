using System;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Elements
{
	/// <summary>
	/// The source_network_type parameter is used to indicate the type of network where the message
	/// originated.
	/// </summary>
	public class source_network_type : TlvByte
	{
		/// <summary>
		/// SMPP element tag
		/// </summary>
		public const short TlvTag = ParameterTags.TAG_SOURCE_NETWORK_TYPE;

		/// <summary>
		/// Default constructor
		/// </summary>
		public source_network_type() : base(TlvTag)
		{
		}

		/// <summary>
		/// Parameterized constructor
		/// </summary>
		public source_network_type(NetworkType type) : base(TlvTag)
		{
			this.Value = type;
		}

		/// <summary>
		/// Override of property value
		/// </summary>
		public new NetworkType Value
		{
			get { return (NetworkType) base.Value; }
			set { base.Value = (byte) value; }
		}

		/// <summary>
		/// Data validation support
		/// </summary>
		protected override void ValidateData()
		{
			if (Value > NetworkType.PAGING_NW)
				throw new System.ArgumentOutOfRangeException("The source_network_type is not valid.");
		}

		/// <summary>
		/// Override of the object.ToString method
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			string s = (HasValue) ? Value.ToString() : "";
			return string.Format("{0}{1}", base.ToString(), s);
		}
	}
}
