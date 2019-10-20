using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyCustomSystem.EntityBehaviour
{
	/* contiene: 
	 *	le variabili private che definiscono il comportamento standard dell'behaviour, invisibili ai figli
	 *	le property e le funzioni che definiscono se alcuni elementi sono visibili o meno in inspector
	 */
	public abstract partial class AbsGenericBehaviour : SerializedMonoBehaviour
	{
		[PropertyOrder(-999998), ShowIf("showIsUniqueInInspector"), DisableInPlayMode, SerializeField]
		private bool _isUnique = false;
		[PropertyOrder(-999996), ShowIf("isUnique"), SerializeField]
		private IsUniqueExceptionBehaviourEnum _isUniqueBehaviour = IsUniqueExceptionBehaviourEnum.destroyNew_BlockSetup;

		[PropertyOrder(-999995), ShowIf("showCanBeSetupMoreThanOneTimeInInspector"), SerializeField]
		private bool _canBeSetupMoreThanOneTime = false;


		/// <summary>
		/// se TRUE mostra _canBeSetupMoreThanOneTime in inspector
		/// </summary>
		protected virtual bool showCanBeSetupMoreThanOneTimeInInspector { get => true; }
		/// <summary>
		/// se TRUE mostra _isUnique in inspector
		/// </summary>
		protected virtual bool showIsUniqueInInspector { get => true; }
		/// <summary>
		/// se TRUE mostra _isUniqueBehaviour in inspector (se il behaviour ha anche  isUnique a TRUE)
		/// </summary>
		protected virtual bool showIsUniqueBehaviourInInspector { get => true; }

		protected virtual bool ShowIsUniqueBehaviourInInspector()
		{
			return showIsUniqueBehaviourInInspector && isUnique;
		}

	}
}