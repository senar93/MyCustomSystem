namespace Senar.Grid
{
    #if UNITY_EDITOR
    using Senar.Grid.Editor;
    using UnityEditor;
    #endif
    using Sirenix.OdinInspector;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

	public class MB_GenericCell : MonoBehaviour
    {
        [ReadOnly]
        public Vector2Int coords;
        [ReadOnly]
        public MB_GenericGrid grid;

        #if UNITY_EDITOR
        [ShowIf("EDITOR_HaveCellEditorData"), DisableInPlayMode, Button("Add Cell Editor Data", ButtonSizes.Gigantic), GUIColor(0.7f, 1f, 0.7f)]
        private void EDITOR_AddCellEditorDataButton()
        {
            this.gameObject.AddComponent<EDITOR_MB_Cell>();
            EditorUtility.SetDirty(this.gameObject);
            AssetDatabase.SaveAssets();
        }

        private bool EDITOR_HaveCellEditorData()
        {
            return GetComponent<EDITOR_MB_Cell>() == null;
		}
        #endif

    }
}