namespace Senar.Grid.Editor
{
    #if UNITY_EDITOR
    using UnityEditor;
    #endif
    using Senar.Grid.Data;
	using Sirenix.OdinInspector;
    using System.Collections;
    using System.Collections.Generic;
	using UnityEngine;

    [RequireComponent(typeof(MB_Grid)), AddComponentMenu("Senar/Grid/_EDITOR ONLY/Grid Instantiator")]
    public class EDITOR_MB_GridInstantiator : MonoBehaviour
    {
        #if UNITY_EDITOR
        [DisableInPlayMode, DisableIf("_grid"), SerializeField]
        private MB_Grid _grid;

        [FoldoutGroup("References"), Required, SerializeField]
        private SO_SenarGrid _gridDataReference;
        [FoldoutGroup("References"), SerializeField]
        private Transform _cellFather;

        [FoldoutGroup("Instantiation Parameters", expanded:false), MinValue(0.1f), SerializeField]
        private float _cellSize = 1;
        [FoldoutGroup("Instantiation Parameters", expanded: false), SerializeField]
        private float _distanceBetweenCells = 0f;
        [FoldoutGroup("Instantiation Parameters", expanded: false), SerializeField]
        private bool _flipX = false;
        [FoldoutGroup("Instantiation Parameters", expanded: false), SerializeField]
        private bool _flipY = false;

        public UltEvents.UltEvent<MB_Grid> EDITOR_OnInstantiateInGrid = new UltEvents.UltEvent<MB_Grid>();

        [SerializeField]
        private List<EDITOR_Abs_GridInstantiatorPass> _extraPass = new List<EDITOR_Abs_GridInstantiatorPass>();



        public MB_Grid EDITOR_grid 
        {
            get 
            {
                return _grid != null ? _grid : GetComponent<MB_Grid>();
            }
        }

        public Transform EDITOR_cellFather
        {
            get 
            {
                return _cellFather != null ? _cellFather : this.transform;
			}
            set
            {
                _cellFather = value;
			}
		}



        public void EDITOR_SetGrid(MB_Grid grid)
        {
            _grid = grid;
        }


        [Button("RESET TO DATA", ButtonSizes.Gigantic),
         DisableInPrefabAssets, DisableInPlayMode, EnableIf("_gridDataReference"),
         GUIColor(0.7f, 1f, 0.7f, 1f), PropertySpace(20)]
        public void EDITOR_InstantiateGrid()
        {
            EDITOR_ImmediateDestroyChilds(EDITOR_cellFather);
            EDITOR_grid.currentGrid = new MB_Cell[_gridDataReference.grid.GetLength(0), _gridDataReference.grid.GetLength(1)];
            if (_gridDataReference.grid != null)
            {
                for (int x = 0; x < _gridDataReference.grid.GetLength(0); x++)
                {
                    for (int y = 0; y < _gridDataReference.grid.GetLength(1); y++)
                    {
                        if (_gridDataReference.grid[x, y] != null)
                        {
                            EDITOR_InstantiateCell(x, y, _gridDataReference.grid[x, y], "");
                        }
                    }
                }
                foreach (EDITOR_Abs_GridInstantiatorPass pass in _extraPass)
                {
                    pass?.Pass(this);
                }
            }
            EDITOR_OnInstantiateInGrid?.Invoke(_grid);

            EditorUtility.SetDirty(this.gameObject);
            AssetDatabase.SaveAssets();
        }

        public void EDITOR_InstantiateCell(int x, int y, MB_Cell cellPrefab, string extraText = "")
        {
            //normalmente la Y deve essere invertita, quindi flaggarla come invertita vuol dire non invertirla di fatto
            if (!_flipY)
            {
                y = _gridDataReference.grid.GetLength(1) - y - 1;
            }
            if (_flipX)
            {
                x = _gridDataReference.grid.GetLength(0) - x - 1;
            }
            GameObject tmp = (GameObject)PrefabUtility.InstantiatePrefab(cellPrefab.gameObject);
            tmp.transform.parent = EDITOR_cellFather;
            tmp.transform.localPosition = new Vector3((_cellSize * (x - _gridDataReference.grid.GetLength(0) / 2)) + _distanceBetweenCells * x,
                                                      0,
                                                      (_cellSize * (y - _gridDataReference.grid.GetLength(1) / 2)) + _distanceBetweenCells * y);
            tmp.transform.localRotation = Quaternion.Euler(0, 0, 0);
            tmp.transform.localScale = Vector3.one * _cellSize;
            MB_Cell tmpCell = tmp.GetComponent<MB_Cell>();
            tmpCell.coords.x = x;
            tmpCell.coords.y = y;
            tmpCell.grid = this.EDITOR_grid;
            tmp.name = "Cell [ " + x + " , " + y + " ]" + (extraText != "" ? (" - " + extraText)
                                                                           : "");
            EDITOR_grid.currentGrid[x, y] = tmpCell;
            EDITOR_grid.currentGrid[x, y]?.EDITOR_OnInstantiateInCell.Invoke(EDITOR_grid.currentGrid[x, y]);
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
    }
}