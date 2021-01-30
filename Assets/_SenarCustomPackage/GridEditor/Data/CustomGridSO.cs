using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Linq;

public class CustomGridSO : SerializedScriptableObject
{
    [Space(20), ReadOnly]
    public CustomCell[,] grid;


    



    public void InitGrid(int width, int height)
    {
        grid = new CustomCell[width, height];
	}


}
