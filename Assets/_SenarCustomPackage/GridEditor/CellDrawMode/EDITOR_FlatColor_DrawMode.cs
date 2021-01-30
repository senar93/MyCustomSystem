#if UNITY_EDITOR
using Sirenix.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EDITOR_FlatColor_DrawMode : EDITOR_AbstractCellDrawMode
{
	public Color backgroundColor = Color.green;

	public override void Draw(Rect rect)
	{
		UnityEditor.EditorGUI.DrawRect(rect.Padding(1f), backgroundColor);
	}
}
#endif