﻿namespace MyCustomSystem.Variables.Data
{
	using MyCustomSystem.Variables.Abstract;
	using MyCustomSystem.Variables.Interface;
	using Sirenix.OdinInspector;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using System.Linq;
	using System;

	//https://github.com/pieterderycke/Jace

	[CreateAssetMenu(menuName = "MyCustomSystem/Variables/Double/Formula Var",
					 fileName = "_NEW FormulaDouble")]
	public class FormulaDoubleVariable_So : AbsVariable_So<double>
	{
		[TextArea]
		public string formula;


		[DictionaryDrawerSettings(KeyLabel = "Name", ValueLabel = "Value", DisplayMode = DictionaryDisplayOptions.Foldout),
		 Space(20)]
		public Dictionary<string, NumericReference> variable = new Dictionary<string, NumericReference>();

		Jace.CalculationEngine engine = new Jace.CalculationEngine();



		public override double Value {
			get {
				Dictionary<string, double> realVariable = variable.ToDictionary(item => item.Key,
																				 item => Convert.ToDouble(item.Value.Value));

				return engine.Calculate(formula, realVariable);
			}
			set { }
		}

		public override bool CanSetValue { get => false; }

	}
}