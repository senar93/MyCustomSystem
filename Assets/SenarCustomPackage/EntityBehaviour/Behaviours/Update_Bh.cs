namespace MyCustomSystem.EntityBehaviour
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.Events;

	[AddComponentMenu("_MyCustomSystem/EntityBehaviour/Unique Behaviour/Update"),
	 RequireComponent(typeof(AbsEntity)),
	 DisallowMultipleComponent]
	public class Update_Bh : AbsDynamicBehaviour
	{
		[Space]
		public UnityEvent onUpdate;

		protected override void CustomSetup()
		{
			base.CustomSetup();
			if (onUpdate == null)
			{
				onUpdate = new UnityEvent();
			}
		}

		void Update()
		{
			if (hasBeenSetup && entity != null && entity.hasBeenSetup && entity.enabled && this.enabled)
			{
				onUpdate?.Invoke();
			}
		}
	}

	public static partial class EntityExtensions
	{
		public static void SubscribeToUpdate(this AbsEntity entity, UnityAction action)
		{
			Update_Bh tmp = entity.GetOrAddBehaviour<Update_Bh>(true, entity.transform);
			tmp.onUpdate.AddListener(action);
		}
	}
}