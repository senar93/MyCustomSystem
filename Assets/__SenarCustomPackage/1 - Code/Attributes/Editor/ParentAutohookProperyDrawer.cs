using MyCustomSystem.Attributes;
using MyCustomSystem.Attributes.Internal;
using MyCustomSystem.Attributes.Internal.Editor;
using System.Reflection;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace MyCustomSystem.Attributes.Editor
{
	[CustomPropertyDrawer(typeof(ParentAutohookAttribute))]
	public class ParentAutohookProperyDrawer : AbsAutohookPropertyDrawer
	{
		protected override Component HowToGetComponent(Component targetObj, System.Type type)
		{
			Component findObject = targetObj.GetComponentInParent(type);
			ParentAutohookAttribute autoHook = attribute as ParentAutohookAttribute;

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