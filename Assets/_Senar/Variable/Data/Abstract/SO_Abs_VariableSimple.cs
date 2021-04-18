namespace Senar.Data.Variable
{
    using Sirenix.OdinInspector;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Simple variable without an initial state
    /// </summary>
    /// <typeparam name="T">variable type</typeparam>
    public abstract class SO_Abs_VariableSimple<T> : SO_Abs_Variable<T>
    {
        /// <summary>
        /// Current value of the variable
        /// </summary>
        [SerializeField, DisableInEditorMode]
        protected T _value;

        [SerializeField, FoldoutGroup("Settings", 1)]
        protected bool callOnValueChanged = false;


        /// <summary>
        /// Get and Set value, callback on set if needed
        /// </summary>
        public override T Value {
            get {
                return this._value;
            }
            set {
                T tmp = _value;
                _value = value;
                if (callOnValueChanged)
                {
                    OnValueChanged?.Invoke(_value, tmp);
                }
            }
        }

        /// <summary>
        /// Callback on value changed, p1: NewValue, p2: OldValue
        /// </summary>
        [HideInInspector, NonSerialized]
        public Action<T, T> OnValueChanged;

    }
}