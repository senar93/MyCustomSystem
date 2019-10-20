using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Linq;

namespace MyCustomSystem.EntityBehaviour
{
	public abstract partial class AbsGenericBehaviour : SerializedMonoBehaviour
	{
		[PropertyOrder(int.MinValue), ReadOnly, HideInEditorMode, SerializeField]
		protected AbsGenericEntity _entity;
		[PropertyOrder(-999999), ReadOnly, HideInEditorMode, SerializeField]
		protected bool _hasBeenSetup = false;

		[PropertyOrder(-999997), DisableInPlayMode, SerializeField] 
		protected int _loadOrder = 0;

		[Space] public bool enabledEntityCall = true;


		/// <summary>
		/// dal minore al maggiore
		/// </summary>
		public int loadOrder
		{ 
			get => _loadOrder; 
			set 
			{ 
				_loadOrder = value;
				entity.ShortBehavioursList();
			}
		}
		/// <summary>
		/// puntatore al entity a cui appartiene il behaviour
		/// </summary>
		public AbsGenericEntity entity { get => _entity; }
		/// <summary>
		/// se TRUE il behaviour ha gia effettuato il Setup
		/// </summary>
		public bool hasBeenSetup { get => _hasBeenSetup; }

		/// <summary>
		/// se TRUE il behaviour può effettuare il Setup anche se lo ha gia effettuato
		/// può essere sovrascritta dai figli
		/// </summary>
		public virtual bool canBeSetupMoreThanOneTime { get => _canBeSetupMoreThanOneTime; }
		/// <summary>
		/// se TRUE il behaviour è unico, e non possono esserci altri behaviour uguali nel entity
		/// può essere sovrascritta dai figli
		/// </summary>
		public virtual bool isUnique { get => _isUnique; }
		/// <summary>
		/// restituisce il comportamento da adottare nel caso il behaviour sia Unique
		/// può essere sovrascritta dai figli
		/// </summary>
		public virtual IsUniqueExceptionBehaviourEnum isUniqueBehaviour { get => _isUniqueBehaviour; }


		#region API
		public void Setup(AbsGenericEntity entity, bool forceSetup = true)
		{
			if (!_hasBeenSetup || (canBeSetupMoreThanOneTime && forceSetup))
			{
				_entity = entity;
				_hasBeenSetup = true;
				if (isUnique)
				{
					AbsGenericBehaviour tmpBehaviour = GetOtherUniqueBehaviourOfTheSameTypeInEntity();
					if(tmpBehaviour != null)
					{
						IsUniqueException(tmpBehaviour);
					}
				} 
				else
				{
					CustomSetup();
				}
			}
		}

		#endregion

		#region OVERRIDE

		#region UNITY FUNCTION
		//sono definiti dentro AbsGenericEntity
		//come internal protected, in modo che possano essere utilizzati unicamente li
		//protected virtual void EntityStart() { }
		//protected virtual void EntityUpdate() { }
		//protected virtual void EntityLateUpdate() { }
		//protected virtual void EntityFixedUpdate() { }

		protected virtual void OnDestroy()
		{
			entity.RemoveBehaviour(this);
			CustomOnDestroy();
		}

		#endregion

		#region MY FUNCTION
		protected virtual void CustomOnDestroy() { }
		protected virtual void CustomSetup() { }

		/// <summary>
		/// controlla se il Behaviour è gia presente nel entity
		/// può essere sovrascritta dai figli per implementare particolari comportamenti
		/// </summary>
		/// <returns></returns>
		protected virtual AbsGenericBehaviour GetOtherUniqueBehaviourOfTheSameTypeInEntity()
		{
			AbsGenericBehaviour tmp = entity.GetBehaviour(this.GetType());
			return (isUnique && tmp != null && tmp != this) ? tmp : null;
		}

		/// <summary>
		/// gestisce il caso che il behaviour sia marchiato come unico e sia gia presente un behaviour uguale nell'entity
		/// può essere sovrascritta dai figli per implementare particolari comportamenti
		/// </summary>
		protected virtual void IsUniqueException(AbsGenericBehaviour otherBehaviour) 
		{
			switch (isUniqueBehaviour)
			{
				case IsUniqueExceptionBehaviourEnum.destroyNew_BlockSetup:
					Debug.LogWarning(this.GetType() + " è gia presente in " + entity + " il nuovo behaviour sarà distrutto");
					Destroy(this);
					break;
				case IsUniqueExceptionBehaviourEnum.destoryOld_ContinueSetup:
					Debug.LogWarning(this.GetType() + " è gia presente in " + entity + " e sarà sostituito");
					Destroy(otherBehaviour);
					CustomSetup();
					break;
			}	
		}

		#endregion

		#endregion

		
		public enum IsUniqueExceptionBehaviourEnum 
		{
			destroyNew_BlockSetup = 1,
			destoryOld_ContinueSetup = 2,

			none_DONT_USE = 0
		}

	}
}