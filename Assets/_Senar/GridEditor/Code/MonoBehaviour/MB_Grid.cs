namespace Senar.Grid
{
    using UnityEngine;
    using Sirenix.OdinInspector;
    using System.Collections.Generic;
    #if UNITY_EDITOR
    using Senar.Grid.Editor;
    using UnityEditor;
    #endif

    [AddComponentMenu("Senar/Grid/Grid", 1)]
    public class MB_Grid : SerializedMonoBehaviour
    {
        [ReadOnly]
        public MB_Cell[,] currentGrid;


        #if UNITY_EDITOR
        [ShowIf("EDITOR_HasGridInstantiator"), DisableInPlayMode, Button("Add Grid Instantiator", ButtonSizes.Gigantic), GUIColor(0.7f, 1f, 0.7f)]
        private void EDITOR_AddGridInstantiatorButton()
        {
            EDITOR_MB_GridInstantiator tmp = this.gameObject.AddComponent<EDITOR_MB_GridInstantiator>();
            tmp.EDITOR_SetGrid(this);
            EditorUtility.SetDirty(this.gameObject);
            AssetDatabase.SaveAssets();
        }

        private bool EDITOR_HasGridInstantiator()
        {
            return GetComponent<EDITOR_MB_GridInstantiator>() == null;
        }
        #endif

    }
}