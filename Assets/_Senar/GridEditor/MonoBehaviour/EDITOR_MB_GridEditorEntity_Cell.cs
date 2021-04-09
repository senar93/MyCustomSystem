namespace Senar.Grid.Editor
{
    using UnityEngine;
    using Sirenix.OdinInspector;
    using System.Collections.Generic;

    /// <summary>
    /// monobehaviour compilato solo in editor; 
    /// contiene tutte le informazioni per permettere al grid editor di riconoscere una cella, non va usato mai fuori dal grid editor
    /// </summary>
    [RequireComponent(typeof(MB_GenericCell))]
    public class EDITOR_MB_GridEditorEntity_Cell : MonoBehaviour
    {
        #if UNITY_EDITOR
        [SerializeField, DisableInPlayMode]
        private MB_GenericCell _cell;
        
        public string typeName;

        public EDITOR_SO_CellDrawMode drawMode;


        /// <summary>
        /// puntatore alla cella
        /// </summary>
        public MB_GenericCell cell
        {
            get 
            {
                return _cell != null ? _cell : GetComponent<MB_GenericCell>(); 
			}
		}



        public static EDITOR_MB_GridEditorEntity_Cell[] EDITOR_cellsType {
            get {
                return Resources.LoadAll<EDITOR_MB_GridEditorEntity_Cell>("");
            }
        }

        public void EDITOR_Draw(Rect rect)
        {
            foreach (EDITOR_Abs_CellDrawMode drawMode in drawMode.GridEditorDrawers)
            {
                drawMode.Draw(rect);
            }
        }

        #endif
    }

}
