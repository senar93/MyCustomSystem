namespace Senar.Data.Variable
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	[CreateAssetMenu(fileName = "PlayerPrefs_String",
					 menuName = "Senar/Global Variables/PlayerPrefs/string")]
	public class SO_String_PlayerPrefs : SO_Abs_Variable_PlayerPrefs<string>
	{
		protected override string GetValueFromPlayerPrefs()
		{
			return HasKey() ? PlayerPrefs.GetString(key) : startingValue;
		}

		protected override void SetValueToPlayerPrefs(string valueToSet)
		{
			PlayerPrefs.SetString(key, valueToSet);
		}
	}
}