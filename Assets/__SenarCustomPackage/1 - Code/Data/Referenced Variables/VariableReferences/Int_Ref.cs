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


	
	public class Int_Ref : AbsNumericReference
	{
		[Required("Reference is required!"),
		 InfoBox("Edit referenced variable here change it everywhere!", "IsNotIntRefNull"),
		 InlineEditor, HideLabel, SerializeField, Space(10),
		 OdinSerialize] //to avoid unity serialization bug
		private IReferenceableVariabile<int> _intRef;


		public override bool CanSetValue {
			get => _intRef.CanSetValue;
		}

		public override double DoubleValue {
			get => _intRef.Value;
			set {
				if (CanSetValue)
					_intRef.Value = ConvertToInt<double>(value);
			}
		}

		public override float FloatValue {
			get => _intRef.Value;
			set {
				if (CanSetValue)
					_intRef.Value = ConvertToInt<float>(value);
			}
		}

		public override int IntValue {
			get => _intRef.Value;
			set {
				if (CanSetValue)
					_intRef.Value = value;
			}
		}

		public override long LongValue {
			get => _intRef.Value;
			set {
				if (CanSetValue)
					_intRef.Value = ConvertToInt<long>(value);
			}
		}

		public override decimal DecimalValue {
			get => _intRef.Value;
			set {
				if (CanSetValue)
					_intRef.Value = ConvertToInt<decimal>(value);
			}
		}

		#region INSPECTOR ONLY
		private bool IsNotIntRefNull()
		{
			return _intRef != null;
		}

		#endregion

	}


}