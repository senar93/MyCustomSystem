/*namespace MyCustomSystem.EntityBehaviour
{
	using MyCustomSystem.EntityBehaviour;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using Sirenix.OdinInspector;
	using MyCustomSystem.Attributes;

	public class DestroyTarget_Bh : AbsBehaviour
	{
		public void DestroyTargetGameObject(GameObject target) 
		{
			if(hasBeenSetup)
				Destroy(target);
		}

		/// <summary>
		/// distrugge l'entity del bersaglio, se esiste
		/// </summary>
		/// <param name="target"></param>
		public void DestroyTargetEntity(AbsBehaviour target)
		{
			if (hasBeenSetup)
				Destroy(target?.entity);
		}
		
		/// <summary>
		/// distrugge l'entity del bersaglio, se esiste
		/// </summary>
		/// <param name="target"></param>
		public void DestroyTargetEntity(AbsEntity target)
		{
			if (hasBeenSetup)
				Destroy(target);
		}

		/// <summary>
		/// distrugge l'entity del bersaglio, se esiste
		/// </summary>
		/// <param name="target"></param>
		public void DestroyTargetEntity(AbsPatch target)
		{
			if (hasBeenSetup)
				Destroy(target?.patchedBehaviour?.entity);
		}

		/// <summary>
		/// distrugge il behaviour del bersaglio, se esiste
		/// </summary>
		/// <param name="target"></param>
		public void DestroyTargetBehaviour(AbsBehaviour target)
		{
			if (hasBeenSetup)
				Destroy(target);
		}

		/// <summary>
		/// distrugge il behaviour del bersaglio, se esiste
		/// </summary>
		/// <param name="target"></param>
		public void DestroyTargetBehaviour(AbsPatch target)
		{
			if (hasBeenSetup)
				Destroy(target?.patchedBehaviour);
		}

		/// <summary>
		/// distrugge la patch del bersaglio, se esiste
		/// </summary>
		/// <param name="target"></param>
		public void DestroyTargetPatch(AbsPatch target)
		{
			if (hasBeenSetup)
				Destroy(target);
		}
	}
}
*/