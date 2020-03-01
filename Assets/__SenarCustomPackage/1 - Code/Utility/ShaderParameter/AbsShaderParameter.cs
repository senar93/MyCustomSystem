using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Events;


[System.Serializable]
public abstract class AbsShaderParameter
{
    public Material targetMaterial;
    public string shaderParameterName;

    public UnityEvent onValueChange;

    protected bool CanSetMaterialParameter()
    {
        return targetMaterial != null && targetMaterial.HasProperty(shaderParameterName);
    }
}
