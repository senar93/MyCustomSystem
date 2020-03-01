using MyCustomSystem.Attributes.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyCustomSystem.Attributes
{
	/// <summary>
	/// cerca automaticamente una referenza all'oggetto associato nel GameObject
	/// </summary>
	[AttributeUsage(AttributeTargets.Field)]
	public class ParentAutohookAttribute : AbsAutohookAttribute
	{
		public bool excludeSelf;

		public ParentAutohookAttribute(bool _hideObject = false, bool _excludeSelf = false)
		{
			hideObject = _hideObject;
			excludeSelf = _excludeSelf;
			currentComponent = null;
		}
	}

}