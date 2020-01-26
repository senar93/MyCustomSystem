namespace MyCustomSystem.Events
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using Sirenix.OdinInspector;

	[CreateAssetMenu(menuName = "MyCustomSystem/Global Event")]
	public class GameEvent : SerializedScriptableObject, ISerializationCallbackReceiver
	{
		#if UNITY_EDITOR || DEBUG
		[ShowInInspector]
		private bool subscribeDebug = false;
		[ShowInInspector]
		private bool unsubscribeDebug = false;
		[ShowInInspector]
		private bool invokeDebug = false;
		#endif

		[ShowInInspector, ReadOnly, Space]
		private List<GameEventListener> listeners = new List<GameEventListener>();


		#region API
		public void RemoveAllListners() 
		{
			#if UNITY_EDITOR || DEBUG
			if (subscribeDebug || unsubscribeDebug)
			{
				Debug.Log("all listners of '" + this + "' has been removed");
			}
			#endif

			listeners = new List<GameEventListener>();
		}

		public void Subscribe(GameEventListener listener)
		{

			if (listener != null)
			{				
				if (!IsSubscribed(listener))
				{
					#if UNITY_EDITOR || DEBUG
					if (subscribeDebug)
					{
						Debug.Log("'" + listener + "' has been subscribed to '" + this + "'");
					}
					#endif

					listeners.Add(listener);
				}
				#if UNITY_EDITOR || DEBUG
				else if (subscribeDebug)
				{
					Debug.LogWarning("'" + listener + "' tried to subscribe for the event '" +
									 this + "' but he was already subscribed for this event");
				}
				#endif
			}
			#if UNITY_EDITOR || DEBUG
			else if (subscribeDebug)
			{
				Debug.LogWarning("'NULL' tried to subscribe for the event, but only non NULL object can be subscribed");
			}
			#endif
		}

		public void Unsubscribe(GameEventListener listener)
		{
			#if UNITY_EDITOR || DEBUG
			if (unsubscribeDebug)
			{
				if (IsSubscribed(listener))
				{
					Debug.Log("'" + listener + "' has been unsubscribed to '" + this + "'");
				}
				else
				{
					Debug.LogWarning("'" + listener + "' tried to unsubscribed for the event '" +
										this + "' but he was not subiscribe to this event");
				}
			}
			#endif

			listeners?.Remove(listener);
		}

		public void Invoke()
		{
			#if UNITY_EDITOR || DEBUG
			if (invokeDebug)
			{
				Debug.Log("the event '" + this + "' has been invoked");
			}
			#endif

			for (int i = 0; i < listeners?.Count; i++)
			{
				if (listeners[i] != null)
				{
					#if UNITY_EDITOR || DEBUG
					if (invokeDebug)
					{
						Debug.Log("'" + listeners[i] + "' responded to the invocation of '" + this + "'");
					}
					#endif

					listeners[i].OnInvoke();
				}
			}

			#if UNITY_EDITOR || DEBUG
			if (invokeDebug)
			{
				Debug.Log("the event '" + this + "' it's over");
			}
			#endif
		}

		public bool IsSubscribed(GameEventListener listener)
		{
			return listeners?.Find(x => x == listener) != null;
		}

		#endregion

		#region "EDITOR"
		[Button("Force Invoke", ButtonSizes.Gigantic), DisableInEditorMode, PropertyOrder(int.MinValue)]
		private void ForceInvoke()
		{
			#if UNITY_EDITOR || DEBUG
			Debug.Log("the event '" + this + "' has been invoked by YOU from UNITY INSPECTOR");
			#endif

			Invoke();
		}

		[Button("Remove NULL Elements", ButtonSizes.Medium), PropertyOrder(int.MinValue + 1)]
		public void RemoveNullElements()
		{
			listeners.RemoveAll(x => x == null);
		}

		#endregion

		#region UNITY
		protected override void OnAfterDeserialize()
		{
			listeners = new List<GameEventListener>();
			base.OnAfterDeserialize();
		}
		#endregion

	}
}
