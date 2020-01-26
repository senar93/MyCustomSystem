namespace MyCustomSystem.Events
{

	using UnityEngine;
	using UnityEngine.Events;
	using Sirenix.OdinInspector;

	[System.Serializable]
	public class GameEventListener
	{
		[SerializeField]
		private GameEvent _gameEvent;
		public UnityEvent response;
		
		public GameEvent gameEvent 
		{
			get 
			{
				return _gameEvent;
			}
			set
			{
				bool previouslySubscribed = IsSubscribed();
				Unsubscribe();
				_gameEvent = value;
				if(previouslySubscribed)
				{
					Subscribe();
				}
			}
		}

		//costruttore
		public GameEventListener(GameEvent gameEvent = null, bool resetResponse = false)
		{
			if (response == null || resetResponse)
			{
				response = new UnityEvent();
			}
			this.gameEvent = gameEvent;
		}

		//distruttore
		~GameEventListener()
		{
			Unsubscribe();
			_gameEvent = null;
		}


		#region API
		public void OnInvoke()
		{
			response?.Invoke();
		}

		public void Subscribe()
		{
			gameEvent?.Subscribe(this);
		}

		public void Unsubscribe()
		{
			gameEvent?.Unsubscribe(this);
		}

		public bool IsSubscribed()
		{
			return gameEvent != null ? gameEvent.IsSubscribed(this) : false;
		}

		#endregion

	}
}