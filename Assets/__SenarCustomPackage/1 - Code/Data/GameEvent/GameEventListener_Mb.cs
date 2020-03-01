namespace MyCustomSystem.Events
{
	using UnityEngine;
	using UnityEngine.Events;
	using Sirenix.OdinInspector;

	[AddComponentMenu("_MyCustomSystem/Events/Event Listener")]
	public class GameEventListener_Mb : SerializedMonoBehaviour
	{
		public GameEventListener gameEventListner = new GameEventListener();

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