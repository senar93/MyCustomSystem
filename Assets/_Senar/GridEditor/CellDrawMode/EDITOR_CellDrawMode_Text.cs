#if UNITY_EDITOR
namespace Senar.Grid.Editor
{
	using Sirenix.Utilities;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class EDITOR_CellDrawMode_Text : EDITOR_Abs_CellDrawMode
	{
		public string text;
		public GUIStyle style;

		public override void Draw(Rect rect)
		{
			if (style != null)
				UnityEditor.EditorGUI.DropShadowLabel(rect, text, style);
			else
				UnityEditor.EditorGUI.DropShadowLabel(rect, text);
		}
	}
}
#endif