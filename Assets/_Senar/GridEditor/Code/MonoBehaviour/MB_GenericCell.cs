namespace Senar.Grid
{
    using Sirenix.OdinInspector;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    #if UNITY_EDITOR
    using Senar.Grid.Editor;
    #endif

    [RequireComponent(typeof(EDITOR_MB_Cell))]
    public class MB_GenericCell : MonoBehaviour
    {
        [ReadOnly]
        public Vector2Int coords;
        [ReadOnly]
        public MB_GenericGrid grid;
    }
}