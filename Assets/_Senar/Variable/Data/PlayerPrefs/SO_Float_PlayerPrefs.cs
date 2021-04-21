namespace Senar.Data.Variable
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	[CreateAssetMenu(fileName = "PlayerPrefs_Float",
					 menuName = "Senar/Global Variables/PlayerPrefs/float")]
	public class SO_Float_PlayerPrefs : SO_Abs_Variable_PlayerPrefs<float>
	{
		protected override float GetValueFromPlayerPrefs()
		{
			return HasKey() ? PlayerPrefs.GetFloat(key) : startingValue;
		}

		protected override void SetValueToPlayerPrefs(float valueToSet)
		{
			PlayerPrefs.SetFloat(key, valueToSet);
		}
	}
}