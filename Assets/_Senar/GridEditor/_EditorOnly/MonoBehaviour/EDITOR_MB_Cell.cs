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
    [RequireComponent(typeof(MB_GenericCell))]
    public class EDITOR_MB_Cell : MonoBehaviour
    {
        #if UNITY_EDITOR
        [SerializeField, DisableInPlayMode, DisableIf("_cell")]
        private MB_GenericCell _cell;
        
        public string EDITOR_typeName;

        public bool EDITOR_excludeFromGridEditor = false;

        public EDITOR_SO_CellDrawMode EDITOR_drawMode;


        /// <summary>
        /// puntatore alla cella
        /// </summary>
        public MB_GenericCell EDITOR_cell
        {
            get 
            {
                return _cell != null ? _cell : GetComponent<MB_GenericCell>(); 
			}
		}



        public static EDITOR_MB_Cell[] EDITOR_allCellsType {
            get {
                return Resources.LoadAll<EDITOR_MB_Cell>("");
            }
        }

        public static EDITOR_MB_Cell[] EDITOR_loadableCellsType {
            get {
                List<EDITOR_MB_Cell> tmp = Resources.LoadAll<EDITOR_MB_Cell>("").ToList();
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
