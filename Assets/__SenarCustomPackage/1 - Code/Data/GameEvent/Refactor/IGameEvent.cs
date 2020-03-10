using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameEvent<GameEventListner> 
	where GameEventListner : IGameEventListener
{
	void Invoke();
	void Subscribe(GameEventListner listener);
	void Unsubscribe(GameEventListner listener);
	bool IsSubscribed(GameEventListner listener);
	void RemoveAllListners();
}
