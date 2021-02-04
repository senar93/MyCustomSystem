#if UNITY_EDITOR
namespace Senar.Grid.Editor
{
	using Sirenix.Utilities;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class EDITOR_CellDrawMode_Texture : EDITOR_Abs_CellDrawMode
	{
		public Texture texture;

		public override void Draw(Rect rect)
		{
			UnityEditor.EditorGUI.DrawPreviewTexture(rect, texture);
		}
	}
}
#endif