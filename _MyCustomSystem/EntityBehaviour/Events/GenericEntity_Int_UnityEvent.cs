using MyCustomSystem.EntityBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MyCustomSystem.EntityBehaviour.Events
{
	[System.Serializable]
	public class GenericEntity_Int_UnityEvent : UnityEvent<AbsGenericEntity, int> { }

}