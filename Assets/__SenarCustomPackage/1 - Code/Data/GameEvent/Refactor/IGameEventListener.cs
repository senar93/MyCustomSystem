using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameEventListener
{
	//IGameEvent<IGameEventListener> GameEvent { get; set; }

	void Invoke();
	void Subscribe();
	void Unsubscribe();
	bool IsSubscribed();
}