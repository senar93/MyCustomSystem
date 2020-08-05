namespace OLD_SenarCustomSystem.Attributes
{
	using OLD_SenarCustomSystem.Attributes.Internal;
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;


	/// <summary>
	/// cerca automaticamente una referenza all'oggetto associato nel GameObject
	/// </summary>
	[AttributeUsage(AttributeTargets.Field)]
	public class SelfAutohookAttribute : AbsAutohookAttribute
	{
		public SelfAutohookAttribute(bool _hideObject = false)
		{
			hideObject = _hideObject;
			currentComponent = null;
		}
	}

}
