﻿namespace Senar.Data.Variable
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

    }
}