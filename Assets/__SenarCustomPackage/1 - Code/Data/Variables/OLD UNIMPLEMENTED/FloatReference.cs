/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyCustomSystem.Data;

[System.Serializable]
public class FloatReference
{
	public bool useReference = true;
	[SerializeField] private float _value;
	[SerializeField] private AbsFloatVariable_So _valueRef;

	public float value
	{
		get 
		{
			return useReference ? _valueRef.value : _value;
		}
		set 
		{
			if(useReference)
			{
				_valueRef.value = value;
			}
			else
			{
				_value = value;
			}
		}
	}

}*/