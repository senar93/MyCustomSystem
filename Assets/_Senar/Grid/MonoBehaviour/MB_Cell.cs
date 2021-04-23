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

	[AddComponentMenu("Senar/Grid/Cell", 0)]
    public class MB_Cell : MonoBehaviour
    {
        [ReadOnly]
        public Vector2Int coords;
        [ReadOnly, Required("The Cells must be instantiated in prefabs or scene from the Grid")]
        public MB_Grid grid;

        #if UNITY_EDITOR
        public UltEvents.UltEvent<MB_Cell> EDITOR_OnInstantiateInCell = new UltEvents.UltEvent<MB_Cell>();


        [ShowIf("EDITOR_HaveCellEditorData"), DisableInPlayMode, Button("Add Cell Editor Data", ButtonSizes.Gigantic), GUIColor(0.7f, 1f, 0.7f)]
        private void EDITOR_AddCellEditorDataButton()
        {
            EDITOR_MB_GridEditorElement tmp = this.gameObject.AddComponent<EDITOR_MB_GridEditorElement>();
            tmp.EDITOR_SetCell(this);
            EditorUtility.SetDirty(this.gameObject);
            AssetDatabase.SaveAssets();
        }

        private bool EDITOR_HaveCellEditorData()
        {
            return GetComponent<EDITOR_MB_GridEditorElement>() == null;
		}
        #endif

    }
}