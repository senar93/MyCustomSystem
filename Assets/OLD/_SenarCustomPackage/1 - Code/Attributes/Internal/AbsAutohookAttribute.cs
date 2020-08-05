namespace OLD_SenarCustomSystem.Attributes.Internal
{
	using System;
	using UnityEngine;


	[AttributeUsage(AttributeTargets.Field)]
	public abstract class AbsAutohookAttribute : PropertyAttribute 
	{
		public bool hideObject;
		public Component currentComponent;
	}

}
