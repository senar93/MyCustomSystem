#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public abstract class EDITOR_AbstractCellDrawMode
{
    public abstract void Draw(Rect rect);

}
#endif