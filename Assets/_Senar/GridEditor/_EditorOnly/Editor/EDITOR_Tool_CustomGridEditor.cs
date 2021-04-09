#if UNITY_EDITOR
namespace Senar.Grid.Editor
{
    using Sirenix.OdinInspector.Editor;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEngine;
    using Senar.Grid.Data;

    public class EDITOR_Tool_CustomGridEditor : OdinMenuEditorWindow
    {
        [MenuItem("Custom Tools/Grid Editor")]
        private static void OpenWindow()
        {
            GetWindow<EDITOR_Tool_CustomGridEditor>().Show();
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree();
            tree.Selection.SupportsMultiSelect = false;

            tree.Add("Settings", SingletonSO_GridEditorSettings.Instance);

            tree.Add("NEW GRID", new EDITOR_ToolWindow_GridEditorTab());

            SO_SenarGrid[] allGrids = Resources.LoadAll<SO_SenarGrid>("");
            foreach (SO_SenarGrid currentGrid in allGrids)
            {
                EDITOR_ToolWindow_GridEditorTab tmpGridEditor = new EDITOR_ToolWindow_GridEditorTab();
                tmpGridEditor.LoadGridAsset(currentGrid);
                tree.Add("Grids/" + currentGrid.name, tmpGridEditor);
            }

            return tree;
        }
    }
}
#endif