using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Linq;

namespace MyCustomSystem.EntityBehaviour
{
	public abstract class AbsGenericEntity : SerializedMonoBehaviour
	{
		[SerializeField] bool setupOnAwake = true;
		public List<AbsCustomBehaviour> customBehaviourList;

		private bool setupFlag = false;



		protected virtual void Awake()
		{
			if (setupOnAwake)
				Setup();
		}

		public void Setup()
		{
			if (!setupFlag)
			{
				setupFlag = true;
				EntitySetup();
				PreBehaviourSetup();
				BehaviourSetup();
				AfterBehaviourSetup();
			}
		}

		protected virtual void EntitySetup() { }
		protected virtual void PreBehaviourSetup() { }
		protected virtual void AfterBehaviourSetup() { }

		private void BehaviourSetup()
		{
			customBehaviourList = GetComponentsInChildren<AbsCustomBehaviour>().ToList();
			customBehaviourList = customBehaviourList.OrderBy(x => x.loadOrder).ToList();
			foreach (AbsCustomBehaviour customBehaviour in customBehaviourList)
			{
				customBehaviour.Setup(this);
			}
		}


		public AbsCustomBehaviour GetBehaviour(System.Type typeToFind) 
		{
			return customBehaviourList.Find(x => x.GetType() == typeToFind);
		}



		public void AddBehaviour(AbsCustomBehaviour behaviour)
		{
			if (behaviour != null)
			{
				this.customBehaviourList.Add(behaviour);
				behaviour.Setup(this);
			}
		}

	}
}