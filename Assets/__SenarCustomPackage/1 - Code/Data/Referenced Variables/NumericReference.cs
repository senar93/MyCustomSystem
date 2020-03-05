namespace MyCustomSystem.Variables
{
	using Sirenix.OdinInspector;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using System;
    using MyCustomSystem.Variables.Interface;
    using MyCustomSystem.Variables.Abstract;
    using System.Linq;

    //TODO: implementare dei metodi per ottenere una lista aggiornata con i possibili scriptable assegnabili

    [System.Serializable]
	public struct NumericReference
	{
		[EnumPaging, HideLabel, SerializeField]
		private Mode mode;


		#region VARIABLE
		[ShowIf("mode", Mode.Value),
		 ShowInInspector, Space(10)]
		private decimal _value;

		[ShowIf("mode", Mode.FloatRef),
		 Required("Reference is required!"),
		 InfoBox("Edit referenced variable here change it everywhere!", "IsNotFloatRefNull"),
		 InlineEditor, HideLabel, ShowInInspector, Space(10)]
		private IHaveValue<float> _floatRef;

		[ShowIf("mode", Mode.DoubleRef),
		 Required("Reference is required!"),
		 InfoBox("Edit referenced variable here change it everywhere!", "IsNotDoubleRefNull"),
		 InlineEditor, HideLabel, ShowInInspector, Space(10)]
		private IHaveValue<double> _doubleRef;

		[ShowIf("mode", Mode.DecimalRef),
		 Required("Reference is required!"),
		 InfoBox("Edit referenced variable here change it everywhere!", "IsNotDecimalRefNull"),
		 InlineEditor, HideLabel, ShowInInspector, Space(10)]
		private IHaveValue<decimal> _decimalRef;

		[ShowIf("mode", Mode.IntRef),
		 Required("Reference is required!"),
		 InfoBox("Edit referenced variable here change it everywhere!", "IsNotIntRefNull"),
		 InlineEditor, HideLabel, ShowInInspector, Space(10)]
		private IHaveValue<int> _intRef;

		#endregion



		public decimal Value {
			get {
				switch (mode)
				{
					case Mode.Value:
						return _value;

					case Mode.FloatRef:
						return Convert.ToDecimal(_floatRef.Value);
					case Mode.IntRef:
						return Convert.ToDecimal(_intRef.Value);
					case Mode.DoubleRef:
						return Convert.ToDecimal(_doubleRef.Value);
					case Mode.DecimalRef:
						return _decimalRef.Value;

					default:
						Debug.LogError("Selected mode has not been implemented!");
						return 0;
				}
			}

			set {
				if (CanSetValue)
				{
					switch (mode)
					{
						case Mode.Value:
							_value = value;
							break;

						case Mode.FloatRef:
							_floatRef.Value = ConvertToFloat(value);
							break;
						case Mode.DoubleRef:
							_doubleRef.Value = ConvertToDouble(value);
							break;
						case Mode.IntRef:
							_intRef.Value = ConvertToInt(value);
							break;
						case Mode.DecimalRef:
							_decimalRef.Value = value;
							break;
					}
				}
			}
		}


		public bool CanSetValue {
			get {
				switch (mode)
				{
					case Mode.Value:
						return true;

					case Mode.FloatRef:
						return _floatRef.CanSetValue;
					case Mode.IntRef:
						return _intRef.CanSetValue;
					case Mode.DoubleRef:
						return _doubleRef.CanSetValue;
					case Mode.DecimalRef:
						return _decimalRef.CanSetValue;

					default:
						return false;
				}
			}
		}



		#region AUTO CONVERSION
		public float FloatValue {
			get {
				return ConvertToFloat(Value);
			}

			set {
				Value = Convert.ToDecimal(value);
			}
		}

		public double DoubleValue {
			get {
				return ConvertToDouble(Value);
			}

			set {
				Value = Convert.ToDecimal(value);
			}
		}

		public int IntValue {
			get {
				return ConvertToInt(Value);
			}

			set {
				Value = Convert.ToDecimal(value);
			}
		}

		public long LongValue {
			get {
				return ConvertToLong(Value);
			}

			set {
				Value = Convert.ToDecimal(value);
			}
		}


		#endregion


		#region CONVERSION METHOD
		private float ConvertToFloat(decimal value)
		{
			try
			{
				return Convert.ToSingle(value);
			}
			catch (OverflowException)
			{
				if (value > 0)
				{
					return float.MaxValue;
				}
				else
				{
					return float.MinValue;
				}
			}
		}

		private double ConvertToDouble(decimal value)
		{
			try
			{
				return Convert.ToDouble(value);
			}
			catch (OverflowException)
			{
				if (value > 0)
				{
					return double.MaxValue;
				}
				else
				{
					return double.MinValue;
				}
			}
		}

		private int ConvertToInt(decimal value)
		{
			try
			{
				return Convert.ToInt32(value);
			}
			catch (OverflowException)
			{
				if (value > 0)
				{
					return int.MaxValue;
				}
				else
				{
					return int.MinValue;
				}
			}
		}

		private long ConvertToLong(decimal value)
		{
			try
			{
				return Convert.ToInt64(value);
			}
			catch (OverflowException)
			{
				if (value > 0)
				{
					return long.MaxValue;
				}
				else
				{
					return long.MinValue;
				}
			}
		}

		#endregion



		public enum Mode
		{
			Value = 0,
			FloatRef = 1,
			DoubleRef = 2,
			DecimalRef = 3,
			IntRef = 4
		}



		#region INSPECTOR ONLY
		private bool IsNotFloatRefNull()
		{
			return _floatRef != null;
		}

		private bool IsNotIntRefNull()
		{
			return _intRef != null;
		}

		private bool IsNotDoubleRefNull()
		{
			return _doubleRef != null;
		}

		private bool IsNotDecimalRefNull()
		{
			return _decimalRef != null;
		}

		#endregion

	}
}