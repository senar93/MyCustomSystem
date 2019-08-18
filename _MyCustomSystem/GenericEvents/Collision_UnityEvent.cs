using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MyCustomSystem.Events
{
	[System.Serializable]
	public class Collision_UnityEvent : UnityEvent<Collision> { }
}