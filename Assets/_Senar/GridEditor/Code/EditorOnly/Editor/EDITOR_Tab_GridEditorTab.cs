#if UNITY_EDITOR
namespace Senar.Grid.Editor
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Sirenix.OdinInspector;
    using Sirenix.OdinInspector.Editor;
    using System.Linq;
    using Sirenix.Utilities;
    using UnityEditor;
    using Sirenix.Serialization;
    using Senar.Grid.Data;

    [ShowOdinSerializedPropertiesInInspector]
    public class EDITOR_Tab_GridEditorTab
    {
        [ShowIf("targetGridAsset"), ReadOnly]
        public SO_SenarGrid targetGridAsset;
        public string gridName = "NewGrid";

        [FoldoutGroup("Grid Size"), PropertyRange(1, 30)]
        public int width = 1;
        [FoldoutGroup("Grid Size"), PropertyRange(1, 30)]
        public int height = 1;

        [FoldoutGroup("Cell Painter"), OnInspectorGUI("OnChange_FastDrawCell"), ValueDropdown("ValueDropdown_FastDrawCell"), SerializeField, AssetsOnly]
        private EDITOR_MB_Cell _cellPainterCell;
        protected static EDITOR_MB_Cell cellPainterCell = null;

        [Space(30), TableMatrix(HorizontalTitle = "Grid", DrawElementMethod = "EDITOR_DrawGrid", ResizableColumns = false, SquareCells = false, RowHeight = 20), ShowIf("ShowIf_Grid"), OdinSerialize]
        public EDITOR_MB_Cell[,] grid = new EDITOR_MB_Cell[0, 0];


        private static EDITOR_MB_Cell EDITOR_DrawGrid(Rect rect, EDITOR_MB_Cell value)
        {
            List<EDITOR_MB_Cell> tmpCellsType = EDITOR_MB_Cell.EDITOR_loadableCellsType.ToList();

            if (Event.current.type == EventType.MouseDown && rect.Contains(Event.current.mousePosition))
            {
                int index = tmpCellsType.FindIndex(x => x == value);
                if (index < 0)
                {
                    index = 0;
                }
                if (index >= 0)
                {
                    index = index < tmpCellsType.Count - 1 ? index + 1 : 0;
                    value = tmpCellsType[index];
                    GUI.changed = true;
                    Event.current.Use();
                }
            }
            else if (Event.current.Equals(Event.KeyboardEvent("^X")) && rect.Contains(Event.current.mousePosition))
            {
                value = null;
                GUI.changed = true;
                Event.current.Use();
            }
            else if (Event.current.Equals(Event.KeyboardEvent("#X")) && rect.Contains(Event.current.mousePosition))
            {
                value = cellPainterCell;
                GUI.changed = true;
                Event.current.Use();
            }

            if (value == null)
            {
                UnityEditor.EditorGUI.DrawRect(rect.Padding(1f), new Color(0, 0, 0, 0.5f));
            }
            else
            {
                value.EDITOR_Draw(rect);
            }

            return value;
        }



        public void LoadGridAsset(SO_SenarGrid target)
        {
            targetGridAsset = target;
            gridName = targetGridAsset.name;
            width = targetGridAsset.grid != null ? targetGridAsset.grid.GetLength(0) : 0;
            height = targetGridAsset.grid != null ? targetGridAsset.grid.GetLength(1) : 0;

            grid = new EDITOR_MB_Cell[width, height];
            if (targetGridAsset.grid != null)
            {
                for (int x = 0; x < height; x++)
                {
                    for (int y = 0; y < width; y++)
                    {
                        grid[y, x] = targetGridAsset.grid[y, x]?.GetComponent<EDITOR_MB_Cell>();
                    }
                }
            }
        }


        #region BUTTONS
        [FoldoutGroup("Grid Size"), Button("Resize Grid", ButtonSizes.Medium), GUIColor(0.7f, 1f, 0.7f, 1f), PropertySpace(20)]
        public void ResizeGrid()
        {
            EDITOR_MB_Cell[,] tmpGrid = new EDITOR_MB_Cell[width, height];

            for (int x = 0; x < tmpGrid.GetLength(0); x++)
            {
                for (int y = 0; y < tmpGrid.GetLength(1); y++)
                {
                    tmpGrid[x, y] = (x < grid.GetLength(0) && y < grid.GetLength(1)) ? grid[x, y] : null;
                }
            }

            grid = tmpGrid;
        }

        [FoldoutGroup("Grid Size"), Button("Clear Grid", ButtonSizes.Medium), GUIColor(1f, 0.7f, 0.7f, 1f), PropertySpace(20)]
        public void ClearGrid()
        {
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    grid[x, y] = null;
                }
            }
        }

        [Button("SAVE", ButtonSizes.Gigantic), GUIColor(0.7f, 1f, 0.7f, 1f), PropertySpace(20)]
        public void Save()
        {
            ResizeGrid();
            if (targetGridAsset == null)
            {
                targetGridAsset = ScriptableObject.CreateInstance<SO_SenarGrid>();
                if (Resources.LoadAll<SO_SenarGrid>("").ToList().Find(x => x.name == gridName) == null)
                {
                    AssetDatabase.CreateAsset(targetGridAsset, SingletonSO_GridEditorSettings.Instance.gridPath + gridName + ".asset");
                }
                else
                {
                    AssetDatabase.CreateAsset(targetGridAsset, SingletonSO_GridEditorSettings.Instance.gridPath + "__" + Random.Range(0, int.MaxValue) + "__" + gridName + ".asset");
                }
            }

            targetGridAsset.grid = new MB_GenericCell[width, height];
            for(int x = 0; x < height; x++)
            {
                for(int y = 0; y < width; y++)
                {
                    targetGridAsset.grid[y, x] = grid[y, x]?.EDITOR_cell;
				}
			}
            //System.Array.Copy(grid, targetGridAsset.grid, grid.Length);
            EditorUtility.SetDirty(targetGridAsset);
            AssetDatabase.SaveAssets();
        }
        #endregion

        #region SHOW IF
        public bool ShowIf_Grid()
        {
            return grid != null && grid.Length > 0;
        }

        #endregion

        #region OnChange
        public void OnChange_FastDrawCell()
        {
            cellPainterCell = _cellPainterCell;
        }

        #endregion

        #region VALUE DROPDOWN
        public EDITOR_MB_Cell[] ValueDropdown_FastDrawCell()
        {
            return EDITOR_MB_Cell.EDITOR_allCellsType;
        }

        #endregion

    }
}
#endif