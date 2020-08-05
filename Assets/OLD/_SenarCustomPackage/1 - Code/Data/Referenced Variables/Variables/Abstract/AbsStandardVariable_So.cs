namespace OLD_SenarCustomSystem.Variables.Abstract
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.Events;
	using System;
	using Sirenix.OdinInspector;


	public abstract class AbsStandardVariable_So<T> : AbsVariable_So<T>, ISerializationCallbackReceiver
	{
		[SerializeField, PropertyOrder(int.MinValue)]
		public T startingValue { get; protected set; }
		[SerializeField, HideInEditorMode, Space]
		private T currentValue;
		[SerializeField]
		private bool isReadOnly = false;

		[HideInInspector] public Action<T> onValueChange;

		public override T Value {
			get => currentValue;
			set {
				if (!isReadOnly)
				{
					currentValue = value;
					onValueChange?.Invoke(value);
				}
			}
		}

		public override bool CanSetValue { get => !isReadOnly; }

		protected override void OnAfterDeserialize()
		{
			RemoveAllValueChangeListner();
			currentValue = startingValue;
		}

		public void ResetValue()
		{
			Value = startingValue;
		}

		public void RemoveAllValueChangeListner()
		{
			onValueChange = null;
		}

		public void ChangeStartingValue_EditorOnly(T newValue)
		{
			if (!Application.isPlaying)
				startingValue = newValue;
		}

	}
}