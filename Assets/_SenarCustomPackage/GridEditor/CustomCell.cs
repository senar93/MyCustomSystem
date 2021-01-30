using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class CustomCell : SerializedMonoBehaviour
{
    public string typeName;

    [ReadOnly] public Vector2Int coords;
    [ReadOnly] public CustomGrid grid;

    #if UNITY_EDITOR
    [Header("EDITOR ONLY"), Required ]
    public EDITOR_AbstractCellDrawMode drawMode;
    #endif



    public static CustomCell[] cellsType 
    {
        get 
        {
            return Resources.LoadAll<CustomCell>("");
        }
    }




}
