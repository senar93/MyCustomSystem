using MyCustomSystem.Attributes.Internal;
using System.Reflection;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace MyCustomSystem.Attributes.Internal.Editor
{
	// base soruce : https://gist.github.com/LotteMakesStuff/d6a9a4944fc667e557083108606b7d22

	[CustomPropertyDrawer(typeof(AbsAutohookAttribute))]
	public abstract class AbsAutohookPropertyDrawer : PropertyDrawer
	{
		protected abstract Component HowToGetComponent(Component targetObj, System.Type type);


		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			AbsAutohookAttribute autoHook = attribute as AbsAutohookAttribute;

			if(Application.isEditor && !Application.isPlaying)
			{
				// First, lets attempt to find a valid component we could hook into this property
				autoHook.currentComponent = FindAutohookTarget(property);
				if (autoHook.currentComponent != null)
				{
					// if we found something, AND the autohook is empty, lets slot it.
					// the reason were straight up looking for a target component is so we
					// can skip drawing the field if theres a valid autohook. 
					// this just looks a bit cleaner but isnt particularly safe. YMMV
					if (property.objectReferenceValue == null)
						property.objectReferenceValue = autoHook.currentComponent;
				}
			}

			if (!autoHook.hideObject ||
			   autoHook.currentComponent == null)
			{
				EditorGUI.PropertyField(position, property, label);
			}

		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			AbsAutohookAttribute autoHook = attribute as AbsAutohookAttribute;
			if(!autoHook.hideObject ||
			   autoHook.currentComponent == null)
			{
				return base.GetPropertyHeight(property, label);
			}

			return 0;
		}

		/// <summary>
		/// Takes a SerializedProperty and finds a local component that can be slotted into it.
		/// Local in this context means its a component attached to the same GameObject.
		/// This could easily be changed to use GetComponentInParent/GetComponentInChildren
		/// </summary>
		/// <param name="property"></param>
		/// <returns></returns>
		protected Component FindAutohookTarget(SerializedProperty property)
		{
			var root = property.serializedObject;

			if (root.targetObject is Component)
			{
				// first, lets find the type of component were trying to autohook...
				var type = GetTypeFromProperty(property);

				// ...then use GetComponent(type) to see if there is one on our object.
				var component = (Component)root.targetObject;
				return HowToGetComponent(component, type);
				//return component.GetComponent(type);
			}

			return null;
		}

		/// <summary>
		/// Uses reflection to get the type from a serialized property
		/// </summary>
		/// <param name="property"></param>
		/// <returns></returns>
		protected static System.Type GetTypeFromProperty(SerializedProperty property)
		{
			// first, lets get the Type of component this serialized property is part of...
			var parentComponentType = property.serializedObject.targetObject.GetType();
			// ... then, using reflection well get the raw field info of the property this
			// SerializedProperty represents...
			var fieldInfo = parentComponentType.GetField(property.propertyPath, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
			// ... using that we can return the raw .net type!
			return fieldInfo.FieldType;
		}

	}

}
#endif