using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyCustomSystem.EntityBehaviour.GenericBehaviour
{
	public class DebugCallerBehaviour : AbsCustomBehaviour
	{
		public void DebugLogCall(string message) 
		{
			Debug.Log(entity.name + " --> " + message);
		}

		public void DebugWarningCall(string message)
		{
			Debug.LogWarning(entity.name + " --> " + message);
		}

		public void DebugErrorCall(string message)
		{
			Debug.LogError(entity.name + " --> " + message);
		}
	}
}