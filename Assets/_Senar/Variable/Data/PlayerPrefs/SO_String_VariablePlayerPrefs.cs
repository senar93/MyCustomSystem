namespace Senar.Data.Variable
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	[CreateAssetMenu(fileName = "PlayerPrefs_String",
					 menuName = "Senar/Global Variables/PlayerPrefs/string")]
	public class SO_String_VariablePlayerPrefs : SO_Abs_VariablePlayerPrefs<string>
	{
		protected override string GetValueFromPlayerPrefs()
		{
			return HasKey() ? PlayerPrefs.GetString(key) : startingValue;
		}

		protected override void SetValueToPlayerPrefs(string valueToSet)
		{
			PlayerPrefs.SetString(key, _value);
		}
	}
}