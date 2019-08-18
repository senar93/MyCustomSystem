using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyCustomSystem.EntityBehaviour
{
	public abstract class AbsUniqueCustomBehaviour : AbsCustomBehaviour
	{
		protected override void CustomSetup()
		{
			base.CustomSetup();
			if (entity.customBehaviourList.FindAll(x => x.GetType() == this.GetType()).Count > 1)
			{
				Debug.LogError(this.GetType() + " è gia presente in " + entity);
			}
		}
	}
}