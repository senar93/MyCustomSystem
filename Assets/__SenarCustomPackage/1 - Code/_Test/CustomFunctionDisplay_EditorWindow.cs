using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using MyCustomSystem.Variables.Data;

public class CustomFunctionDisplay_EditorWindow : OdinEditorWindow
{
	[MenuItem("SenarCustomSystem/Custom Function Display")]
    private static void OpenWindow()
	{
		GetWindow<CustomFunctionDisplay_EditorWindow>().Show();
	}


	#region PARAMETERS
	//forse è meglio spostare tutte queste opzioni di configurazione in uno scriptable a parte per renderle persistenti
	[FoldoutGroup("Background Option")]
	public Vector2 startPosition = new Vector2(30, 30);
	[FoldoutGroup("Background Option")]
	public Vector2 rectSize = new Vector2(1000, 1000);
	[FoldoutGroup("Background Option")]
	public Vector2 extraBackgroundSize = new Vector2(5, 5);
	[FoldoutGroup("Background Option")]
	public Color rectColor = new Color(0, 0, 0, 0.3f);

	[FoldoutGroup("Grid Option"), MinValue(1)]
	public int xGridFrequence = 40;
	[FoldoutGroup("Grid Option"), MinValue(1)]
	public int yGridFrequence = 40;
	[FoldoutGroup("Grid Option")]
	public Color gridBackgroundColor = new Color(1, 1, 1, 0.3f);

	[MinValue(2)]
	public int evaluationFrequence = 100;

	#endregion

	[CustomValueDrawer("CustomDrawerTest"), ShowInInspector]
	static public FormulaDoubleVariable_So asd;



	private FormulaDoubleVariable_So CustomDrawerTest(FormulaDoubleVariable_So value, GUIContent label)
	{
		DrawGraph(value, label);
		return (FormulaDoubleVariable_So) EditorGUILayout.ObjectField(value, typeof(FormulaDoubleVariable_So), false);
	}


	private void DrawGraph(FormulaDoubleVariable_So target, GUIContent label)
	{
		Handles.BeginGUI();

		DrawGraphBackground();
		DrawFunction(target);

		Handles.EndGUI();
	}

	private void DrawGraphBackground()
	{
		Rect testRect = new Rect(startPosition.x - extraBackgroundSize.x, startPosition.y - extraBackgroundSize.y,
								 rectSize.x + extraBackgroundSize.x * 2, rectSize.y + extraBackgroundSize.y * 2);
		EditorGUI.DrawRect(testRect, rectColor);

		Handles.color = gridBackgroundColor;
		DrawVerticalGrid();
		DrawHorizontalGrid();



	}

	private void DrawVerticalGrid(){
		Vector2 tmpStartPoint = startPosition;
		Vector2 tmpEndPoint = startPosition + (rectSize * Vector2.up);
		Handles.DrawLine(tmpStartPoint, tmpEndPoint);
		for (int i = 0; i < xGridFrequence; i++)
		{
			tmpStartPoint.x += rectSize.x / xGridFrequence;
			tmpEndPoint.x += rectSize.x / xGridFrequence;
			Handles.DrawLine(tmpStartPoint, tmpEndPoint);
		}
	}

	private void DrawHorizontalGrid()
	{
		Vector2 tmpStartPoint = startPosition;
		Vector2 tmpEndPoint = startPosition + (rectSize * Vector2.right);
		Handles.DrawLine(tmpStartPoint, tmpEndPoint);
		for (int i = 0; i < yGridFrequence; i++)
		{
			tmpStartPoint.y += rectSize.y / yGridFrequence;
			tmpEndPoint.y += rectSize.y / yGridFrequence;
			Handles.DrawLine(tmpStartPoint, tmpEndPoint);
		}
	}


	private void DrawFunction(FormulaDoubleVariable_So target)
	{
		//Handles.color = Color.blue;
		//Handles.DrawLine(startPosition, startPosition + rectSize);
		/*points[0] = startPosition;
		//draw Vertical
		for (int i = 1; i < evaluationFrequence; i++)
		{
			points[i] = points[i - 1];
			points[i].y += rectSize.x / evaluationFrequence;
		}
		Handles.DrawLines(points);*/
	}

}
