using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System.Linq;
using Sirenix.Utilities;
using UnityEditor;

[ShowOdinSerializedPropertiesInInspector]
public class EDITOR_GridEditorTab
{
    [ShowIf("targetGridAsset"), ReadOnly]
    public CustomGridSO targetGridAsset;
    public string gridName = "NewGrid";

    [FoldoutGroup("Grid Size"), Range(1, 30)] 
    public int width = 1;
    [FoldoutGroup("Grid Size"), Range(1, 30)]
    public int height = 1;

    [FoldoutGroup("Fast Draw"), OnInspectorGUI("OnChange_FastDrawCell"), ValueDropdown("ValueDropdown_FastDrawCell"), SerializeField, AssetsOnly]
    private CustomCell _fastDrawCell;
    public static CustomCell fastDrawCell = null;

    [Space(30), TableMatrix(HorizontalTitle = "Grid", DrawElementMethod = "EDITOR_DrawGrid", ResizableColumns = false, SquareCells = false, RowHeight = 20), ShowIf("ShowIf_Grid")]
    public CustomCell[,] grid = new CustomCell[0, 0];


    private static CustomCell EDITOR_DrawGrid(Rect rect, CustomCell value)
    {
        List<CustomCell> tmpCellsType = CustomCell.cellsType.ToList();

        if (Event.current.type == EventType.MouseDown && rect.Contains(Event.current.mousePosition))
        {
            int index = tmpCellsType.FindIndex(x => x == value);
            if(index < 0)
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
        else if(Event.current.Equals(Event.KeyboardEvent("^X"))  && rect.Contains(Event.current.mousePosition))
        {
            value = null;
            GUI.changed = true;
            Event.current.Use();
        }
        else if(Event.current.Equals(Event.KeyboardEvent("&X")) && rect.Contains(Event.current.mousePosition))
        {
            value = fastDrawCell;
            GUI.changed = true;
            Event.current.Use();
        }

        if (value == null)
        {
            UnityEditor.EditorGUI.DrawRect(rect.Padding(1f), new Color(0, 0, 0, 0.5f));
        }
        else
        {
            value.drawMode.Draw(rect);
		}
        
        return value;
    }



    public void LoadGridAsset(CustomGridSO target)
    {
        targetGridAsset = target;
        gridName = targetGridAsset.name;
        width = targetGridAsset.grid != null ? targetGridAsset.grid.GetLength(0) : 0;
        height = targetGridAsset.grid != null ? targetGridAsset.grid.GetLength(1) : 0;

        grid = new CustomCell[width, height];
        if(targetGridAsset.grid != null)
        {
            System.Array.Copy(targetGridAsset.grid, grid, targetGridAsset.grid.Length);
        }
    }


    #region BUTTONS
    [FoldoutGroup("Grid Size"), Button("Resize Grid", ButtonSizes.Gigantic), GUIColor(0.7f, 1f, 0.7f, 1f), PropertySpace(20)]
    public void ResizeGrid()
    {
        CustomCell[,] tmpGrid = new CustomCell[width, height];

        for(int x = 0; x < tmpGrid.GetLength(0); x++)
        {
            for(int y = 0; y < tmpGrid.GetLength(1); y++)
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
        if(targetGridAsset == null)
        {
            targetGridAsset = ScriptableObject.CreateInstance<CustomGridSO>();
            if(Resources.LoadAll<CustomGridSO>("").ToList().Find(x => x.name == gridName) == null)
            {
                AssetDatabase.CreateAsset(targetGridAsset, "Assets/_GGJ 2021/ScriptableObjects/Resources/Grids/" + gridName + ".asset");
            }
            else
            {
                AssetDatabase.CreateAsset(targetGridAsset, "Assets/_GGJ 2021/ScriptableObjects/Resources/Grids/" + "__" + Random.Range(0, int.MaxValue) + "__" + gridName + ".asset");
            }
        }
        targetGridAsset.InitGrid(width, height);
        System.Array.Copy(grid, targetGridAsset.grid, grid.Length);
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
        fastDrawCell = _fastDrawCell;
	}

	#endregion

	#region VALUE DROPDOWN
    public CustomCell[] ValueDropdown_FastDrawCell()
    {
        return CustomCell.cellsType;
	}

	#endregion

}