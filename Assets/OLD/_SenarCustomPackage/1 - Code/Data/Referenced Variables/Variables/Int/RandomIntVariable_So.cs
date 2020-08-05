namespace OLD_SenarCustomSystem.Variables.Data
{
    using OLD_SenarCustomSystem.Utility;
    using OLD_SenarCustomSystem.Variables.Abstract;
	using OLD_SenarCustomSystem.Variables.Interface;
	using Sirenix.OdinInspector;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	[CreateAssetMenu(menuName = "MyCustomSystem/Variables/Int/RandomIntVar",
					 fileName = "_NEW Random Int")]
	public class RandomIntVariable_So : AbsVariable_So<int>
	{
		[PropertyOrder(1)]
		public bool getNewRandomValueOnEveryRead = false;
		[PropertyOrder(2)]
		public SeedRandom randomParameter;

		public override int Value {
			get {
				return getNewRandomValueOnEveryRead ? GetNewRandomValue()
														: randomParameter.currentValue;
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

		public override bool CanSetValue { get => false; }

	}
}