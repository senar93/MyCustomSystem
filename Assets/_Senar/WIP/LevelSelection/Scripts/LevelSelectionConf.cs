namespace Senar.WIP.Data
{
    using UnityEngine;
    using Sirenix.OdinInspector;
    using System.Linq;
	using Senar.Data;

	[CreateAssetMenu(fileName = "Level Selection Conf", menuName = "Custom/Settings/Level Selection Conf", order = 1)]
    public class LevelSelectionConf : Abs_SO_Singleton<LevelSelectionConf>
    {
        public bool autoAddAllLevelInAllWorldsOnBuild = true;
    }
}