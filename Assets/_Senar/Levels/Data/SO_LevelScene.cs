namespace Senar.Data.LevelSelection
{
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using Sirenix.OdinInspector;
    #if UNITY_EDITOR
    using UnityEditor;
	using System.Collections.Generic;
	using System.Linq;
#endif


	[CreateAssetMenu(fileName = "Level_Scene", menuName = "Senar/Level Selection/Level (Scene)", order = 0)]
    public class SO_LevelScene : SO_Abs_LevelAsset
    {
        #if UNITY_EDITOR
        [AssetSelector, SerializeField, OnValueChanged("EDITOR_SetSceneNameAndPath"), Required("It cannot be NULL!\nMust have an associated scene containing the Level")]
        protected SceneAsset EDITOR_scene;
        #endif

        /// <summary>
        /// Use sceneCompletePath instead to load the correct scene
        /// </summary>
        [ShowInInspector, ReadOnly]
        public string sceneName { get; protected set; }
        /// <summary>
        /// It is the safest way of getting the correct scene
        /// </summary>
        [ShowInInspector, ReadOnly]
        public string sceneCompletePath { get; protected set; }


        public override void LoadLevel()
        {
            SceneManager.LoadScene(this.sceneCompletePath, LoadSceneMode.Single);
        }

        public virtual void LoadLevel(LoadSceneMode loadSceneMode)
        {
            SceneManager.LoadScene(this.sceneCompletePath, loadSceneMode);
        }


        #if UNITY_EDITOR
        [ShowIf("EDITOR_CheckShowSetSceneDataButton"), Button("Set Scene Data", ButtonSizes.Gigantic), GUIColor(0.7f, 1f, 0.7f)]
        private void EDITOR_SetSceneNameAndPath()
        {
            sceneName = EDITOR_scene == null ? "" : EDITOR_scene.name;
            sceneCompletePath = EDITOR_scene == null ? "" : AssetDatabase.GetAssetPath(EDITOR_scene);
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
        }

        private bool EDITOR_CheckShowSetSceneDataButton()
        {
            if (EDITOR_scene == null)
            {
                sceneName = "";
                sceneCompletePath = "";
                return false;
            }

            return sceneName != EDITOR_scene.name || sceneCompletePath != AssetDatabase.GetAssetPath(EDITOR_scene);
        }


        [ShowIf("EDITOR_CheckShowAddSceneToBuild"), Button("Set Scene Data", ButtonSizes.Gigantic), GUIColor(0.7f, 1f, 0.7f)]
        public void EDITOR_AddSceneToBuildButton()
        {
            List<EditorBuildSettingsScene> tmpSceneList = EditorBuildSettings.scenes.ToList();

            if (EDITOR_scene)
            {
                EDITOR_SetSceneNameAndPath();
                if (tmpSceneList.Find(x => x.path == sceneCompletePath) == null)
                {
                    tmpSceneList.Add(new EditorBuildSettingsScene(sceneCompletePath, true));
                }
            }

            EditorBuildSettings.scenes = tmpSceneList.ToArray();
            AssetDatabase.SaveAssets();
        }

        private bool EDITOR_CheckShowAddSceneToBuild()
        {
            return EDITOR_scene == null || 
                   EditorBuildSettings.scenes.ToList().Find(x => x.path == sceneCompletePath) == null;
        }
        #endif

    }


}
