namespace SenarCustomSystem.EntityBehaviour
{
    using UnityEngine;
    using Sirenix.OdinInspector;
    using System.Linq;
    using UnityEngine.Events;
    using System.Collections.Generic;
	using UltEvents;

    //TODO: aggiungere un attributo che faccia lo stesso di "DisallowMultipleComponent" ma contando anche i component del parent


    [DisallowMultipleComponent][ShowOdinSerializedPropertiesInInspector]
    public class AbsEntity : MonoBehaviour
    {

        [HideInEditorMode, PropertyOrder(int.MinValue), ReadOnly, ShowInInspector, PropertySpace]
        public SetupStatusEnum setupStatus { get; protected set; } = SetupStatusEnum.NotSetup;

		[HideIf("setupStatus", SetupStatusEnum.NotSetup), 
		 PropertyOrder(int.MaxValue), ReadOnly, ShowInInspector, PropertySpace(16)]
        public List<AbsBehaviour> behaviourList { get; protected set; } = new List<AbsBehaviour>();

        [DisableIf("setupStatus", SetupStatusEnum.SetupInProgress),
		 DisableIf("setupStatus", SetupStatusEnum.SetupCompleted), Space]
        public UltEvent onSetup;



		#region PROPERTIES
		/// <summary>
		/// Unoptimized
		/// </summary>
		public List<AbsBehaviour> setuppedBehaviourList 
		{
			get
			{
				CleareNullReferenceFromBehavioursList();
				return (behaviourList != null) ? behaviourList.FindAll(x => x.setupStatus == SetupStatusEnum.SetupCompleted)
											   : new List<AbsBehaviour>();
			}
		}

		#endregion



		#region OVERRIDABLE
		protected virtual void CustomSetup() { }

		protected virtual void OrderBehavioursList()
		{
			CleareNullReferenceFromBehavioursList();
			behaviourList = behaviourList.OrderBy(x => x.priorityInBehavioursList).ToList();
		}

		#endregion



		#region PUBLIC
		public void Setup()
        {
			if (!SelfDestroyIfIsNestedEntities())
			{
				if (setupStatus == SetupStatusEnum.NotSetup)
				{
					DestroyChildNestedEntities();

					setupStatus = SetupStatusEnum.SetupInProgress;

					if (behaviourList == null)
					{
						behaviourList = new List<AbsBehaviour>();
					}

					CustomSetup();
					SetupAllBehaviours();

					if (onSetup != null)
					{
						onSetup?.Invoke();
					}

					CleareNullReferenceFromBehavioursList();
					setupStatus = SetupStatusEnum.SetupCompleted;
				}
			}
		}


		#region RUNTIME GET & CHECK BEHAVIOUR
		public bool HasBehaviour(AbsBehaviour targetToFind)
		{
			return behaviourList.Find(x => x == targetToFind) != null;
		}

		public T GetBehaviour<T>() where T : AbsBehaviour
		{
			return behaviourList.Find(x => x is T) as T;
		}
		public List<T> GetBehaviours<T>() where T : AbsBehaviour
		{
			return behaviourList.FindAll(x => x is T) as List<T>;
		}
		public AbsBehaviour GetBehaviour(System.Type behaviourType)
		{
			return behaviourList.Find(x => x.GetType() == behaviourType);
		}
		public List<AbsBehaviour> GetBehaviours(System.Type behavioursType)
		{
			return behaviourList.FindAll(x => x.GetType() == behavioursType);
		}
		/// <summary>
		/// Unoptimized, Implemented only for convenince of syntax
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="behaviourPointer"></param>
		/// <returns></returns>
		public bool TryGetBehaviour<T>(out T behaviourPointer) where T : AbsBehaviour
		{
			behaviourPointer = GetBehaviour<T>();
			return behaviourPointer != null;
		}

		#endregion


