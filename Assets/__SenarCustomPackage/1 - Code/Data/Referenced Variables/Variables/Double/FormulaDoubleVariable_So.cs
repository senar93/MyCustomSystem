namespace MyCustomSystem.Variables.Data
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
	//TODO: pulsante Bake per creare la versione ottimizzata non modificabile della formula
	//TODO: pulsante UnBake per tornare alla versione non ottimizzata facilmente modificabile

	[CreateAssetMenu(menuName = "MyCustomSystem/Variables/Double/Formula Var",
					 fileName = "_NEW FormulaDouble")]
	public class FormulaDoubleVariable_So : AbsVariable_So<double>
	{
		[TextArea]
		public string formula;


		[DictionaryDrawerSettings(KeyLabel = "Name", ValueLabel = "Value", DisplayMode = DictionaryDisplayOptions.Foldout),
		 Space(20)]
		public Dictionary<string, AbsNumericReference> variable = new Dictionary<string, AbsNumericReference>();

		Jace.CalculationEngine engine = new Jace.CalculationEngine();



		public override double Value {
			get {
				Dictionary<string, double> realVariable = variable.ToDictionary(item => item.Key,
																				 item => Convert.ToDouble(item.Value.DoubleValue));

				return engine.Calculate(formula, realVariable);
			}
			set { }
		}

		public override bool CanSetValue { get => false; }

	}
}