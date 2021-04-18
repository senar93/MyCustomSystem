namespace Senar.Data.Variable
{
    using Sirenix.OdinInspector;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Template for a generic variable
    /// </summary>
    /// <typeparam name="T">variable type</typeparam>
    public abstract class SO_Abs_Variable<T> : SerializedScriptableObject
    {
        /// <summary>
        /// Get and Set value, callback on set if needed
        /// </summary>
        public abstract T Value {
            get;
            set;
        }


        [SerializeField, FoldoutGroup("Settings", 0)]
        protected bool callOnValueChangedInEditor = false;


        /// <summary>
        /// Callback on value changed in editor, p1: NewValue
        /// </summary>
        [HideInInspector, NonSerialized]
        public Action<T> EDITOR_OnValueChanged;

        /// <summary>
        /// Call EDITOR_OnValueChanged if CallOnValueChangedInEditor is TRUE
        /// </summary>
        protected virtual void OnValidate()
        {
            if (callOnValueChangedInEditor)
            {
                EDITOR_OnValueChanged?.Invoke(Value);
            }
        }

    }
}