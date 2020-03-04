using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyCustomSystem.EntityBehaviour;
using Sirenix.OdinInspector;
using MyCustomSystem.Variables;

public class testBh : AbsBehaviour
{
	public NumericReference asd;


	[ContextMenu("Do Something"), Button("DO SOMETHING!")]
	public void DoSomething()
	{
		Debug.Log(asd.Value);
	}
}
