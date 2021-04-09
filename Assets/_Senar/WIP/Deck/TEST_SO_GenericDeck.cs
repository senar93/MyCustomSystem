namespace Senar.WIP
{
    using Sirenix.OdinInspector;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class TEST_SO_GenericDeck<T> : SerializedScriptableObject
    {
        public GenericDeck<T> deck;
    }
}