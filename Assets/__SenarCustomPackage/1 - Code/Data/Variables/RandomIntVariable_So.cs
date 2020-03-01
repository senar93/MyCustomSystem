using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyCustomSystem/Variables/Int/RandomIntVar",
                 fileName = "_NEW Random Int")]
public class RandomIntVariable_So : AbsIntVariable_So
{
    public SeedRandom randomParameter;

    public override int value
    {
        get { return randomParameter.currentValue; }
        set { }
    }

    protected override void OnAfterDeserialize()
    {
        Reset();
    }

    public void Reset()
    {
        randomParameter = new SeedRandom(randomParameter.seed, randomParameter.min, randomParameter.max);
    }

    public float GetNewRandomValue()
    {
        return randomParameter.GetNextValue();
    }

}
