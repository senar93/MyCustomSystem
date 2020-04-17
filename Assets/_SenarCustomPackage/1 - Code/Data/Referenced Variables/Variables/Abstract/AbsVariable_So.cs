namespace SenarCustomSystem.Variables.Abstract
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using Sirenix.OdinInspector;
	using SenarCustomSystem.Variables.Interface;


	public abstract class AbsVariable_So<T> : SerializedScriptableObject, IReferenceableVariabile<T>
	{
		public abstract T Value { get; set; }

		[SerializeField, ReadOnly]
		public abstract bool CanSetValue { get; }
	}
}