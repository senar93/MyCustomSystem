using Sirenix.OdinInspector.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EDITOR_CustomGridEditor : OdinMenuEditorWindow
{
    [MenuItem("Custom Tools/Grid Editor")]
    private static void OpenWindow()
    {
        GetWindow<EDITOR_CustomGridEditor>().Show();
    }

	protected override OdinMenuTree BuildMenuTree()
	{
        var tree = new OdinMenuTree();
        tree.Selection.SupportsMultiSelect = false;

        tree.Add("NEW GRID", new EDITOR_GridEditorTab());

        CustomGridSO[] allGrids = Resources.LoadAll<CustomGridSO>("");
        foreach(CustomGridSO currentGrid in allGrids)
        {
            EDITOR_GridEditorTab tmpGridEditor = new EDITOR_GridEditorTab();
            tmpGridEditor.LoadGridAsset(currentGrid);
            tree.Add("Grids/" + currentGrid.name, tmpGridEditor);
        }

        return tree;
    }
}
