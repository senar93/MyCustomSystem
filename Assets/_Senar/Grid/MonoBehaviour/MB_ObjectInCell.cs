using Senar.Grid;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MB_ObjectInCell : MonoBehaviour
{
    public MB_Cell cell;


    [ReadOnly, ShowInInspector]
    public Vector2Int coords
    {
        get 
        {
            return cell != null ? cell.coords : Vector2Int.zero;
		}
	}
    [ReadOnly, ShowInInspector]
    public MB_Grid grid 
    {
        get {
            return cell != null ? cell.grid : null;
        }
    }

    #if UNITY_EDITOR
    public UltEvents.UltEvent<MB_Cell> OnInstantiateInCell = new UltEvents.UltEvent<MB_Cell>();
    #endif

    public void Init(MB_Cell currentCell)
	{
        cell = currentCell;
        #if UNITY_EDITOR
        OnInstantiateInCell?.Invoke(cell);
        #endif
    }

}
