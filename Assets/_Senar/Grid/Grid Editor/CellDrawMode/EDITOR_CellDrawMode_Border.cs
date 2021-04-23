#if UNITY_EDITOR
namespace Senar.Grid.Editor
{
	using Sirenix.Utilities;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class EDITOR_CellDrawMode_Border : EDITOR_Abs_CellDrawMode
	{
		public Color borderColor = Color.red;
		public bool up = true;
		public bool left = true;
		public bool down = true;
		public bool right = true;

		public float upSize = 0.7f;
		public float leftSize = 0.7f;
		public float downSize = 0.7f;
		public float rightSize = 0.7f;

		public override void Draw(Rect rect)
		{
			Rect tmpLeft = new Rect(rect);
			Rect tmpUp = new Rect(rect);
			Rect tmpDown = new Rect(rect);
			Rect tmpRight = new Rect(rect);
			tmpUp.yMax = tmpUp.yMax - tmpUp.height * upSize;
			tmpLeft.xMax = tmpLeft.xMax - tmpLeft.width * leftSize;
			tmpDown.yMin = tmpDown.yMin + tmpDown.height * downSize;
			tmpRight.xMin = tmpRight.xMin + tmpRight.width * rightSize;

			if (up) UnityEditor.EditorGUI.DrawRect(tmpUp.Padding(1f), borderColor);
			if (left) UnityEditor.EditorGUI.DrawRect(tmpLeft.Padding(1f), borderColor);
			if (down) UnityEditor.EditorGUI.DrawRect(tmpDown.Padding(1f), borderColor);
			if (right) UnityEditor.EditorGUI.DrawRect(tmpRight.Padding(1f), borderColor);

		}
	}
}
#endif