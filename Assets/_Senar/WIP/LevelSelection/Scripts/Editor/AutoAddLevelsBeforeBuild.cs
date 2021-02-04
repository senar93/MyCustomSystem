namespace Senar.WIP.Data.LevelSelection.Editor
{
#if UNITY_EDITOR
    using UnityEditor;
    using UnityEditor.Build;
    using UnityEditor.Build.Reporting;
    using UnityEngine;

    class AutoAddLevelsBeforeBuild : IPreprocessBuildWithReport
    {
        public int callbackOrder { get { return 0; } }
        public void OnPreprocessBuild(BuildReport report)
        {
            if ( LevelSelectionConf.Instance != null && 
                 LevelSelectionConf.Instance.autoAddAllLevelInAllWorldsOnBuild )
            {
                Debug.Log("Auto set all levels scene: Start");
                LevelUtility.SetEditorBuildSettingsScenes();
                Debug.Log("Auto set all levels scene: Done");
            }
        }
    }

#endif
}
