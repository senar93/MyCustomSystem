namespace SenarCustomSystem.Data
{
	using UnityEngine;
	using Sirenix.OdinInspector;
	using System.Linq;

	public abstract class AbsSingletonScriptableObject<T> : SerializedScriptableObject where T : AbsSingletonScriptableObject<T>
	{
		[SerializeField] protected bool excludeFromLoading = false; 

		protected static T _instance;
		public static T Instance
		{
			get
			{
				if (!_instance)
				{
					LoadSingleton();
				}

				return _instance;
			}
		}
		/// <summary>
		/// Worst performance ever, don't use if not absolutely necessary
		/// </summary>
		public static T[] AllIstances
		{
			get 
			{
				T[] tmp = Resources.LoadAll<T>("");
				tmp = (tmp != null && tmp.Length > 0) ? tmp.Where(x => x != null && x.excludeFromLoading == false).ToArray()
													  : null;
				tmp = (tmp != null && tmp.Length > 0) ? tmp.OrderBy(x => x.name).ToArray<T>()
													  : null;

				return tmp;
			}
		}

		public static bool LoadSingleton()
		{
			T[] tmp = AllIstances;
			if (tmp != null)
			{
				_instance = tmp.First();
			}

			#if DEBUG || UNITY_EDITOR
				DebugLog_FileFound(tmp, typeof(T));
				DebugLogWarning_MultipleFilesFound(tmp, typeof(T));
				DebugLogError_NoFilesFound(tmp, typeof(T));
			#endif
			return _instance != null;
		}


		#region DEBUG LOG FUNCTIONS
		#if DEBUG || UNITY_EDITOR
			private static void DebugLogWarning_MultipleFilesFound(T[] tmp, System.Type type)
			{
				if (tmp != null && tmp.Length > 1)
				{
					string msg = "Sono state trovate più istanze della classe <color=" + DebugHelper.colorBlue + ">" + type + "</color>\n" +
									"Verrà caricato solo il primo file in ordine alfabetico\n" +
									"Loaded:  <color=" + DebugHelper.colorGreen + ">" + tmp.First().name + "</color>";
					msg += "\n";
					for (int i = 1; i < tmp.Length; i++)
					{
						msg += "Not Loaded:  <color=" + DebugHelper.colorRed + ">" + tmp[i].name + "</color>\n";
					}
				
					Debug.LogWarning(msg);
				}
			}

			private static void DebugLogError_NoFilesFound(T[] tmp, System.Type type)
			{
				if (tmp == null || tmp.Length < 1)
				{
					Debug.LogError("File di tipo <color=" + DebugHelper.colorBlue + ">" + type + "</color> non trovato!\n" + 
									"Il file non esiste o non si trova in alcuna cartella \"Resources\"");
				}
			}

			private static void DebugLog_FileFound(T[] tmp, System.Type type)
			{
				if (tmp != null && tmp.Length == 1)
				{
					Debug.Log("File di tipo <color=" + DebugHelper.colorBlue + ">" + type + "</color> trovato correttamente\n" +
								"Loaded:  <color=" + DebugHelper.colorGreen + ">" + tmp.First().name + "</color>");
				}
			}
		#endif
		#endregion

	}
}