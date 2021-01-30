using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEditor;

public class CustomGrid : MonoBehaviour
{
    [Required]
    public CustomGridSO gridDataReference;

    [MinValue(0.1f)] 
    public float cellSize;
    public float distanceBetweenCells = 0f;

    [Header("Ref"), Required]
    public Transform cellFather;
    [Required]
    public CustomCell emptyCell;
    [Required]
    public CustomCell invisibleWall;

    #if UNITY_EDITOR
    [Button("RESET TO DATA", ButtonSizes.Gigantic), GUIColor(0.7f, 1f, 0.7f, 1f), PropertySpace(20), ShowIf("gridDataReference")]
    public void InstantiateGrid()
    {
        ImmediateDestroyChilds(cellFather);
        if (gridDataReference.grid != null)
        {
            for (int x = 0; x < gridDataReference.grid.GetLength(0); x++)
            {
                for (int y = 0; y < gridDataReference.grid.GetLength(1); y++)
                {
                    InstantiateCell(x, y, gridDataReference.grid[x,y] ? gridDataReference.grid[x, y] : emptyCell, true);
                }
            }
        }

        for(int i = -1; i < gridDataReference.grid.GetLength(0) + 1; i++)
        {
            InstantiateCell(i, -1, invisibleWall, false);
		}
        for (int i = -1; i < gridDataReference.grid.GetLength(0) + 1; i++)
        {
            InstantiateCell(i, gridDataReference.grid.GetLength(1), invisibleWall, false);
        }
        for (int i = 0; i < gridDataReference.grid.GetLength(1); i++)
        {
            InstantiateCell(-1, i, invisibleWall, false);
        }
        for (int i = 0; i < gridDataReference.grid.GetLength(1); i++)
        {
            InstantiateCell(gridDataReference.grid.GetLength(0), i, invisibleWall, false);
        }

    }


    private void InstantiateCell(int x, int y, CustomCell cellPrefab, bool flipY = false)
    {
        if(flipY)
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
        CustomCell tmpCell = tmp.GetComponent<CustomCell>();
        tmpCell.coords.x = x;
        tmpCell.coords.y = y;
        tmpCell.grid = this;
        tmp.name = "Cell [ " + x + " , " + y + " ]";
    }


    private void ImmediateDestroyChilds(Transform target)
    {
        while (target.transform.childCount > 0)
        {
            DestroyImmediate(target.transform.GetChild(0).gameObject);
        }
    }
    #endif
}
