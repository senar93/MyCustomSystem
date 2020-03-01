using MyCustomSystem.Attributes;
using MyCustomSystem.Attributes.Internal;
using MyCustomSystem.Attributes.Internal.Editor;
using System.Reflection;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace MyCustomSystem.Attributes.Editor
{
	[CustomPropertyDrawer(typeof(ChildAutohookAttribute))]
	public class ChildAutohookPropertyDrawer : AbsAutohookPropertyDrawer
	{
		protected override Component HowToGetComponent(Component targetObj, System.Type type)
		{
			Component findObject = targetObj.GetComponentInChildren(type);
			ChildAutohookAttribute autoHook = attribute as ChildAutohookAttribute;

			if (findObject == null || 
				(autoHook.excludeSelf && (targetObj.gameObject == findObject.gameObject)))
			{
				return null;
			}

			return findObject;
		}
	}
}
#endif