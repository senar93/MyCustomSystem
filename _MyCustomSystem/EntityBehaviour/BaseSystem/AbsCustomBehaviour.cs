using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Linq;

namespace MyCustomSystem.EntityBehaviour
{
	public abstract class AbsCustomBehaviour : SerializedMonoBehaviour
	{
		[ReadOnly] public AbsGenericEntity entity;
		/// <summary>
		/// dal minore al maggiore
		/// </summary>
		public int loadOrder;


		protected virtual void CustomOnDestroy() { }
		protected virtual void CustomSetup() { }


		public void Setup(AbsGenericEntity entity)
		{
			this.entity = entity;
			CustomSetup();
		}

		void OnDestroy()
		{
			entity.customBehaviourList.Remove(this);
			CustomOnDestroy();
		}


	}
}