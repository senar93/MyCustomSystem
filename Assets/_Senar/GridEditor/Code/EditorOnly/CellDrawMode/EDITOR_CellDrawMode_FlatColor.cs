#if UNITY_EDITOR
namespace Senar.Grid.Editor
{
	using Sirenix.Utilities;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class EDITOR_CellDrawMode_FlatColor : EDITOR_Abs_CellDrawMode
	{
		public Color backgroundColor = Color.green;

		public override void Draw(Rect rect)
		{
			UnityEditor.EditorGUI.DrawRect(rect.Padding(1f), backgroundColor);
		}
	}
}
#endif