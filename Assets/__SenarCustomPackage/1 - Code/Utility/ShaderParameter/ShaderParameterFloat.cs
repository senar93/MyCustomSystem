using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Events;


[System.Serializable]
public class ShaderParameterFloat : AbsShaderParameter
{

    [ShowInInspector, ShowIf("CanSetMaterialParameter")]
    public float value {
        get {
            return CanSetMaterialParameter() ? targetMaterial.GetFloat(shaderParameterName) : -9999;
        }
        set {
            if (CanSetMaterialParameter())
            {
                onValueChange?.Invoke();
                targetMaterial?.SetFloat(shaderParameterName, value);
            }
        }
    }

    public ShaderParameterFloat(Material material, string shaderParameterName, float startingValue)
    {
        targetMaterial = material;
        this.shaderParameterName = shaderParameterName;
        value = startingValue;
    }

}