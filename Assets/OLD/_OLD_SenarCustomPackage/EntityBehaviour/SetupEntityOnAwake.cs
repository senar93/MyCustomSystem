using SenarCustomSystem.EntityBehaviour;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AbsEntity)), DisallowMultipleComponent]
public class SetupEntityOnAwake : MonoBehaviour
{
    private void Awake()
    {
        AbsEntity tmp;
        if (TryGetComponent(out tmp))
        {
            tmp.Setup();
        }
    }

    [HideInPlayMode, Button("Remove Setup On Awake", ButtonSizes.Medium), GUIColor(1f, 0.4f, 0.4f)]
    private void Inspector_RemoveSetupEntityOnAwake()
    {
        DestroyImmediate(this,true);
    }
}
