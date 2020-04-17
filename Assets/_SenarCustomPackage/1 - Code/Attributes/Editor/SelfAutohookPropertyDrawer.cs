#if UNITY_EDITOR
namespace SenarCustomSystem.Attributes.Editor
{
	using SenarCustomSystem.Attributes;
	using SenarCustomSystem.Attributes.Internal;
	using SenarCustomSystem.Attributes.Internal.Editor;
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