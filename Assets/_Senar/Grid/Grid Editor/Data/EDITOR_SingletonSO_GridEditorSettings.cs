#if UNITY_EDITOR
namespace Senar.Grid.Editor
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Senar.Data;
    using Sirenix.OdinInspector;

	[CreateAssetMenu(fileName = "Grid Editor Settings", menuName = "Senar/Grids Editor/Singleton/Grid Editor Settings", order = 0)]
    public class EDITOR_SingletonSO_GridEditorSettings : SO_Abs_Singleton<EDITOR_SingletonSO_GridEditorSettings>
    {
        [Header("Paths"), Space(20)]
        public string gridPath = "Assets/Resources/Grids/";

        [Header("Grids Max Size"), Space(20)]
        [MinValue(1)] public int gridMaxWidth = 30;
        [MinValue(1)] public int gridMaxHeight = 30;
    }
}
#endif