namespace MyCustomSystem.Events
{

	using UnityEngine;
	using UnityEngine.Events;
	using Sirenix.OdinInspector;

	[System.Serializable]
	public class AbsGameEventListener<GameEventType, UnityEvent> : IGameEventListener
		where GameEventType : IGameEvent< AbsGameEventListener<GameEventType, UnityEvent> >
		where UnityEvent : UnityEngine.Events.UnityEvent, new()
	{


		[SerializeField, InlineEditor]
		private GameEventType _gameEvent;
		public UnityEvent response;
		
		public GameEventType GameEvent 
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
		public AbsGameEventListener(GameEventType gameEvent, bool resetResponse = false)
		{
			if (response == null || resetResponse)
			{
				response = new UnityEvent();
			}
			this.GameEvent = gameEvent;
		}

		//distruttore
		~AbsGameEventListener()
		{
			Unsubscribe();
			//_gameEvent = null;
		}


		#region API
		public void Invoke()
		{
			response?.Invoke();
		}

		public void Subscribe()
		{
			this.GameEvent?.Subscribe(this);
		}

		public void Unsubscribe()
		{
			this.GameEvent?.Unsubscribe(this);
		}

		public bool IsSubscribed()
		{
			return this.GameEvent != null ? this.GameEvent.IsSubscribed(this) : false;
		}

		#endregion

	}
}