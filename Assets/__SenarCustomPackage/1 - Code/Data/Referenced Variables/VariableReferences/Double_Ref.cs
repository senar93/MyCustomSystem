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


	public class Double_Ref : AbsNumericReference
	{
		[Required("Reference is required!"),
		 InfoBox("Edit referenced variable here change it everywhere!", "IsNotDoubleRefNull"),
		 InlineEditor, HideLabel, SerializeField, Space(10),
		 OdinSerialize] //to avoid unity serialization bug
		private IReferenceableVariabile<double> _doubleRef;


		public override bool CanSetValue {
			get => _doubleRef.CanSetValue;
		}

		public override double DoubleValue {
			get => _doubleRef.Value;
			set {
				if (CanSetValue)
					_doubleRef.Value = value;
			}
		}

		public override float FloatValue {
			get => ConvertToFloat<double>(_doubleRef.Value);
			set {
				if (CanSetValue)
					_doubleRef.Value = value;
			}
		}

		public override int IntValue {
			get => ConvertToInt<double>(_doubleRef.Value);
			set {
				if (CanSetValue)
					_doubleRef.Value = value;
			}
		}

		public override long LongValue {
			get => ConvertToLong<double>(_doubleRef.Value);
			set {
				if (CanSetValue)
					_doubleRef.Value = value;
			}
		}

		public override decimal DecimalValue {
			get => ConvertToDecimal<double>(_doubleRef.Value);
			set {
				if (CanSetValue)
					_doubleRef.Value = ConvertToDouble<decimal>(value);
			}
		}

		#region INSPECTOR ONLY
		private bool IsNotDoubleRefNull()
		{
			return _doubleRef != null;
		}

		#endregion

	}


}