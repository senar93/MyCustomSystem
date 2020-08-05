namespace OLD_SenarCustomSystem.Variables.Reference
{
	using Sirenix.OdinInspector;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using System;
	using OLD_SenarCustomSystem.Variables.Interface;
	using OLD_SenarCustomSystem.Variables.Abstract;
	using System.Linq;
	using Sirenix.Serialization;


	public class Double_Val : AbsNumericReference
	{
		[SerializeField]
		private double _doubleValue;

		public override bool CanSetValue { 
			get => true; 
		}

		public override double DoubleValue 
		{
			get => _doubleValue; 
			set => _doubleValue = value;
		}

		public override float FloatValue
		{ 
			get => ConvertToFloat<double>(_doubleValue); 
			set => _doubleValue = value;
		}

		public override int IntValue 
		{
			get => ConvertToInt<double>(_doubleValue);
			set => _doubleValue = value;
		}

		public override long LongValue 
		{
			get => ConvertToLong<double>(_doubleValue);
			set => _doubleValue = value;
		}

		public override decimal DecimalValue 
		{
			get => ConvertToDecimal<double>(_doubleValue);
			set => _doubleValue = ConvertToDouble<decimal>(value);
		}

	}


}