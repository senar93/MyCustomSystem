using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyCustomSystem.EntityBehaviour;

public class testBh : AbsBehaviour
{

	[ContextMenu("Do Something")]
	void DoSomething()
	{
		Debug.Log("Perform operation");
	}
}
