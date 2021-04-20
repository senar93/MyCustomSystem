namespace Senar.Data.Variable
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	[CreateAssetMenu(fileName = "PlayerPrefs_Bool",
					 menuName = "Senar/Global Variables/PlayerPrefs/bool")]
	public class SO_Bool_VariablePlayerPrefs : SO_Abs_VariablePlayerPrefs<bool>
	{
		protected override bool GetValueFromPlayerPrefs()
		{
			return HasKey() ? PlayerPrefs.GetInt(key) > 0 : startingValue;
		}

		protected override void SetValueToPlayerPrefs(bool valueToSet)
		{
			PlayerPrefs.SetInt(key, _value ? 1 : 0);
		}
	}
}