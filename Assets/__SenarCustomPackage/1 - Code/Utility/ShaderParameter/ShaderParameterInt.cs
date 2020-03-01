using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Events;


[System.Serializable]
public class ShaderParameterInt : AbsShaderParameter
{

    [ShowInInspector, ShowIf("CanSetMaterialParameter")]
    public int value {
        get {
            return CanSetMaterialParameter() ? targetMaterial.GetInt(shaderParameterName) : -9999;
        }
        set {
            if (targetMaterial != null && targetMaterial.HasProperty(shaderParameterName))
            {
                onValueChange?.Invoke();
                targetMaterial?.SetInt(shaderParameterName, value);
            }
        }
    }

    public ShaderParameterInt(Material material, string shaderParameterName, int startingValue)
    {
        targetMaterial = material;
        this.shaderParameterName = shaderParameterName;
        value = startingValue;
    }

}
