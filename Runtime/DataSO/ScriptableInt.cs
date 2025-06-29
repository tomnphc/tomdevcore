using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace TomDev.DataSO
{
    [CreateAssetMenu(fileName = "New ScriptableInt", menuName = "TomDev/DataSO/Int")]
    public class ScriptableInt : ScriptableDataBase
    {
#if ODIN_INSPECTOR
        [Title("Int Value")]
        [SerializeField, MinValue(0)] private int defaultValue = 0;
        [SerializeField, ReadOnly] private int currentValue = 0;
#else
        [Header("Int Value")]
        [SerializeField] private int defaultValue = 0;
        [SerializeField] private int currentValue = 0;
#endif
        
#if ODIN_INSPECTOR
        [ShowInInspector, PropertyOrder(1)]
#endif
        public int Value
        {
            get
            {
                if (saved && !string.IsNullOrEmpty(prefKey))
                {
                    return PlayerPrefs.GetInt(prefKey, defaultValue);
                }
                return currentValue;
            }
        }
        
#if ODIN_INSPECTOR
        [Button("Set Value"), PropertyOrder(2)]
#endif
        public void SetValue(int value)
        {
            currentValue = value;
            InvokeValueChange();
            if (saved && !string.IsNullOrEmpty(prefKey))
            {
                PlayerPrefs.SetInt(prefKey, value);
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
        [Button("Increment"), PropertyOrder(4)]
#endif
        public void Increment()
        {
            SetValue(Value + 1);
        }
        
#if ODIN_INSPECTOR
        [Button("Decrement"), PropertyOrder(5)]
#endif
        public void Decrement()
        {
            SetValue(Value - 1);
        }
        
        protected override void SaveValueToPlayerPrefs()
        {
            PlayerPrefs.SetInt(prefKey, currentValue);
            PlayerPrefs.Save();
        }
        
        protected override void LoadValueFromPlayerPrefs()
        {
            if (PlayerPrefs.HasKey(prefKey))
            {
                currentValue = PlayerPrefs.GetInt(prefKey, defaultValue);
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
        public static implicit operator int(ScriptableInt scriptableInt)
        {
            return scriptableInt.Value;
        }
        
        public static ScriptableInt operator +(ScriptableInt a, int b)
        {
            a.SetValue(a.Value + b);
            return a;
        }
        
        public static ScriptableInt operator -(ScriptableInt a, int b)
        {
            a.SetValue(a.Value - b);
            return a;
        }
        
        public static ScriptableInt operator ++(ScriptableInt a)
        {
            a.SetValue(a.Value + 1);
            return a;
        }
        
        public static ScriptableInt operator --(ScriptableInt a)
        {
            a.SetValue(a.Value - 1);
            return a;
        }
    }
} 