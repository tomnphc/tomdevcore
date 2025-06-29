using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace TomDev.DataSO
{
    [CreateAssetMenu(fileName = "New ScriptableString", menuName = "TomDev/DataSO/String")]
    public class ScriptableString : ScriptableDataBase
    {
#if ODIN_INSPECTOR
        [Title("String Value")]
        [SerializeField, TextArea(3, 5)] private string defaultValue = "";
        [SerializeField, ReadOnly] private string currentValue = "";
#else
        [Header("String Value")]
        [SerializeField] private string defaultValue = "";
        [SerializeField] private string currentValue = "";
#endif
        
#if ODIN_INSPECTOR
        [ShowInInspector, PropertyOrder(1)]
#endif
        public string Value
        {
            get
            {
                if (saved && !string.IsNullOrEmpty(prefKey))
                {
                    return PlayerPrefs.GetString(prefKey, defaultValue);
                }
                return currentValue;
            }
        }
        
#if ODIN_INSPECTOR
        [Button("Set Value"), PropertyOrder(2)]
#endif
        public void SetValue(string value)
        {
            currentValue = value ?? "";
            InvokeValueChange();
            if (saved && !string.IsNullOrEmpty(prefKey))
            {
                PlayerPrefs.SetString(prefKey, currentValue);
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
        [Button("Clear"), PropertyOrder(4)]
#endif
        public void Clear()
        {
            SetValue("");
        }
        
#if ODIN_INSPECTOR
        [Button("Append 'Test'"), PropertyOrder(5)]
#endif
        public void AppendTest()
        {
            SetValue(Value + "Test");
        }
        
        protected override void SaveValueToPlayerPrefs()
        {
            PlayerPrefs.SetString(prefKey, currentValue);
            PlayerPrefs.Save();
        }
        
        protected override void LoadValueFromPlayerPrefs()
        {
            if (PlayerPrefs.HasKey(prefKey))
            {
                currentValue = PlayerPrefs.GetString(prefKey, defaultValue);
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
        public static implicit operator string(ScriptableString scriptableString)
        {
            return scriptableString.Value;
        }
        
        public static ScriptableString operator +(ScriptableString a, string b)
        {
            a.SetValue(a.Value + b);
            return a;
        }
        
        // Helper methods
        public bool IsEmpty => string.IsNullOrEmpty(Value);
        public bool IsNullOrWhiteSpace => string.IsNullOrWhiteSpace(Value);
        public int Length => Value.Length;
        
        public bool Contains(string value)
        {
            return Value.Contains(value);
        }
        
        public bool StartsWith(string value)
        {
            return Value.StartsWith(value);
        }
        
        public bool EndsWith(string value)
        {
            return Value.EndsWith(value);
        }
    }
} 