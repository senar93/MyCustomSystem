namespace MyCustomSystem.EntityBehaviour
{
	using UnityEngine;
	using UnityEngine.Events;

	[AddComponentMenu("_MyCustomSystem/EntityBehaviour/Unique Behaviour/Late Update"),
	 RequireComponent(typeof(AbsEntity)),
	 DisallowMultipleComponent]
	public class LateUpdate_Bh : AbsDynamicBehaviour
	{
		[Space]
		public UnityEvent onLateUpdate;

		protected override void CustomSetup()
		{
			base.CustomSetup();
			if (onLateUpdate == null)
			{
				onLateUpdate = new UnityEvent();
			}
		}

		void LateUpdate()
		{
			if (hasBeenSetup && entity != null && entity.hasBeenSetup && entity.enabled && this.enabled)
			{
				onLateUpdate?.Invoke();
			}
		}
	}

	public static partial class EntityExtensions
	{
		public static void SubscribeToLateUpdate(this AbsEntity entity, UnityAction action)
		{
			LateUpdate_Bh tmp = entity.GetOrAddBehaviour<LateUpdate_Bh>(true, entity.transform);
			tmp.onLateUpdate.AddListener(action);
		}
	}
}