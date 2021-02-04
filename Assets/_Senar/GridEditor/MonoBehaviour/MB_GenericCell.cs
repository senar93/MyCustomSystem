namespace Senar.Grid
{
    using Sirenix.OdinInspector;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class MB_GenericCell : MonoBehaviour
    {
        [ReadOnly]
        public Vector2Int coords;
        [ReadOnly]
        public MB_GenericGrid grid;
    }
}