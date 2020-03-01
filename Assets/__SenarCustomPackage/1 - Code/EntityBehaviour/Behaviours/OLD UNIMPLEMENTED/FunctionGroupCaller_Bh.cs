/*namespace MyCustomSystem.EntityBehaviour
{
	using MyCustomSystem.EntityBehaviour;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using Sirenix.OdinInspector;
	using MyCustomSystem.Attributes;
	using UnityEngine.Events;

	public class FunctionGroupCaller_Bh : AbsBehaviour
	{
		[SerializeField, Space] bool callOnSetup = false;
		[TextArea(), Space] private string inspectorDescription;
		[Space] public UE_Entity calledFunction;

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
			if(hasBeenSetup)
				calledFunction.Invoke(entity);
		}

	}
}*/