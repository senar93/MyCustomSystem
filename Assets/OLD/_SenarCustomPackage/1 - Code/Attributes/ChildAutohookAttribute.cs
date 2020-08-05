namespace OLD_SenarCustomSystem.Attributes
{
	using OLD_SenarCustomSystem.Attributes.Internal;
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	/// <summary>
	/// cerca automaticamente una referenza all'oggetto associato nei figli del GameObject
	/// </summary>
	[AttributeUsage(AttributeTargets.Field)]
	public class ChildAutohookAttribute : AbsAutohookAttribute
	{
		public bool excludeSelf;

		public ChildAutohookAttribute(bool _hideObject = false, bool _excludeSelf = false)
		{
			hideObject = _hideObject;
			excludeSelf = _excludeSelf;
			currentComponent = null;
		}
	}

}