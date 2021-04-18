namespace Senar.Data.Variable
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	[CreateAssetMenu(fileName = "PlayerPrefs_Int",
					 menuName = "Senar/Global Variables/PlayerPrefs/int")]
	public class SO_Int_VariablePlayerPrefs : SO_Abs_VariablePlayerPrefs<int>
	{
		protected override int GetValueFromPlayerPrefs()
		{
			return HasKey() ? PlayerPrefs.GetInt(key) : startingValue;
		}

		protected override void SetValueToPlayerPrefs(int valueToSet)
		{
			PlayerPrefs.SetInt(key, _value);
		}
	}
}