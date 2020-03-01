using MyCustomSystem.Attributes;
using MyCustomSystem.Attributes.Internal;
using MyCustomSystem.Attributes.Internal.Editor;
using System.Reflection;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace MyCustomSystem.Attributes.Editor
{
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