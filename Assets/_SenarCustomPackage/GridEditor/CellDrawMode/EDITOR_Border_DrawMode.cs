#if UNITY_EDITOR
using Sirenix.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EDITOR_Border_DrawMode : EDITOR_FlatColor_DrawMode
{
	public Color borderColor = Color.red;
	public bool up = true;
	public bool left = true;
	public bool down = true;
	public bool right = true;


	public override void Draw(Rect rect)
	{
		Rect tmpLeft = new Rect(rect);
		Rect tmpUp = new Rect(rect);
		Rect tmpDown = new Rect(rect);
		Rect tmpRight = new Rect(rect);
		tmpLeft.xMax = tmpLeft.xMax - tmpLeft.width * 0.7f;
		tmpRight.xMin = tmpLeft.xMin - tmpLeft.width * 0.7f;

		tmpUp.yMax = tmpUp.yMax - tmpUp.height * 0.7f;

		base.Draw(rect);

		if (up) UnityEditor.EditorGUI.DrawRect(tmpUp.Padding(1f), borderColor);
		if (left) UnityEditor.EditorGUI.DrawRect(tmpLeft.Padding(1f), borderColor);
		if (down) UnityEditor.EditorGUI.DrawRect(tmpDown.Padding(1f), borderColor);
		if (right) UnityEditor.EditorGUI.DrawRect(tmpRight.Padding(1f), borderColor);

	}
}
#endif