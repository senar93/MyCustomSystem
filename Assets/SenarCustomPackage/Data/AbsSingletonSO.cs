namespace MyCustomSystem.Data
{
	using UnityEngine;
	using Sirenix.OdinInspector;
	using System.Linq;

	public class AbsSingletonSO<T> : SerializedScriptableObject where T : AbsSingletonSO<T>
	{
		static T _instance;
		public static T Instance
		{
			get
			{
				if (!_instance)
				{
					T[] tmp = Resources.LoadAll<T>("").OrderBy(x => x.name).ToArray<T>();
					
					if (tmp.Length > 1)
					{
						Debug.LogWarning("Sono state trovate più istanze della classe " + typeof(T) +
										 "; verrà caricato solo il file " + tmp.First().name);
					}

					_instance = tmp.First();

					if (!_instance)
					{
						Debug.LogError("Non sono presenti istanze della classe " + typeof(T));
					}

				}

				return _instance;
			}
		}

	}
}