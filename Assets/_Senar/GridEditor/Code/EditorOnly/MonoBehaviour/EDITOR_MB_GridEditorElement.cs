namespace Senar.Grid.Editor
{
    using UnityEngine;
    using Sirenix.OdinInspector;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// monobehaviour compilato solo in editor; 
    /// contiene tutte le informazioni per permettere al grid editor di riconoscere una cella, non va usato mai fuori dal grid editor
    /// </summary>
    [RequireComponent(typeof(MB_Cell)), AddComponentMenu("Senar/Grid/_EDITOR ONLY/Grid Editor Element")]
    public class EDITOR_MB_GridEditorElement : MonoBehaviour
    {
        #if UNITY_EDITOR
        [SerializeField, DisableInPlayMode, DisableIf("_cell")]
        private MB_Cell _cell;
        
        public string EDITOR_typeName;

        public bool EDITOR_excludeFromGridEditor = false;

        public EDITOR_SO_CellDrawMode EDITOR_drawMode;


        /// <summary>
        /// puntatore alla cella
        /// </summary>
        public MB_Cell EDITOR_cell
        {
            get 
            {
                return _cell != null ? _cell : GetComponent<MB_Cell>(); 
			}
		}

        public void EDITOR_SetCell(MB_Cell cell)
        {
            _cell = cell;
        }


        public static EDITOR_MB_GridEditorElement[] EDITOR_allCellsType {
            get {
                return Resources.LoadAll<EDITOR_MB_GridEditorElement>("");
            }
        }

        public static EDITOR_MB_GridEditorElement[] EDITOR_loadableCellsType {
            get {
                List<EDITOR_MB_GridEditorElement> tmp = Resources.LoadAll<EDITOR_MB_GridEditorElement>("").ToList();
                tmp.RemoveAll(x => x.EDITOR_excludeFromGridEditor);
                return tmp.ToArray();
            }
        }

        public void EDITOR_Draw(Rect rect)
        {
            foreach (EDITOR_Abs_CellDrawMode drawMode in EDITOR_drawMode.GridEditorDrawers)
            {
                drawMode.Draw(rect);
            }
        }

        #endif
    }

}
