#if UNITY_EDITOR
namespace Senar.Grid.Editor
{
	using Sirenix.OdinInspector;
	using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class EDITOR_FillEmptyCells : EDITOR_Abs_GridInstantiatorPass
    {
        [Required, ValueDropdown("ValueDropdown_PossibileCells")]
        public MB_GenericCell emptyCell;

        public override void Pass(MB_GenericGrid currentGrid)
        {
            for (int x = 0; x < currentGrid.currentGrid.GetLength(0); x++)
            {
                for (int y = 0; y < currentGrid.currentGrid.GetLength(1); y++)
                {
                    if (currentGrid.currentGrid[x, y] == null)
                    {
                        currentGrid.EDITOR_InstantiateCell(x, y, emptyCell, "PassEmpty", true);
                    }
                }
            }
        }

        public MB_GenericCell[] ValueDropdown_PossibileCells()
        {
            List<MB_GenericCell> tmpList = new List<MB_GenericCell>();
            foreach (EDITOR_MB_Cell tmpCell in EDITOR_MB_Cell.EDITOR_allCellsType)
            {
                tmpList.Add(tmpCell.EDITOR_cell);
            }
            return tmpList.ToArray();
        }
        
    }
}
#endif