		#region RUNTIME ADD & SETUP BEHAVIOUR
		/// <summary>
		/// aggiunge correttamente un behaviour ad un entity a runtime (ma senza eseguirne il setup);
		/// se la safeMode è abilitata il setup viene eseguito solo e soltanto se il behaviour è nell'entity o in un suo child;
		/// altrimenti viene eseguito a priori, questo però potrebbe causare dei problemi e va usato solo se si ha la certezza che sia la cosa giusta da fare;
		/// (in caso che l'entity sia disabilitata la safeMode impedisce di eseguire il setup anche in caso sia correttamente in child)
		/// </summary>
		/// <param name="target"></param>
		/// <param name="safeMode"></param>
		public bool Add_ExistingBehaviour(AbsBehaviour target, bool safeMode = true)
		{
			bool condition = !safeMode 
							 || (setupStatus != SetupStatusEnum.NotSetup 
								 && target.setupStatus == SetupStatusEnum.NotSetup
								 && !HasBehaviour(target)
								 && target.GetComponentInParent<AbsEntity>() == this);

			if (condition)
			{
				behaviourList.Add(target);
				OrderBehavioursList();
				return true;
			}

			#if DEBUG || UNITY_EDITOR
			if (safeMode)
			{
				Debug.LogWarning(target + " ha cercato di aggiungersi alla lista di behaviour di " +
								 this + " in modalità safe, ma non è stato possibile aggiungerlo!" +
								 "\nL'errore è stato gestito in automatico, ma conviene controllare il codice, è sintomo di qualcosa che non va");
			}
			#endif

			return false;
		}

		/// <summary>
		/// esegue correttamente il setup di un behaviour in un entity a runtime
		/// se la safeMode è abilitata il setup viene eseguito solo e soltanto se il behaviour è nell'entity o in un suo child
		/// altrimenti viene eseguito a priori, questo però potrebbe causare dei problemi e va usato solo se si ha la certezza che sia la cosa giusta da fare
		/// (in caso che l'entity sia disabilitata la safeMode impedisce di eseguire il setup anche in caso sia correttamente in child)
		/// </summary>
		/// <param name="target"></param>
		/// <param name="safeMode"></param>
		public bool Setup_ExistingBehaviour(AbsBehaviour target, bool safeMode = true)
		{
			bool condition = !safeMode
							 || (setupStatus != SetupStatusEnum.NotSetup
								 && target.setupStatus == SetupStatusEnum.NotSetup
								 && HasBehaviour(target)
								 && target.GetComponentInParent<AbsEntity>() == this);
			
			if(condition)
			{
				target.UsableByEntityOnly_BehaviourSetup(this);
			}

			return condition;
		}

		/// <summary>
		/// aggiunge e esegue correttamente il setup di un behaviour in un entity a runtime
		/// se la safeMode è abilitata il setup viene eseguito solo e soltanto se il behaviour è nell'entity o in un suo child
		/// altrimenti viene eseguito a priori, questo però potrebbe causare dei problemi e va usato solo se si ha la certezza che sia la cosa giusta da fare
		/// (in caso che l'entity sia disabilitata la safeMode impedisce di eseguire il setup anche in caso sia correttamente in child)
		/// </summary>
		/// <param name="target"></param>
		/// <param name="safeMode"></param>
		public bool AddAndSetup_ExistingBehaviour(AbsBehaviour target, bool safeMode = true)
		{
			bool tmp = Add_ExistingBehaviour(target, safeMode);

			if (tmp)
			{
				target.UsableByEntityOnly_BehaviourSetup(this);
			}

			return tmp;
		}

		public T Add_NewBehaviour<T>(GameObject positionInHierarchy, bool safeMode = true) where T : AbsBehaviour
		{
			T behaviourPointer = positionInHierarchy.AddComponent<T>();
			if(behaviourPointer)
			{
				bool tmp = Add_ExistingBehaviour(behaviourPointer, safeMode);
				if(!tmp)
				{
					DestroyImmediate(behaviourPointer);
					return null;
				}
				return behaviourPointer;
			}

			#if DEBUG || UNITY_EDITOR
				Debug.LogWarning("Non è stato possibile aggiungere il behaviour al GameObject + \"" + 
								 positionInHierarchy + "\" appartenente all'entity \"" + this + "\"");
			#endif
							 
			return null;
		}
		public T Add_NewBehaviour<T>(bool safeMode = true) where T : AbsBehaviour
		{
			return Add_NewBehaviour<T>(this.gameObject, safeMode);
		}
		
