namespace OLD_SenarCustomSystem.EntityBehaviour
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using Sirenix.OdinInspector;
	using OLD_SenarCustomSystem.Events;

	[AddComponentMenu("_MyCustomSystem/EntityBehaviour/Events/GameEventListener_Bh")]
	public class GameEventListener_Bh : AbsBehaviour
    {
		public GameEventListener gameEventListner = new GameEventListener();

		[ReadOnly] public bool hasBeenSubscribed = false;

		public void Subscribe()
		{
			if (hasBeenSetup)
			{
				gameEventListner?.Subscribe();
			}
		}

		public void Unsubscribe()
		{
			if (hasBeenSetup)
			{
				gameEventListner?.Unsubscribe();
			}
		}

		protected override void CustomSetup()
		{
			base.CustomSetup();
			Subscribe();
		}

		private void OnEnable()
		{
			Subscribe();
		}

		private void OnDisable()
		{
			Unsubscribe();
		}
	}
}