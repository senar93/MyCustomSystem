using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Events;


[System.Serializable]
public class ShaderParameterTexture : AbsShaderParameter
{
    [ShowInInspector, ShowIf("CanSetMaterialParameter")]
    public Texture value {
        get {
            return CanSetMaterialParameter() ? targetMaterial.GetTexture(shaderParameterName) : null;
        }
        set {
            if (CanSetMaterialParameter())
            {
                onValueChange?.Invoke();
                targetMaterial?.SetTexture(shaderParameterName, value);
            }
        }
    }

    public ShaderParameterTexture(Material material, string shaderParameterName, Texture startingValue)
    {
        targetMaterial = material;
        this.shaderParameterName = shaderParameterName;
        value = startingValue;
    }

}