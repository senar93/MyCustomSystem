namespace Senar.Grid.Data
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Sirenix.OdinInspector;
    using System.Linq;
    using UnityEditor;

	/// <summary>
	/// scriptable object con dentro le informazioni sui tipi di celle da cui è composta la griglia;
	/// creato automaticamente dal grid editor
	/// </summary>
	public class SO_SenarGrid : SerializedScriptableObject
    {
        [Space(20)]
        public MB_GenericCell[,] grid;
    }
}