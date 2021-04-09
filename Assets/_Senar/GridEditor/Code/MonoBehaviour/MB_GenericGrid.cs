namespace Senar.Grid
{
    using UnityEngine;
    using Sirenix.OdinInspector;
    using System.Collections.Generic;
    using Senar.Grid.Data;
    #if UNITY_EDITOR
    using Senar.Grid.Editor;
    using UnityEditor;
    #endif

    public class MB_GenericGrid : SerializedMonoBehaviour
    {
        [FoldoutGroup("References"), Required]
        public SO_SenarGrid gridDataReference;
        [FoldoutGroup("References"), Required]
        public Transform cellFather;

        [FoldoutGroup("Grid Instantiation Value"), MinValue(0.1f)]
        public float cellSize;
        [FoldoutGroup("Grid Instantiation Value")]
        public float distanceBetweenCells = 0f;


        [ReadOnly]
        public MB_GenericCell[,] currentGrid;



        #region EDITOR
        #if UNITY_EDITOR
        public List<EDITOR_Abs_GridInstantiatorPass> EDITOR_extraPass = new List<EDITOR_Abs_GridInstantiatorPass>();
        #endif

        #if UNITY_EDITOR
        [Button("RESET TO DATA", ButtonSizes.Gigantic),
         DisableInPrefabAssets, DisableInPlayMode, ShowIf("gridDataReference"),
         GUIColor(0.7f, 1f, 0.7f, 1f), PropertySpace(20)]
        public void EDITOR_InstantiateGrid()
        {
            EDITOR_ImmediateDestroyChilds(cellFather);
            currentGrid = new MB_GenericCell[gridDataReference.grid.GetLength(0), gridDataReference.grid.GetLength(1)];
            if (gridDataReference.grid != null)
            {
                for (int x = 0; x < gridDataReference.grid.GetLength(0); x++)
                {
                    for (int y = 0; y < gridDataReference.grid.GetLength(1); y++)
                    {
                        if (gridDataReference.grid[x, y] != null)
                        {
                            EDITOR_InstantiateCell(x, y, gridDataReference.grid[x, y], "", true);
                        }
                    }
                }
                foreach (EDITOR_Abs_GridInstantiatorPass pass in EDITOR_extraPass)
                {
                    pass?.Pass(this);
                }
            }

            EditorUtility.SetDirty(this.gameObject);
            AssetDatabase.SaveAssets();
        }

        public void EDITOR_InstantiateCell(int x, int y, MB_GenericCell cellPrefab, string extraText = "", bool flipY = false)
        {
            if (flipY)
            {
                y = gridDataReference.grid.GetLength(1) - y - 1;
            }
            GameObject tmp = (GameObject)PrefabUtility.InstantiatePrefab(cellPrefab.gameObject);
            tmp.transform.parent = cellFather;
            tmp.transform.localPosition = new Vector3((cellSize * (x - gridDataReference.grid.GetLength(0) / 2)) + distanceBetweenCells * x,
                                                      0,
                                                      (cellSize * (y - gridDataReference.grid.GetLength(1) / 2)) + distanceBetweenCells * y);
            tmp.transform.localRotation = Quaternion.Euler(0, 0, 0);
            tmp.transform.localScale = Vector3.one * cellSize;
            MB_GenericCell tmpCell = tmp.GetComponent<MB_GenericCell>();
            tmpCell.coords.x = x;
            tmpCell.coords.y = y;
            tmpCell.grid = this;
            tmp.name = "Cell [ " + x + " , " + y + " ]" + (extraText != "" ? (" - " + extraText) 
                                                                           :  "");
            currentGrid[x, y] = tmpCell;
        }

        public void EDITOR_ImmediateDestroyChilds(Transform target)
        {
            while (target.transform.childCount > 0)
            {
                DestroyImmediate(target.transform.GetChild(0).gameObject);
            }
            EditorUtility.SetDirty(this.gameObject);
            AssetDatabase.SaveAssets();
        }
        #endif

        #endregion
    }
}