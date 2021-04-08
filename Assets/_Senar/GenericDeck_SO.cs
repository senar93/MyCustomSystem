using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericDeck_SO<T> : SerializedScriptableObject
{
    public GenericDeck<T> deck;
}
