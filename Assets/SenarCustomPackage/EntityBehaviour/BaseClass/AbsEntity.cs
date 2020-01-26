namespace MyCustomSystem.EntityBehaviour
{
	using Sirenix.OdinInspector;
    using System;
    using System.Collections;
	using System.Collections.Generic;
	using System.Linq;
	using UnityEngine;
	using UnityEngine.Events;

	[DisallowMultipleComponent]
	public class AbsEntity : SerializedMonoBehaviour
	{
		[HideInEditorMode, ReadOnly, SerializeField]
		public bool hasBeenSetup { get; protected set; }

		[HideIf("hasBeenSetup"), SerializeField]
		private bool setupOnAwake = true;

		[ShowIf("hasBeenSetup"), PropertyOrder(int.MaxValue), ReadOnly, SerializeField, Space]
		protected List<AbsBehaviour> behaviourList = new List<AbsBehaviour>();

		[SerializeField, DisableInPlayMode] private bool _enabled = true;

		[TabGroup("AbsEntity_Events", "Setup"), DisableIf("hasBeenSetup"), Space]
		public UnityEvent onSetup;
		[TabGroup("AbsEntity_Events", "Destory"), Space]
		public UnityEvent onDestroy;
		[TabGroup("AbsEntity_Events", "Enable & Disable"), Space]
		public UnityEvent onEnable;
		[TabGroup("AbsEntity_Events", "Enable & Disable"), Space]
		public UnityEvent onDisable;

		#region PROPERTY
		public new bool enabled {
			get 
			{
				return _enabled;
			}
			set 
			{
				if (enabled != value)
				{
					_enabled = value;
					switch (_enabled)
					{
						case true:
							CustomOnEnable();
							onEnable?.Invoke();
							break;
						case false:
							CustomOnDisable();
							onDisable?.Invoke();
							break;
					}
				}
			}
		}

		#endregion

		#region OVERRIDABLE
		protected virtual void Awake()
		{
			if (setupOnAwake)
			{
				Setup();
			}
		}

		protected virtual void CustomSetup() { }

		/// <summary>
		/// funzione chiamata durante OnDestroy,
		/// per implementare comportamente custom durante OnDestroy sovrascrivere questa funzione
		/// </summary>
		protected virtual void CustomOnDestroy() { }

		protected virtual void CustomOnEnable() { }

		protected virtual void CustomOnDisable() { }

		#endregion

		#region API
		/// <summary>
		/// esegue il Setup dell'entity
		/// </summary>
		/// <param name="forceSetup">se true esegue il setup anche se l'entity è gia stata inizializzata una volta (solo per le entity che lo supportano)</param>
		public void Setup(bool forceSetup = true)
		{
			if (!hasBeenSetup)
			{
				if (behaviourList == null)
				{
					behaviourList = new List<AbsBehaviour>();
				}

				hasBeenSetup = true;
				CustomSetup();
				SetupAllBehaviours();

				if (onSetup == null)
				{
					onSetup = new UnityEvent();
				}

				onSetup?.Invoke();
			}
		}

		/// <summary>
		/// ottiene il riferimento al primo behaviour del tipo passato come parametro dell'entity
		/// </summary>
		/// <param name="typeToFind">tipo del behaviour da cercare</param>
		/// <returns>puntattore al behaviour trovato, null se non è presente</returns>
		public T GetBehaviour<T>() where T : AbsBehaviour
		{
			return behaviourList.Find(x => x is T) as T;
		}

		/// <summary>
		/// ottiene il riferimento a tutti i behaviour del tipo passato come parametro dell'entity
		/// </summary>
		/// <param name="typeToFind">tipo del behaviour da cercare</param>
		/// <returns>lista contenente tutti i puntatori dei behaviour trovati</returns>
		public List<T> GetBehaviours<T>() where T : AbsBehaviour
		{
			return behaviourList.FindAll(x => x is T) as List<T>;
		}

		/// <summary>
		/// genera parecchia garbage
		/// </summary>
		/// <returns></returns>
		public List<AbsBehaviour> GetAllBehaviours()
		{
			return new List<AbsBehaviour>(behaviourList);
		}

		/// <summary>
		/// aggiunge un behaviour gia presente sul GameObject all'entity associata
		/// </summary>
		/// <param name="behaviour">behaviopur da aggiungere</param>
		public void AddInstantiatedBehaviour(AbsBehaviour behaviour, bool setupOnAdd = true, bool checkIfIsChild = true)
		{
			if (!checkIfIsChild || behaviour.transform.IsChildOf(this.transform))
			{
				if (behaviour != null)
				{
					this.behaviourList.Add(behaviour);
					if(setupOnAdd)
					{
						SetupBehaviour(behaviour);
					}
				}
			}
		}

		/// <summary>
		/// aggiunge un nuovo behaviour all'entity
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="setupOnAdd"></param>
		/// <param name="transformThatContainThisBehaviour"></param>
		/// <returns></returns>
		public T AddNewBehaviour<T>(bool setupOnAdd = true, Transform transformThatContainThisBehaviour = null) where T : AbsBehaviour
		{
			GameObject parent;

			// imposta parent con l'oggetto che deve contenere il nuovo behaviour 
			// (solo come posizione nei gameobject)
			if (transformThatContainThisBehaviour != null &&
				transformThatContainThisBehaviour.IsChildOf(this.transform))
			{
				parent = transformThatContainThisBehaviour.gameObject;
			}
			else
			{
				parent = this.gameObject;
			}

			T returnValue = parent.AddComponent<T>();

			AddInstantiatedBehaviour(returnValue, setupOnAdd, false);

			return returnValue;
		}

		/// <summary>
		/// ottiene il behaviour del tipo selezionato, se non è presente lo aggiunge
		/// </summary>
		/// <param name="behaviour">behaviopur da ottenere o aggiungere</param>
		public T GetOrAddBehaviour<T>(bool setupOnAdd = true, Transform transformThatContainThisBehaviour = null) where T : AbsBehaviour
		{
			T returnValue = GetBehaviour<T>();
			if (returnValue == null)
			{
				returnValue = AddNewBehaviour<T>(setupOnAdd, transformThatContainThisBehaviour);
			}
			return returnValue;
		}

		/// <summary>
		/// rimuove un behaviour dall'entity associata
		/// </summary>
		/// <param name="behaviour"></param>
		public void RemoveBehaviour(AbsBehaviour behaviour, bool destroyBehaviour = true)
		{
			behaviourList.Remove(behaviour);
			if(destroyBehaviour)
			{
				Destroy(behaviour);
			}
		}

		#endregion

		#region INTERNAL
		/// <summary>
		/// esegue automaticamente il setup di tutti i behaviour presenti nel GameObject e in tutti i suoi figli
		/// </summary>
		private void SetupAllBehaviours()
		{
			behaviourList.Clear();
			behaviourList = GetComponentsInChildren<AbsBehaviour>().ToList();
			for(int i = 0; i < behaviourList.Count; i++)
			{
				SetupBehaviour(behaviourList[i]);
			}
		}

		private void SetupBehaviour(AbsBehaviour behaviour)
		{
			if (behaviour != null && 
				behaviour.setupByEntity && 
				!behaviour.hasBeenSetup)
			{
				behaviour.Setup(this);
			}
		}

		/// <summary>
		/// funzione chiamata alla distruzione dell'entity
		/// si consiglia di non sovrascriverla ma di utilizzare invece CustomOnDestroy
		/// per garantire il corretto funzionamento dell'entity
		/// </summary>
		private void OnDestroy()
		{
			CustomOnDestroy();
			onDestroy?.Invoke();
		}

		#endregion

	}

}
