namespace Senar.Data.Variable
{
    using Sirenix.OdinInspector;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Variable with an initial default value.
    /// If possible it loads the value of the variable saved in the PlayerPrefs
    /// </summary>
    /// <typeparam name="T">variable type</typeparam>
    public abstract class SO_Abs_VariablePlayerPrefs<T> : SO_Abs_VariableStandard<T>
    {
        [PropertyOrder(int.MinValue), Required("The key must be set and must be unique!")]
        public string key;

        [SerializeField, FoldoutGroup("PlayerPrefs Settings", 0)]
        protected bool autoLoadPlayerPrefsOnEveryRead = true;
        [SerializeField, FoldoutGroup("PlayerPrefs Settings", 1)]
        protected bool autoSavePlayerPrefsOnValueChanged = true;


        protected abstract T GetValueFromPlayerPrefs();
        protected abstract void SetValueToPlayerPrefs(T valueToSet);

        public override T Value {
            get {
                if (autoLoadPlayerPrefsOnEveryRead)
                {
                    LoadFromPlayerPrefs();
                }
                return base.Value;
            }
            set {
                base.Value = value;
                if (autoSavePlayerPrefsOnValueChanged)
                {
                    SaveToPlayerPrefs();
                }
            }
        }

        /// <summary>
        /// check if Key exists
        /// </summary>
        /// <returns></returns>
        public bool HasKey()
        {
            return PlayerPrefs.HasKey(key);
        }

        /// <summary>
        /// save value to PlayerPrefs
        /// </summary>
        public void SaveToPlayerPrefs()
        {
            SetValueToPlayerPrefs(_value);
            PlayerPrefs.Save();
        }

        /// <summary>
        /// set value from PlayerPrefs, fallback to startingValue
        /// </summary>
        public void LoadFromPlayerPrefs()
        {
            _value = GetValueFromPlayerPrefs();
        }

    }
}