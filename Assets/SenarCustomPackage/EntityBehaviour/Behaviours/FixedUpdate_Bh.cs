namespace MyCustomSystem.EntityBehaviour
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.Events;

	[AddComponentMenu("_MyCustomSystem/EntityBehaviour/Unique Behaviour/Fixed Update"),
	 RequireComponent(typeof(AbsEntity)),
	 DisallowMultipleComponent]
	public class FixedUpdate_Bh : AbsDynamicBehaviour
	{
		[Space]
		public UnityEvent onFixedUpdate;

		protected override void CustomSetup()
		{
			base.CustomSetup();
			if (onFixedUpdate == null)
			{
				onFixedUpdate = new UnityEvent();
			}
		}

		void FixedUpdate()
		{
			if (hasBeenSetup && entity != null && entity.hasBeenSetup && entity.enabled && this.enabled)
			{
				onFixedUpdate?.Invoke();
			}
		}
	}

	public static partial class EntityExtensions
	{
		public static void SubscribeToFixedUpdate(this AbsEntity entity, UnityAction action)
		{
			FixedUpdate_Bh tmp = entity.GetOrAddBehaviour<FixedUpdate_Bh>(true, entity.transform);
			tmp.onFixedUpdate.AddListener(action);
		}
	}
}