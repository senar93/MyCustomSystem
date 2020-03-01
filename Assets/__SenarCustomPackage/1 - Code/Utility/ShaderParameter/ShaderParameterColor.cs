using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Events;


[System.Serializable]
public class ShaderParameterColor : AbsShaderParameter
{
    [ShowInInspector, ShowIf("CanSetMaterialParameter")]
    public Color value {
        get {
            return CanSetMaterialParameter() ? targetMaterial.GetColor(shaderParameterName) : Color.clear;
        }
        set {
            if (CanSetMaterialParameter())
            {
                onValueChange?.Invoke();
                targetMaterial?.SetColor(shaderParameterName, value);
            }
        }
    }

    public ShaderParameterColor(Material material, string shaderParameterName, Color startingValue)
    {
        targetMaterial = material;
        this.shaderParameterName = shaderParameterName;
        value = startingValue;
    }

}