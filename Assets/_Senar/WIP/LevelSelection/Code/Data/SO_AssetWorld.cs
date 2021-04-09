namespace Senar.WIP.Data.LevelSelection
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Sirenix.OdinInspector;
    using UnityEditor;

    [CreateAssetMenu(fileName = "World 1", menuName = "Custom/Level Selection/World", order = 1)]
    public class SO_AssetWorld : SerializedScriptableObject
    {
        public string worldName;

        [Space(20), AssetSelector, InlineEditor]
        public List<SO_AssetLevel> levels;


        #if UNITY_EDITOR
            [ShowIf("EDITOR_CheckWrongLevelsWorldReference"),
             Button("Set this World reference to all levels in list", ButtonSizes.Large), GUIColor(1f, 1f, 0.7f)]
            public void EDITOR_SetWorldReferenceToAllLevelsInList()
            {
                EDITOR_RemoveNullElements();
                foreach (SO_AssetLevel level in levels)
                {
                    level.world = this;
                }
                EditorUtility.SetDirty(this);
                AssetDatabase.SaveAssets();
            }

            [ShowIf("EDITOR_CheckNullElementsInLevels"),
             Button("Remove all NULL value in levels", ButtonSizes.Large), GUIColor(1f, 0.7f, 0.7f)]
            public void EDITOR_RemoveNullElements()
            {
                for (int i = 0; i < levels.Count; i++)
                {
                    if (levels[i] == null)
                    {
                        levels.RemoveAt(i);
                        i--;
                    }
                }
                EditorUtility.SetDirty(this);
                AssetDatabase.SaveAssets();
            }

            private bool EDITOR_CheckWrongLevelsWorldReference()
            {
                foreach (SO_AssetLevel level in levels)
                {
                    if (level != null && level.world != this)
                    {
                        return true;
                    }
                }
                return false;
            }

            private bool EDITOR_CheckNullElementsInLevels()
            {
                foreach (SO_AssetLevel level in levels)
                {
                    if (level == null)
                    {
                        return true;
                    }
                }
                return false;
            }
        #endif

    }

}