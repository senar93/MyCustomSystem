#if UNITY_EDITOR
namespace SenarCustomSystem.Attributes.Editor
{
	using SenarCustomSystem.Attributes;
	using SenarCustomSystem.Attributes.Internal;
	using SenarCustomSystem.Attributes.Internal.Editor;
	using System.Reflection;
	using UnityEditor;
	using UnityEngine;


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