namespace MyCustomSystem.Variables.Reference
{
	using Sirenix.OdinInspector;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using System;
	using MyCustomSystem.Variables.Interface;
	using MyCustomSystem.Variables.Abstract;
	using System.Linq;
	using Sirenix.Serialization;


	public class Decimal_Ref : AbsNumericReference
	{
		[Required("Reference is required!"),
		 InfoBox("Edit referenced variable here change it everywhere!", "IsNotDecimalRefNull"),
		 InlineEditor, HideLabel, SerializeField, Space(10),
		 OdinSerialize] //to avoid unity serialization bug
		private IReferenceableVariabile<decimal> _decimalRef;


		public override bool CanSetValue {
			get => _decimalRef.CanSetValue;
		}

		public override double DoubleValue {
			get => ConvertToDouble<decimal>(_decimalRef.Value);
			set {
				if (CanSetValue)
					_decimalRef.Value = ConvertToDecimal<double>(value);
			}
		}

		public override float FloatValue {
			get => ConvertToFloat<decimal>(_decimalRef.Value);
			set {
				if (CanSetValue)
					_decimalRef.Value = ConvertToDecimal<float>(value);
			}
		}

		public override int IntValue {
			get => ConvertToInt<decimal>(_decimalRef.Value);
			set {
				if (CanSetValue)
					_decimalRef.Value = value;
			}
		}

		public override long LongValue {
			get => ConvertToLong<decimal>(_decimalRef.Value);
			set {
				if (CanSetValue)
					_decimalRef.Value = value;
			}
		}

		public override decimal DecimalValue {
			get => _decimalRef.Value;
			set {
				if (CanSetValue)
					_decimalRef.Value = value;
			}
		}

		#region INSPECTOR ONLY
		private bool IsNotDecimalRefNull()
		{
			return _decimalRef != null;
		}
		#endregion


	}


}