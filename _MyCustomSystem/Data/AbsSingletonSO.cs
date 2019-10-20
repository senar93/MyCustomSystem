using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Linq;

namespace MyCustomSystem.Data
{
	public class AbsSingletonSO<T> : SerializedScriptableObject where T : SerializedScriptableObject
	{
		static T _instance;
		public static T Instance
		{
			get
			{
				if (!_instance)
				{
					T[] tmp = Resources.LoadAll<T>("");
					if (tmp.Length > 1)
						Debug.LogWarning("Sono state trovate più istanze della classe " + typeof(T) + 
										 "; verrà caricato solo il file " + tmp.First().name);
					_instance = tmp.First();
				}
				if (!_instance)
					Debug.LogError("Non sono presenti istanze della classe " + typeof(T));
				return _instance;
			}
		}

	}
}