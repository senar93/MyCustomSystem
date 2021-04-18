namespace Senar.Data.Variable
{
	using Sirenix.OdinInspector;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	/// <summary>
	/// variable with an initial default value
	/// </summary>
	/// <typeparam name="T">variable type</typeparam>
	public abstract class SO_Abs_VariableStandard<T> : SO_Abs_VariableSimple<T>, ISerializationCallbackReceiver
	{
		/// <summary>
		/// the value that the variable takes as soon as the game start
		/// </summary>
		[SerializeField, DisableInPlayMode]
		public T startingValue;

		protected override void OnAfterDeserialize()
		{
			ResetToStartingValue();
		}

		/// <summary>
		/// set value to startingValue
		/// </summary>
		public virtual void ResetToStartingValue()
		{
			_value = startingValue;
		}

	}
}