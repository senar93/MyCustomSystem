namespace Senar.Grid.Data
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Senar.Data;

    [CreateAssetMenu]
    public class SingletonSO_GridEditorSettings : Abs_SO_Singleton<SingletonSO_GridEditorSettings>
    {
        public string gridPath = "Assets/Resources/Grids/";
        //public int gridMaxWidth = 30;
        //public int gridMaxHeight = 30;
    }
}