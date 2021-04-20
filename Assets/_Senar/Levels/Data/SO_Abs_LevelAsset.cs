namespace Senar.Data.LevelSelection
{
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using Sirenix.OdinInspector;

    public abstract class SO_Abs_LevelAsset : SerializedScriptableObject
    {
        public string levelName;

        public abstract void LoadLevel();

    }
}