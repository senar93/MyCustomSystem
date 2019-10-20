using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Events;

namespace MyCustomSystem.EntityBehaviour.GenericBehaviour
{
	public class DestroyBehaviour : AbsGenericBehaviour
	{

		public void DestroyTarget(GameObject target) 
		{
			Destroy(target);
		}

		public void DestroyEntity() 
		{
			DestroyTarget(entity.gameObject);
		}


	}
}
