namespace MyCustomSystem.Variables.Abstract
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using Sirenix.OdinInspector;
	using MyCustomSystem.Variables.Interface;


	public abstract class AbsVariable_So<T> : SerializedScriptableObject, IHaveValue<T>
	{
		public abstract T Value { get; set; }

		[ShowInInspector, ReadOnly]
		public abstract bool CanSetValue { get; }
	}
}