using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace MyCustomSystem.Data
{
	public class AbsGenericStartingValueVariableSO<T> : AbsGenericVariableSO<T> where T : struct, ISerializationCallbackReceiver
	{
		[SerializeField] protected T _initialValue;

		public T initalValue => _initialValue;



		protected override void OnAfterDeserialize()
		{
			value = initalValue;
			base.OnAfterDeserialize();
		}

	}
}