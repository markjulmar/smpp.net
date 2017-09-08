using System;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Elements
{
	/// <summary>
	/// Language types
	/// </summary>
	public enum Language 
	{
		/// <summary>
		/// Unspecified
		/// </summary>
		UNSPECIFIED	= 0,
		/// <summary>
		/// English
		/// </summary>
		ENGLISH		= 1,
		/// <summary>
		/// French
		/// </summary>
		FRENCH		= 2,
		/// <summary>
		/// Spanish
		/// </summary>
		SPANISH		= 3,
		/// <summary>
		/// German
		/// </summary>
		GERMAN		= 4,
		/// <summary>
		/// Portugese
		/// </summary>
		PORTUGESE	= 5
	}

	/// <summary>
	/// The language_indicator parameter is used to indicate the language of the short message.
	/// </summary>
	public class language_indicator : TlvByte
	{
		/// <summary>
		/// SMPP element tag
		/// </summary>
		public const short TlvTag = ParameterTags.TAG_LANGUAGE_INDICATOR;

		/// <summary>
		/// Default constructor
		/// </summary>
		public language_indicator() : base(TlvTag)
		{
		}

		/// <summary>
		/// Parameterized constructor
		/// </summary>
		public language_indicator(Language val) : base(TlvTag)
		{
			this.Value = val;
		}

		/// <summary>
		/// Override of property value
		/// </summary>
		public new Language Value
		{
			get { return (Language) base.Value; }
			set { base.Value = (byte) value; }
		}

		/// <summary>
		/// Override of the object.ToString method.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			string s = (HasValue) ? Value.ToString() : "";
			return string.Format("{0}{1}", base.ToString(), s);
		}
	}
}
