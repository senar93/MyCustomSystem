#if UNITY_EDITOR
namespace Senar.Grid.Editor
{
	using Sirenix.OdinInspector;
	using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class EDITOR_Abs_GridInstantiatorPass
    {
        public abstract void Pass(MB_GenericGrid currentGrid);
    }
}
#endif