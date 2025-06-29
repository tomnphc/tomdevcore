using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace TomDev.DataSO
{
    [CreateAssetMenu(fileName = "New ScriptableFloat", menuName = "TomDev/DataSO/Float")]
    public class ScriptableFloat : ScriptableDataBase
    {
#if ODIN_INSPECTOR
        [Title("Float Value")]
        [SerializeField, MinValue(0f)] private float defaultValue = 0f;
        [SerializeField, ReadOnly] private float currentValue = 0f;
#else
        [Header("Float Value")]
        [SerializeField] private float defaultValue = 0f;
        [SerializeField] private float currentValue = 0f;
#endif
        
#if ODIN_INSPECTOR
        [ShowInInspector, PropertyOrder(1)]
#endif
        public float Value
        {
            get
            {
                if (saved && !string.IsNullOrEmpty(prefKey))
                {
                    return PlayerPrefs.GetFloat(prefKey, defaultValue);
                }
                return currentValue;
            }
        }
        
#if ODIN_INSPECTOR
        [Button("Set Value"), PropertyOrder(2)]
#endif
        public void SetValue(float value)
        {
            currentValue = value;
            InvokeValueChange();
            if (saved && !string.IsNullOrEmpty(prefKey))
            {
                PlayerPrefs.SetFloat(prefKey, value);
                PlayerPrefs.Save();
            }
        }
        
#if ODIN_INSPECTOR
        [Button("Reset to Default"), PropertyOrder(3)]
#endif
        public void ResetToDefault()
        {
            SetValue(defaultValue);
        }
        
#if ODIN_INSPECTOR
        [Button("Add 1"), PropertyOrder(4)]
#endif
        public void AddOne()
        {
            SetValue(Value + 1f);
        }
        
#if ODIN_INSPECTOR
        [Button("Subtract 1"), PropertyOrder(5)]
#endif
        public void SubtractOne()
        {
            SetValue(Value - 1f);
        }
        
        protected override void SaveValueToPlayerPrefs()
        {
            PlayerPrefs.SetFloat(prefKey, currentValue);
            PlayerPrefs.Save();
        }
        
        protected override void LoadValueFromPlayerPrefs()
        {
            if (PlayerPrefs.HasKey(prefKey))
            {
                currentValue = PlayerPrefs.GetFloat(prefKey, defaultValue);
            }
        }
        
        protected override void OnValidate()
        {
            base.OnValidate();
            
            // Auto-save when value changes in editor
            if (saved && !string.IsNullOrEmpty(prefKey))
            {
                SaveToPlayerPrefs();
            }
        }
        
        // Operator overloads for easier usage
        public static implicit operator float(ScriptableFloat scriptableFloat)
        {
            return scriptableFloat.Value;
        }
        
        public static ScriptableFloat operator +(ScriptableFloat a, float b)
        {
            a.SetValue(a.Value + b);
            return a;
        }
        
        public static ScriptableFloat operator -(ScriptableFloat a, float b)
        {
            a.SetValue(a.Value - b);
            return a;
        }
        
        public static ScriptableFloat operator *(ScriptableFloat a, float b)
        {
            a.SetValue(a.Value * b);
            return a;
        }
        
        public static ScriptableFloat operator /(ScriptableFloat a, float b)
        {
            if (b != 0)
            {
                a.SetValue(a.Value / b);
            }
            return a;
        }
    }
} 