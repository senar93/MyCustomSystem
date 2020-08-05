namespace SenarCustomSystem.EntityBehaviour
{
    using Sirenix.OdinInspector;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.Events;

	[ShowOdinSerializedPropertiesInInspector]
	public class AbsBehaviour : MonoBehaviour
    {
		[HideIf("setupStatus", SetupStatusEnum.NotSetup),
		 PropertyOrder(int.MinValue), ReadOnly, ShowInInspector, PropertySpace]
		public AbsEntity entity { get; protected set; }

		[HideInEditorMode, PropertyOrder(int.MinValue + 1), ReadOnly, ShowInInspector, PropertySpace]
		public SetupStatusEnum setupStatus { get; protected set; } = SetupStatusEnum.NotSetup;

		[DisableIf("setupStatus", SetupStatusEnum.SetupInProgress),
		 DisableIf("setupStatus", SetupStatusEnum.SetupCompleted),
		 PropertyOrder(int.MinValue + 2), DisableInPlayMode, SerializeField]
		private bool _setupByEntity = true;
		public bool setupByEntity { get => _setupByEntity; }

		[DisableIf("setupStatus", SetupStatusEnum.SetupInProgress),
		 DisableIf("setupStatus", SetupStatusEnum.SetupCompleted),
		 DisableIf("@this.setupByEntity == false"),
		 PropertyOrder(int.MinValue + 3), DisableInPlayMode, SerializeField]
		public int priorityInBehavioursList = 0;

		[DisableIf("setupStatus", SetupStatusEnum.SetupInProgress),
		 DisableIf("setupStatus", SetupStatusEnum.SetupCompleted), Space]
		public UnityEvent onSetup;



		#region OVERRIDABLE
		protected virtual void CustomSetup() { }

		#endregion



		#region PUBLIC
		/// <summary>
		/// shortcut to call AbsEntity function for Setup
		/// </summary>
		/// <param name="entity"></param>
		public void Setup(AbsEntity entity)
		{
			if(entity != null)
			{
				entity.Setup_ExistingBehaviour(this);
			}
		}

		#endregion



		#region INTERNAL, PROTECTED, PRIVATE
		/// <summary>
		/// Don't use outside AbsEntity!
		/// To setup a behaviour use AbsEntity methods 
		/// </summary>
		/// <param name="entity"></param>
		internal void UsableByEntityOnly_BehaviourSetup(AbsEntity entity)
		{
			if (setupStatus == SetupStatusEnum.NotSetup && entity != null)
			{
				setupStatus = SetupStatusEnum.SetupInProgress;
				this.entity = entity;
				CustomSetup();

				if (onSetup != null)
				{
					onSetup?.Invoke();
				}

				setupStatus = SetupStatusEnum.SetupCompleted;
			}
		}

		#endregion


	}
}