#if UNITY_EDITOR
namespace OLD_SenarCustomSystem.Attributes.Editor
{
	using OLD_SenarCustomSystem.Attributes;
	using OLD_SenarCustomSystem.Attributes.Internal;
	using OLD_SenarCustomSystem.Attributes.Internal.Editor;
	using System.Reflection;
	using UnityEditor;
	using UnityEngine;


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