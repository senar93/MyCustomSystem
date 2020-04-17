namespace SenarCustomSystem.Variables.Reference
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


	public class Float_Ref : AbsNumericReference
	{
		[Required("Reference is required!"),
		 InfoBox("Edit referenced variable here change it everywhere!", "IsNotFloatRefNull"),
		 InlineEditor, HideLabel, SerializeField, Space(10),
		 OdinSerialize] //to avoid unity serialization bug
		private IReferenceableVariabile<float> _floatRef;


		public override bool CanSetValue {
			get => _floatRef.CanSetValue;
		}

		public override double DoubleValue {
			get => _floatRef.Value;
			set {
				if (CanSetValue)
					_floatRef.Value = ConvertToFloat<double>(value);
			}
		}

		public override float FloatValue {
			get => _floatRef.Value;
			set {
				if (CanSetValue)
					_floatRef.Value = value;
			}
		}

		public override int IntValue {
			get => ConvertToInt<float>(_floatRef.Value);
			set {
				if (CanSetValue)
					_floatRef.Value = value;
			}
		}

		public override long LongValue {
			get => ConvertToLong<double>(_floatRef.Value);
			set {
				if (CanSetValue)
					_floatRef.Value = value;
			}
		}

		public override decimal DecimalValue {
			get => ConvertToDecimal<double>(_floatRef.Value);
			set {
				if (CanSetValue)
					_floatRef.Value = ConvertToFloat<decimal>(value);
			}
		}

		#region INSPECTOR ONLY
		private bool IsNotFloatRefNull()
		{
			return _floatRef != null;
		}

		#endregion

	}


}