namespace MyCustomSystem.Events
{
	using UnityEngine;
	using UnityEngine.Events;
	using Sirenix.OdinInspector;

	[AddComponentMenu("_MyCustomSystem/Events/Event Listener")]
	public class GameEventListener_Mb2 : SerializedMonoBehaviour
	{
		public IGameEventListener gameEventListner;

		private void OnEnable()
		{
			gameEventListner.Subscribe();
		}

		private void OnDisable()
		{
			gameEventListner.Unsubscribe();
		}

	}
}