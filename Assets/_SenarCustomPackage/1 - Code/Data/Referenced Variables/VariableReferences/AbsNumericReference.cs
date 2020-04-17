namespace SenarCustomSystem.Variables
{
	using Sirenix.OdinInspector;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using System;
    using SenarCustomSystem.Variables.Interface;
    using SenarCustomSystem.Variables.Abstract;
    using System.Linq;
    using Sirenix.Serialization;


    //TODO: implementare dei metodi per ottenere una lista aggiornata con i possibili scriptable assegnabili se si usano le interface
    
	//[System.Serializable] Don't use this to avoid Odin and unity serialization bug, let odin serialize all
	public abstract class AbsNumericReference : IDoubleValue, IFloatValue, IIntValue, ILongValue, IDecimalValue
	{
		public abstract bool CanSetValue { get; }

		public abstract double DoubleValue { get; set; }
		public abstract float FloatValue { get; set; }
		public abstract int IntValue { get; set; }
		public abstract long LongValue { get; set; }
		public abstract decimal DecimalValue { get; set; }



		#region CONVERSION METHOD
		protected float ConvertToFloat<T>(T value) where T : IComparable
		{
			try
			{
				return Convert.ToSingle(value);
			}
			catch (OverflowException)
			{
				if (value.CompareTo(0) >= 0)
				{
					return float.MaxValue;
				}
				else
				{
					return float.MinValue;
				}
			}
		}

		protected decimal ConvertToDecimal<T>(T value) where T : IComparable
		{
			try
			{
				return Convert.ToDecimal(value);
			}
			catch (OverflowException)
			{
				if (value.CompareTo(0) >= 0)
				{
					return decimal.MaxValue;
				}
				else
				{
					return decimal.MinValue;
				}
			}
		}

		protected int ConvertToInt<T>(T value) where T : IComparable
		{
			try
			{
				return Convert.ToInt32(value);
			}
			catch (OverflowException)
			{
				if (value.CompareTo(0) >= 0)
				{
					return int.MaxValue;
				}
				else
				{
					return int.MinValue;
				}
			}
		}

		protected long ConvertToLong<T>(T value) where T : IComparable
		{
			try
			{
				return Convert.ToInt64(value);
			}
			catch (OverflowException)
			{
				if (value.CompareTo(0) >= 0)
				{
					return long.MaxValue;
				}
				else
				{
					return long.MinValue;
				}
			}
		}

		protected double ConvertToDouble<T>(T value) where T : IComparable
		{
			try
			{
				return Convert.ToDouble(value);
			}
			catch (OverflowException)
			{
				if (value.CompareTo(0) >= 0)
				{
					return double.MaxValue;
				}
				else
				{
					return double.MinValue;
				}
			}
		}

		#endregion

	}
}