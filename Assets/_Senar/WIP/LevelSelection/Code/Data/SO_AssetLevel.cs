namespace Senar.WIP.Data.LevelSelection
{
    using Sirenix.OdinInspector;
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    [CreateAssetMenu(fileName = "Level 1", menuName = "Custom/Level Selection/Level", order = 2)]
    public class SO_AssetLevel : SerializedScriptableObject
    {
        public string levelName;

        /// <summary>
        /// Use sceneCompletePath instead to load the correct scene
        /// </summary>
        public string sceneName { get => _sceneName; }
        [ReadOnly, SerializeField]
        private string _sceneName;
        /// <summary>
        /// It is the safest way of getting the correct scene
        /// </summary>
        public string sceneCompletePath { get => _sceneCompletePath; }
        [ReadOnly, SerializeField]
        private string _sceneCompletePath;

        [AssetSelector, Required("It cannot be NULL!\nBut it can be set automatically by the world")]
        public SO_AssetWorld world;


        #if UNITY_EDITOR
            [AssetSelector, Required("It cannot be NULL!\nMust have an associated scene containing the Level")]
            public SceneAsset EDITOR_scene;
        #endif



        public void LoadLevel(LoadSceneMode loadSceneMode = LoadSceneMode.Single)
        {
            SceneManager.LoadScene(this.sceneCompletePath, loadSceneMode);
        }


        #if UNITY_EDITOR
            [ShowIf("EDITOR_CheckShowSetSceneNameButton"), Button("Set scene Name", ButtonSizes.Gigantic), GUIColor(0.7f, 1f, 0.7f)]
            public void EDITOR_SetSceneNameAndPath()
            {
                _sceneName = EDITOR_scene == null ? "" : EDITOR_scene.name;
                _sceneCompletePath = EDITOR_scene == null ? "" : AssetDatabase.GetAssetPath(EDITOR_scene);
                EditorUtility.SetDirty(this);
                AssetDatabase.SaveAssets();
            }

            private bool EDITOR_CheckShowSetSceneNameButton()
            {
                if (EDITOR_scene == null)
                {
                    _sceneName = "";
                    _sceneCompletePath = "";
                    return false;
                }

                return sceneName != EDITOR_scene.name || sceneCompletePath != AssetDatabase.GetAssetPath(EDITOR_scene);
            }
        #endif

    }


}