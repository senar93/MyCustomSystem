using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Linq;

namespace MyCustomSystem.EntityBehaviour
{
	public abstract class AbsGenericEntity : SerializedMonoBehaviour
	{
		[ReadOnly, HideInEditorMode, SerializeField] 
		private bool _hasBeenSetup = false;

		[ShowIf("showCanBeSetupMoreThanOneTimeInInspector"), SerializeField] 
		private bool _canBeSetupMoreThanOneTime = false;
		[SerializeField] 
		private bool setupOnAwake = true;

		[PropertyOrder(int.MaxValue), ReadOnly, HideInEditorMode, SerializeField, Space] 
		protected List<AbsGenericBehaviour> customBehaviourList = new List<AbsGenericBehaviour>();


		public bool hasBeenSetup { get => _hasBeenSetup; }
		public virtual bool canBeSetupMoreThanOneTime { get => _canBeSetupMoreThanOneTime; }
		protected virtual bool showCanBeSetupMoreThanOneTimeInInspector { get => true; }


		#region API
		/// <summary>
		/// esegue il Setup dell'entity
		/// </summary>
		/// <param name="forceSetup">se true esegue il setup anche se l'entity è gia stata inizializzata una volta (solo per le entity che lo supportano)</param>
		public void Setup(bool forceSetup = true)
		{
			if (!_hasBeenSetup || (canBeSetupMoreThanOneTime && forceSetup))
			{
				if(customBehaviourList == null)
					customBehaviourList = new List<AbsGenericBehaviour>();

				_hasBeenSetup = true;
				EntitySetup();
				PreBehaviourSetup();
				BehaviourSetup();
				AfterBehaviourSetup();
			}
		}

		/// <summary>
		/// ottiene il riferimento al primo behaviour del tipo passato come parametro dell'entity
		/// </summary>
		/// <param name="typeToFind">tipo del behaviour da cercare</param>
		/// <returns>puntattore al behaviour trovato, null se non è presente</returns>
		public AbsGenericBehaviour GetBehaviour(System.Type typeToFind)
		{
			return customBehaviourList.Find(x => x.GetType() == typeToFind);
		}

		/// <summary>
		/// ottiene il riferimento a tutti i behaviour del tipo passato come parametro dell'entity
		/// </summary>
		/// <param name="typeToFind">tipo del behaviour da cercare</param>
		/// <returns>lista contenente tutti i puntatori dei behaviour trovati</returns>
		public List<AbsGenericBehaviour> GetBehaviours(System.Type typeToFind)
		{
			return customBehaviourList.FindAll(x => x.GetType() == typeToFind);
		}

		/// <summary>
		/// genera parecchia garbage
		/// </summary>
		/// <returns></returns>
		public List<AbsGenericBehaviour> GetAllBehaviours()
		{
			return new List<AbsGenericBehaviour>(customBehaviourList);
		}

		/// <summary>
		/// aggiunge un behaviour gia presente sul GameObject all'entity associata
		/// </summary>
		/// <param name="behaviour">behaviopur da aggiungere</param>
		public void AddBehaviour(AbsGenericBehaviour behaviour, bool checkIfIsChild = true)
		{
			if (!checkIfIsChild || behaviour.gameObject.GetComponentInParent<AbsGenericEntity>() == this)
			{
				if (behaviour != null)
				{
					this.customBehaviourList.Add(behaviour);
					customBehaviourList = customBehaviourList.OrderBy(x => x.loadOrder).ToList();
					behaviour.Setup(this);
				}
			}
			else
			{
				Debug.LogError("L'oggetto " + behaviour.name + " ha cercato di aggiungere un behaviour di tipo " +
								behaviour.GetType() + " a " + this.name + " ma l'oggetto non è un suo child!");
			}
		}

		/// <summary>
		/// rimuove un behaviour dall'entity associata
		/// </summary>
		/// <param name="behaviour"></param>
		public void RemoveBehaviour(AbsGenericBehaviour behaviour)
		{
			customBehaviourList.Remove(behaviour);
		}

		/// <summary>
		/// ordina i behaviours del entity in base al loro loadOrder
		/// </summary>
		public void ShortBehavioursList() 
		{
			customBehaviourList = customBehaviourList.OrderBy(x => x.loadOrder).ToList();
		}

		#endregion

		#region OVERRIDE

		#region UNITY FUNCTION
		protected virtual void Awake()
		{
			if (setupOnAwake)
			{
				Setup();
			}
		}

		protected virtual void Start()
		{
			if (_hasBeenSetup)
			{
				for (int i = 0; i < customBehaviourList.Count; i++)
				{
					if (customBehaviourList[i].hasBeenSetup && customBehaviourList[i].enabledEntityCall)
					{
						customBehaviourList[i].EntityStart();
					}
				}
			}
		}

		protected virtual void Update()
		{
			if (_hasBeenSetup)
			{
				for (int i = 0; i < customBehaviourList.Count; i++)
				{
					if (customBehaviourList[i].hasBeenSetup && customBehaviourList[i].enabledEntityCall)
					{
						customBehaviourList[i].EntityUpdate();
					}
				}
			}
		}

		protected virtual void LateUpdate()
		{
			if (_hasBeenSetup)
			{
				for (int i = 0; i < customBehaviourList.Count; i++)
				{
					if (customBehaviourList[i].hasBeenSetup && customBehaviourList[i].enabledEntityCall)
					{
						customBehaviourList[i].EntityLateUpdate();
					}
				}
			}
		}

		protected virtual void FixedUpdate()
		{
			if (_hasBeenSetup)
			{
				for (int i = 0; i < customBehaviourList.Count; i++)
				{
					if (customBehaviourList[i].hasBeenSetup && customBehaviourList[i].enabledEntityCall)
					{
						customBehaviourList[i].EntityFixedUpdate();
					}
				}
			}
		}

		#endregion

		#region MY FUNCTION
		/// <summary>
		/// prima funzione chiamata nel Setup, contiene le parti del setup strettamente legate al entity e indipendenti dai behaviour
		/// </summary>
		protected virtual void EntitySetup() { }
		/// <summary>
		/// seconda funzione chiamata nel Setup, 
		/// contiene tutte le parti del setup che vanno eseguite necessariamente prima del setup dei behaviour
		/// </summary>
		protected virtual void PreBehaviourSetup() { }
		/// <summary>
		/// ultima funzione chiamata nel Setup,
		/// contiene tutte le parti del setup che vanno eseguite necessariamente dopo il setup dei behaviour
		/// </summary>
		protected virtual void AfterBehaviourSetup() { }

		#endregion

		#endregion

		#region INTERNAL
		/// <summary>
		/// esegue automaticamente il setup di tutti i behaviour presenti nel GameObject e in tutti i suoi figli
		/// </summary>
		private void BehaviourSetup()
		{
			customBehaviourList.Clear();
			customBehaviourList = GetComponentsInChildren<AbsGenericBehaviour>().ToList();
			ShortBehavioursList();
			foreach (AbsGenericBehaviour customBehaviour in customBehaviourList)
			{
				customBehaviour.Setup(this);
			}
		}

		#endregion

	}



	// avendo definito i metodi come internal sono visibili solo in questo file, per tutti gli altri contano come protected
	public abstract partial class AbsGenericBehaviour : SerializedMonoBehaviour
	{
		protected internal virtual void EntityStart() { }
		protected internal virtual void EntityUpdate() { }
		protected internal virtual void EntityLateUpdate() { }
		protected internal virtual void EntityFixedUpdate() { }
	}

}