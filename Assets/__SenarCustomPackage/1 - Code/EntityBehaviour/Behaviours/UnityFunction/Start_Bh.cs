namespace MyCustomSystem.EntityBehaviour
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.Events;

	[AddComponentMenu("_MyCustomSystem/EntityBehaviour/Unique Behaviour/Start"),
	 RequireComponent(typeof(AbsEntity)),
	 DisallowMultipleComponent]
	public class Start_Bh : AbsDynamicBehaviour
	{
		[Space]
		public UnityEvent onStart;

		protected override void CustomSetup()
		{
			base.CustomSetup();
			if (onStart == null)
			{
				onStart = new UnityEvent();
			}
		}

		void Start()
		{
			if (hasBeenSetup && entity != null && entity.hasBeenSetup && entity.enabled && this.enabled)
			{
				onStart?.Invoke();
			}
		}
	}

	public static partial class EntityExtensions
	{
		public static void SubscribeToStart(this AbsEntity entity, UnityAction action)
		{
			Start_Bh tmp = entity.GetOrAddBehaviour<Start_Bh>(true, entity.transform);
			tmp.onStart.AddListener(action);
		}
	}
}