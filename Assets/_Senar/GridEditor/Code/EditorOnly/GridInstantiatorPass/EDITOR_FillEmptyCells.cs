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
        public MB_Cell emptyCell;

        public override void Pass(EDITOR_MB_GridInstantiator currentGridInstantiator)
        {
            for (int x = 0; x < currentGridInstantiator.EDITOR_grid.currentGrid.GetLength(0); x++)
            {
                for (int y = 0; y < currentGridInstantiator.EDITOR_grid.currentGrid.GetLength(1); y++)
                {
                    if (currentGridInstantiator.EDITOR_grid.currentGrid[x, y] == null)
                    {
                        currentGridInstantiator.EDITOR_InstantiateCell(x, y, emptyCell, "PassEmpty");
                    }
                }
            }
        }

        public MB_Cell[] ValueDropdown_PossibileCells()
        {
            List<MB_Cell> tmpList = new List<MB_Cell>();
            foreach (EDITOR_MB_GridEditorElement tmpCell in EDITOR_MB_GridEditorElement.EDITOR_allCellsType)
            {
                tmpList.Add(tmpCell.EDITOR_cell);
            }
            return tmpList.ToArray();
        }
        
    }
}
#endif