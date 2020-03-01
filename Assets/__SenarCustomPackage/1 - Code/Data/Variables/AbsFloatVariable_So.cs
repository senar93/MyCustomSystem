using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


//[CreateAssetMenu("MyCustomSystem/Variables/Float")]
public abstract class AbsFloatVariable_So : SerializedScriptableObject
{
    public virtual float value { get; set; }
}
