using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace TomDev.DataSO
{
    [CreateAssetMenu(fileName = "New ScriptableBool", menuName = "TomDev/DataSO/Bool")]
    public class ScriptableBool : ScriptableDataBase
    {
#if ODIN_INSPECTOR
        [Title("Bool Value")]
        [SerializeField, ToggleLeft] private bool defaultValue = false;
        [SerializeField, ReadOnly] private bool currentValue = false;
#else
        [Header("Bool Value")]
        [SerializeField] private bool defaultValue = false;
        [SerializeField] private bool currentValue = false;
#endif
        
#if ODIN_INSPECTOR
        [ShowInInspector, PropertyOrder(1)]
#endif
        public bool Value
        {
            get
            {
                if (saved && !string.IsNullOrEmpty(prefKey))
                {
                    return PlayerPrefs.GetInt(prefKey, defaultValue ? 1 : 0) == 1;
                }
                return currentValue;
            }
        }
        
#if ODIN_INSPECTOR
        [Button("Set Value"), PropertyOrder(2)]
#endif
        public void SetValue(bool value)
        {
            currentValue = value;
            
            if (saved && !string.IsNullOrEmpty(prefKey))
            {
                PlayerPrefs.SetInt(prefKey, value ? 1 : 0);
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
        [Button("Toggle"), PropertyOrder(4)]
#endif
        public void Toggle()
        {
            SetValue(!Value);
        }
        
#if ODIN_INSPECTOR
        [Button("Set True"), PropertyOrder(5)]
#endif
        public void SetTrue()
        {
            SetValue(true);
        }
        
#if ODIN_INSPECTOR
        [Button("Set False"), PropertyOrder(6)]
#endif
        public void SetFalse()
        {
            SetValue(false);
        }
        
        protected override void SaveValueToPlayerPrefs()
        {
            PlayerPrefs.SetInt(prefKey, currentValue ? 1 : 0);
            PlayerPrefs.Save();
        }
        
        protected override void LoadValueFromPlayerPrefs()
        {
            if (PlayerPrefs.HasKey(prefKey))
            {
                currentValue = PlayerPrefs.GetInt(prefKey, defaultValue ? 1 : 0) == 1;
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
        public static implicit operator bool(ScriptableBool scriptableBool)
        {
            return scriptableBool.Value;
        }
        
        public static ScriptableBool operator !(ScriptableBool a)
        {
            a.SetValue(!a.Value);
            return a;
        }
        
        public static ScriptableBool operator &(ScriptableBool a, bool b)
        {
            a.SetValue(a.Value && b);
            return a;
        }
        
        public static ScriptableBool operator |(ScriptableBool a, bool b)
        {
            a.SetValue(a.Value || b);
            return a;
        }
        
        public static ScriptableBool operator ^(ScriptableBool a, bool b)
        {
            a.SetValue(a.Value ^ b);
            return a;
        }
    }
} 