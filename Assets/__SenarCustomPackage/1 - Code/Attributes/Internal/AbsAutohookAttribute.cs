using System;
using UnityEngine;

namespace MyCustomSystem.Attributes.Internal
{
	[AttributeUsage(AttributeTargets.Field)]
	public abstract class AbsAutohookAttribute : PropertyAttribute 
	{
		public bool hideObject;
		public Component currentComponent;
	}

}
