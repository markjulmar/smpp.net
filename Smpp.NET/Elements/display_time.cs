using System;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Elements
{
	/// <summary>
	/// Display time types
	/// </summary>
	public enum DisplayTimeType : byte
	{
		/// <summary>
		/// Temporary
		/// </summary>
		TEMPORARY	= 0x0,
		/// <summary>
		/// Default
		/// </summary>
		DEFAULT		= 0x1,
		/// <summary>
		/// Invoke
		/// </summary>
		INVOKE		= 0x2
	}

	/// <summary>
	/// The display_time parameter is used to associate a display time of the short message on the MS.
	/// </summary>
	public class display_time : TlvByte
	{
		/// <summary>
		/// SMPP element tag
		/// </summary>
		public const short TlvTag = ParameterTags.TAG_DISPLAY_TIME;

		/// <summary>
		/// Default constructor
		/// </summary>
		public display_time() : base(TlvTag)
		{
		}

		/// <summary>
		/// Parameterized constructor
		/// </summary>
		public display_time(DisplayTimeType type) : base(TlvTag)
		{
			this.Value = type;
		}

		/// <summary>
		/// Override property value
		/// </summary>
		public new DisplayTimeType Value
		{
			get { return (DisplayTimeType) base.Value; }
			set { base.Value = (byte) value; }
		}

		/// <summary>
		/// This validates the data.
		/// </summary>
		protected override void ValidateData()
		{
			if (Value < DisplayTimeType.TEMPORARY || Value > DisplayTimeType.INVOKE)
				throw new ArgumentOutOfRangeException("The display_time value is not value.");
		}

		/// <summary>
		/// Override the object.ToString method.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			string s = (HasValue) ? Value.ToString() : "";
			return string.Format("{0}{1}", base.ToString(), s);
		}
	}
}