		public T AddAndSetup_NewBehaviour<T>(GameObject positionInHierarchy, bool safeMode = true) where T : AbsBehaviour
		{
			T tmp = Add_NewBehaviour<T>(positionInHierarchy, safeMode);
			if (tmp)
			{
				Setup_ExistingBehaviour(tmp);
				return tmp;
			}

			return null;
		}
		public T AddAndSetup_NewBehaviour<T>(bool safeMode = true) where T : AbsBehaviour
		{
			return AddAndSetup_NewBehaviour<T>(this.gameObject, safeMode);
		}

		#endregion


		public void CleareNullReferenceFromBehavioursList()
		{
			if (behaviourList == null)
			{
				behaviourList = new List<AbsBehaviour>();
			}

			behaviourList.RemoveAll(x => x == null);
		}

		#endregion



		#region INTERNAL, PROTECTED, PRIVATE
		private void SetupAllBehaviours()
		{
			behaviourList.Clear();
			behaviourList = GetComponentsInChildren<AbsBehaviour>().ToList();
			OrderBehavioursList();
			for (int i = 0; i < behaviourList.Count; i++)
			{
				SetupBehaviour(behaviourList[i]);
			}
		}

		private void SetupBehaviour(AbsBehaviour behaviour)
		{
			if (behaviour != null &&
				behaviour.setupByEntity &&
				behaviour.setupStatus == SetupStatusEnum.NotSetup)
			{
				behaviour.UsableByEntityOnly_BehaviourSetup(this);
			}
		}

		/// <summary>
		/// distrugge l'entity che sta venendo setuppata e tutti i behaviour che ha in child se è una nested entity
		/// </summary>
		/// <returns></returns>
		private bool SelfDestroyIfIsNestedEntities()
		{
			AbsEntity[] otherEntities = GetComponentsInParent<AbsEntity>().Where(x => x != this).ToArray();
			if (otherEntities.Length > 0)
			{
				Debug.LogError(this.name + " is a Nested Entity!\n" +
							   "Nested Entity are forbidden, therefore " + this.name + " will be destroyed :(");
				DestroyImmediate(this.gameObject);
				
				return true;
			}

			return false;
		}

		/// <summary>
		/// distrugge i gameobjects delle nested entity trovate all'interno di quella che sta venendo setuppata in questo momento
		/// </summary>
		/// <returns></returns>
		private bool DestroyChildNestedEntities()
		{
			AbsEntity[] otherEntities = GetComponentsInChildren<AbsEntity>().Where(x => x != this).ToArray();
			if (otherEntities.Length > 0)
			{
				Debug.LogError("Finded one ore more Nested Entity inside " + this.name +
								 "!\nNested Entity are forbidden, if they are setupped they will be destroyed!");
				foreach (AbsEntity tmp in otherEntities)
				{
					DestroyImmediate(tmp.gameObject);
				}

				return true;
			}

			return false;
		}

		#endregion



		#region INSPECTOR
		#region SETUP ON AWAKE 
		private SetupEntityOnAwake setupOnAwakePointer = null;

		[HideIf("Inspector_HideSetupOnAwakeButtonFlag"), HideInPlayMode, 
		 Button("Add Setup On Awake", ButtonSizes.Medium), GUIColor(0.4f, 1f, 0.4f),
		 PropertySpace(32)]
		private void Inspector_AddSetupEntityOnAwake()
		{
			if (setupOnAwakePointer == null)
			{
				setupOnAwakePointer = this.gameObject.AddComponent<SetupEntityOnAwake>();
			}
		}

		private bool Inspector_HideSetupOnAwakeButtonFlag()
		{
			return (setupOnAwakePointer != null ? true : TryGetComponent(out setupOnAwakePointer)) ;
		}

		#endregion

		#endregion


	}
}