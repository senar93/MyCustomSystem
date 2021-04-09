#if UNITY_EDITOR
namespace Senar.Grid.Editor
{
    using Sirenix.OdinInspector;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu(fileName = "CellDrawMode_", menuName = "Senar/Grid Editor/Cell Draw Mode", order = 1)]
    public class EDITOR_SO_CellDrawMode : SerializedScriptableObject
    {
        [Required]
        public List<EDITOR_Abs_CellDrawMode> GridEditorDrawers = new List<EDITOR_Abs_CellDrawMode>();
    }
}
#endif