#if UNITY_EDITOR
namespace OLD_SenarCustomSystem.Attributes.Editor
{
	using OLD_SenarCustomSystem.Attributes;
	using OLD_SenarCustomSystem.Attributes.Internal;
	using OLD_SenarCustomSystem.Attributes.Internal.Editor;
	using System.Reflection;
	using UnityEditor;
	using UnityEngine;


	[CustomPropertyDrawer(typeof(SelfAutohookAttribute))]
	public class SelfAutohookPropertyDrawer : AbsAutohookPropertyDrawer
	{
		protected override Component HowToGetComponent(Component targetObj, System.Type type)
		{
			return targetObj.GetComponent(type);
		}
	}
}
#endif