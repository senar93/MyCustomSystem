namespace Senar.Data.Variable
{
	using Senar.Utility;
	using Sirenix.OdinInspector;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	[CreateAssetMenu(fileName = "Random_Int",
					 menuName = "Senar/Global Variables/Random/int")]
	public class SO_Int_VariableRandom : SO_Abs_Variable<int>, ISerializationCallbackReceiver
	{
		[SerializeField, PropertyOrder(int.MinValue)]
		public SeedRandom randomParameter;

		[SerializeField, FoldoutGroup("Random Settings", 0)]
		protected bool getNewRandomValueOnEveryRead = false;

		public override int Value {
			get 
			{
				return getNewRandomValueOnEveryRead ? GetNewRandomValue() : randomParameter.currentValue;
			}
			set { }
		}

		protected override void OnAfterDeserialize()
		{
			Reset();
		}

		public void Reset()
		{
			if (randomParameter != null)
			{
				randomParameter = new SeedRandom(randomParameter.seed, randomParameter.min, randomParameter.max);
			}
		}

		public int GetNewRandomValue()
		{
			return randomParameter.GetNextValue();
		}
	}
}