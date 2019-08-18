using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace MyCustomSystem.Data
{
	public class AbsGenericVariableSO<T> : SerializedScriptableObject where T : struct
	{
		public T value;
	}
}

