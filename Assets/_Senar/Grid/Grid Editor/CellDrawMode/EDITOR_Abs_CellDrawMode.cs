#if UNITY_EDITOR
namespace Senar.Grid.Editor
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Sirenix.OdinInspector;

    public abstract class EDITOR_Abs_CellDrawMode
    {
        public abstract void Draw(Rect rect);

    }
}
#endif