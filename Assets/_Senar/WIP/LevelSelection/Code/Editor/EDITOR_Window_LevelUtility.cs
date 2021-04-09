namespace Senar.WIP.Data.LevelSelection.Editor
{
#if UNITY_EDITOR
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEngine;
    using Sirenix.OdinInspector;
    using System.Linq;

    public class EDITOR_Window_LevelUtility : EditorWindow
    {
        //TODO:
        //aggiungere un file di configurazione scriptable singleton, che contenga le configurazioni di questo sistema e si ricrei se viene eliminato


        // Add menu item named "Example Window" to the Window menu
        [MenuItem("Custom Tools/Level Utility")]
        public static void ShowWindow()
        {
            //Show existing window instance. If one doesn't exist, make one.
            EditorWindow.GetWindow(typeof(EDITOR_Window_LevelUtility));
        }



        void OnGUI()
        {
            var style = new GUIStyle(GUI.skin.button);
            style.normal.textColor = new Color(0.7f, 1f, 0.7f, 1f);
            style.fontSize = 30;
            if (GUILayout.Button("Add all levels\nto Build", style, GUILayout.MinHeight(40), GUILayout.MaxHeight(200)))
            {
                SetEditorBuildSettingsScenes();
            }

            if ( SingletonSO_LevelSelectionConf.Instance.autoAddAllLevelInAllWorldsOnBuild == false && 
                 GUILayout.Button("ENABLE: Auto Add Level On Build"))
            {
                SingletonSO_LevelSelectionConf.Instance.autoAddAllLevelInAllWorldsOnBuild = true;
                Debug.Log("Auto set levels scene on build " + (SingletonSO_LevelSelectionConf.Instance.autoAddAllLevelInAllWorldsOnBuild ? "ENABLED" : "DISABLED"));
            }
            else if ( SingletonSO_LevelSelectionConf.Instance.autoAddAllLevelInAllWorldsOnBuild == true &&
                      GUILayout.Button("DISABLE: Auto Add Level On Build"))
            {
                SingletonSO_LevelSelectionConf.Instance.autoAddAllLevelInAllWorldsOnBuild = false;
                Debug.Log("Auto set levels scene on build " + (SingletonSO_LevelSelectionConf.Instance.autoAddAllLevelInAllWorldsOnBuild ? "ENABLED" : "DISABLED"));
            }
            /*if (GUILayout.Button("TEST"))
            {
                LevelSelectionConf a = ScriptableObject.CreateInstance(typeof(LevelSelectionConf)) as LevelSelectionConf;
                AssetDatabase.CreateAsset(a, "Assets/SingletonAsset_CreatedBySystem.asset");
                Debug.Log(a.name);
            }*/
        }


        public static void SetEditorBuildSettingsScenes()
        {
            string[] guids = AssetDatabase.FindAssets("t:AssetWorld");
            List<EditorBuildSettingsScene> tmpSceneList = EditorBuildSettings.scenes.ToList();

            for (int j = 0; j < guids.Length; j++)
            {
                string tmpWorldPath = AssetDatabase.GUIDToAssetPath(guids[j]);
                SO_AssetWorld tmpWorld = AssetDatabase.LoadAssetAtPath(tmpWorldPath, typeof(SO_AssetWorld)) as SO_AssetWorld;
                tmpWorld.EDITOR_RemoveNullElements();

                for (int i = 0; i < tmpWorld.levels.Count; i++)
                {
                    SO_AssetLevel tmpLevel = tmpWorld.levels[i];
                    if (tmpLevel.EDITOR_scene)
                    {
                        tmpLevel.EDITOR_SetSceneNameAndPath();
                        string tmpScenePath = AssetDatabase.GetAssetPath(tmpLevel.EDITOR_scene);
                        if (tmpSceneList.Find(x => x.path == tmpScenePath) == null)
                        {
                            tmpSceneList.Add(new EditorBuildSettingsScene(tmpScenePath, true));
                        }
                    }
                }
            }

            EditorBuildSettings.scenes = tmpSceneList.ToArray();
            AssetDatabase.SaveAssets();
        }


    }

#endif
}
