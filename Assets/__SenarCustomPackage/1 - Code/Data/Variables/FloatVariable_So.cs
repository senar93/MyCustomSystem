using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "MyCustomSystem/Variables/Float/FloatVar",
                 fileName = "_NEW Float")]
public class FloatVariable_So : AbsFloatVariable_So, ISerializationCallbackReceiver
{
    [ShowInInspector, PropertyOrder(int.MinValue)] public float startingValue { get; protected set; }
    [SerializeField, HideInEditorMode, Space] private float currentValue;

    [HideInInspector] public Action<float> onValueChange;

    public override float value 
    {
        get => currentValue;
        set 
        {
            currentValue = value;
            onValueChange?.Invoke(value);
        }
    }

    protected override void OnAfterDeserialize()
    {
        RemoveAllValueChangeListner();
        ResetValue();
    }

    public void ResetValue()
    {
        value = startingValue;
    }

    public void RemoveAllValueChangeListner()
    {
        onValueChange = null;
    }

    public void ChangeStartingValue_EditorOnly(float newValue) 
    {
        if(!Application.isPlaying)
            startingValue = newValue;
    }

}