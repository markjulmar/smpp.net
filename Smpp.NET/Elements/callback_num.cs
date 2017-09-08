using System;
using System.Text;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Elements
{
	/// <summary>
	/// The digit modes for callback numbers.
	/// </summary>
	public enum DigitMode
	{
		/// <summary>
		/// BCD
		/// </summary>
		TBCD  = 0x0,
		/// <summary>
		/// ASCII
		/// </summary>
		ASCII = 0xD,
		/// <summary>
		/// Unknown
		/// </summary>
		UNKNOWN = 0xFF
	}

	/// <summary>
	/// The callback_num parameter associates a callback number with the message.  In TDMA
	/// networks, it is possible to send and receive multiple callback numbers to/from TDMA mobile
	/// stations.
	/// </summary>
	public class callback_num : TlvParameter
	{
		// Constants
		private const byte DIGITMODE_MASK  = 0xD;

		/// <summary>
		/// SMPP element tag
		/// </summary>
		public const short TlvTag = ParameterTags.TAG_CALLBACK_NUM;

		/// <summary>
		/// Default constructor
		/// </summary>
		public callback_num() : base(TlvTag)
		{
		}

		/// <summary>
		/// Parameterized constructor
		/// </summary>
		public callback_num(DigitMode digitMode, TypeOfNumber typeOfNumber, NumericPlanIndicator numberPlan, string digits) : base(TlvTag)
		{
            SmppWriter writer = new SmppWriter(Data, true);
            writer.Add((byte)digitMode);
            writer.Add((byte)typeOfNumber);
            writer.Add((byte)numberPlan);
            writer.Add(digits, false);
        }

		/// <summary>
		/// Returns the digit mode indicator
		/// </summary>
		public DigitMode DigitModeIndicator
		{
			get	
            { 
                return (HasValue) ? 
                    (DigitMode) Data.GetBuffer()[0] : 
                    DigitMode.UNKNOWN;	
            }
			
			set 
			{
                SmppWriter writer = new SmppWriter(Data, true);
				writer.Add((byte)value);
			}
		}

		/// <summary>
		/// Returns the type of number
		/// </summary>
		public TypeOfNumber TypeOfNumber
		{
			get
			{
				return (HasValue && Data.Length > 1) ?
					(TypeOfNumber) Data.GetBuffer()[1] :
				        TypeOfNumber.UNKNOWN;
			}

			set
			{
                SmppWriter writer = new SmppWriter(Data, true);
                writer.Skip(1);
                writer.Add((byte)value);
			}
		}

		/// <summary>
		/// Returns the numbering plan indicator
		/// </summary>
		public NumericPlanIndicator NumberingPlanIndicator
		{
			get
			{
				if (HasValue && Data.Length > 2)
					return (NumericPlanIndicator) Data.GetBuffer()[2];
				return NumericPlanIndicator.UNKNOWN;
			}

			set
			{
                SmppWriter writer = new SmppWriter(Data, true);
                writer.Skip(2);
                writer.Add((byte)value);
			}
		}

		/// <summary>
		/// This returns the digits from the callback number
		/// </summary>
		public string Digits
		{
			get
			{
				if (HasValue && Data.Length > 3)
				{
                    SmppReader reader = new SmppReader(Data, true);
                    reader.Skip(3);
                    return reader.ReadString();
				}
				return "";
			}

			set
			{
                SmppWriter writer = new SmppWriter(Data, true);
                writer.Skip(3);
				writer.Add(value, false);
			}
		}

		/// <summary>
		/// Override for object.ToString
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendFormat("{0} DigitMode={1},", base.ToString(), DigitModeIndicator);
			sb.AppendFormat("ton={0},",this.TypeOfNumber);
			sb.AppendFormat("npi={0},",this.NumberingPlanIndicator);
			sb.AppendFormat("Digits=\"{0}\"", this.Digits);
			return sb.ToString();
		}
	}
}
