using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MyCustomSystem.EntityBehaviour.GenericBehaviour
{
	public class FunctionGroupCallerBehaviour : AbsCustomBehaviour
	{
		[SerializeField, Space] bool callOnSetup = false;
		public UnityEvent calledFunction;

		protected override void CustomSetup()
		{
			base.CustomSetup();
			if(callOnSetup)
			{
				CallFunctions();
			}
		}

		public void CallFunctions() 
		{
			calledFunction.Invoke();
		}

	}
}