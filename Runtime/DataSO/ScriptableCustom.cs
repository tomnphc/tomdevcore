using UnityEngine;
using System;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace TomDev.DataSO
{
    [CreateAssetMenu(fileName = "New ScriptableCustom", menuName = "TomDev/DataSO/Custom")]
    public class ScriptableCustom<T> : ScriptableDataBase where T : class, new()
    {
#if ODIN_INSPECTOR
        [Title("Custom Value")]
        [SerializeField, InlineEditor] private T defaultValue = new T();
        [SerializeField, ReadOnly, InlineEditor] private T currentValue = new T();
#else
        [Header("Custom Value")]
        [SerializeField] private T defaultValue = new T();
        [SerializeField] private T currentValue = new T();
#endif
        
#if ODIN_INSPECTOR
        [ShowInInspector, PropertyOrder(1), InlineEditor]
#endif
        public T Value
        {
            get
            {
                if (saved && !string.IsNullOrEmpty(prefKey))
                {
                    try
                    {
                        string json = PlayerPrefs.GetString(prefKey, "");
                        if (!string.IsNullOrEmpty(json))
                        {
                            return JsonUtility.FromJson<T>(json);
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.LogError($"[{name}] Error loading custom value: {e.Message}");
                    }
                    return defaultValue;
                }
                return currentValue;
            }
        }
        
#if ODIN_INSPECTOR
        [Button("Set Value"), PropertyOrder(2)]
#endif
        public void SetValue(T value)
        {
            currentValue = value ?? new T();
            
            if (saved && !string.IsNullOrEmpty(prefKey))
            {
                try
                {
                    string json = JsonUtility.ToJson(currentValue);
                    PlayerPrefs.SetString(prefKey, json);
                    PlayerPrefs.Save();
                }
                catch (Exception e)
                {
                    Debug.LogError($"[{name}] Error saving custom value: {e.Message}");
                }
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
        [Button("Check if Default"), PropertyOrder(4)]
#endif
        public void CheckIfDefault()
        {
            bool isDefault = IsDefault();
            Debug.Log($"[{name}] Is default value: {isDefault}");
        }
        
        protected override void SaveValueToPlayerPrefs()
        {
            try
            {
                string json = JsonUtility.ToJson(currentValue);
                PlayerPrefs.SetString(prefKey, json);
                PlayerPrefs.Save();
            }
            catch (Exception e)
            {
                Debug.LogError($"[{name}] Error saving custom value: {e.Message}");
            }
        }
        
        protected override void LoadValueFromPlayerPrefs()
        {
            if (PlayerPrefs.HasKey(prefKey))
            {
                try
                {
                    string json = PlayerPrefs.GetString(prefKey, "");
                    if (!string.IsNullOrEmpty(json))
                    {
                        currentValue = JsonUtility.FromJson<T>(json);
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError($"[{name}] Error loading custom value: {e.Message}");
                    currentValue = defaultValue;
                }
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
        
        // Helper methods
        public bool IsDefault()
        {
            return JsonUtility.ToJson(Value) == JsonUtility.ToJson(defaultValue);
        }
    }
} 