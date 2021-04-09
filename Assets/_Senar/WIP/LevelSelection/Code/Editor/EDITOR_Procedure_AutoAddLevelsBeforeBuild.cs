namespace Senar.WIP.Data.LevelSelection.Editor
{
#if UNITY_EDITOR
    using UnityEditor;
    using UnityEditor.Build;
    using UnityEditor.Build.Reporting;
    using UnityEngine;

    class EDITOR_Procedure_AutoAddLevelsBeforeBuild : IPreprocessBuildWithReport
    {
        public int callbackOrder { get { return 0; } }
        public void OnPreprocessBuild(BuildReport report)
        {
            if ( SingletonSO_LevelSelectionConf.Instance != null && 
                 SingletonSO_LevelSelectionConf.Instance.autoAddAllLevelInAllWorldsOnBuild )
            {
                Debug.Log("Auto set all levels scene: Start");
                EDITOR_Window_LevelUtility.SetEditorBuildSettingsScenes();
                Debug.Log("Auto set all levels scene: Done");
            }
        }
    }

#endif
}
