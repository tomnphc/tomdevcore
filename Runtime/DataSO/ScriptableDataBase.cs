using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace TomDev.DataSO
{
    public abstract class ScriptableDataBase : ScriptableObject
    {
#if ODIN_INSPECTOR
        [Title("Save Settings")]
        [SerializeField, ToggleLeft] protected bool saved = false;
        [SerializeField, ShowIf("saved"), Required] protected string prefKey = "";
#else
        [Header("Save Settings")]
        [SerializeField] protected bool saved = false;
        [SerializeField] protected string prefKey = "";
#endif
        
        public bool Saved => saved;
        public string PrefKey => prefKey;
        
        protected virtual void OnValidate()
        {
            // Ensure prefKey is not empty when saved is true
            if (saved && string.IsNullOrEmpty(prefKey))
            {
                prefKey = name;
            }
        }
        
#if ODIN_INSPECTOR
        [Button("Check Existed Key"), ShowIf("saved")]
#endif
        [ContextMenu("Check Existed Key")]
        public virtual void CheckExistedKey()
        {
            if (!saved || string.IsNullOrEmpty(prefKey))
            {
                Debug.LogWarning($"[{name}] Cannot check key: Saved is false or PrefKey is empty");
                return;
            }
            
            bool exists = PlayerPrefs.HasKey(prefKey);
            Debug.Log($"[{name}] Key '{prefKey}' exists in PlayerPrefs: {exists}");
        }
        
#if ODIN_INSPECTOR
        [Button("Clear PlayerPrefs Key"), ShowIf("saved")]
#endif
        public virtual void ClearPlayerPrefsKey()
        {
            if (!saved || string.IsNullOrEmpty(prefKey))
            {
                Debug.LogWarning($"[{name}] Cannot clear key: Saved is false or PrefKey is empty");
                return;
            }
            
            PlayerPrefs.DeleteKey(prefKey);
            PlayerPrefs.Save();
            Debug.Log($"[{name}] Cleared PlayerPrefs key: {prefKey}");
        }
        
        protected virtual void SaveToPlayerPrefs()
        {
            if (!saved || string.IsNullOrEmpty(prefKey))
                return;
                
            SaveValueToPlayerPrefs();
        }
        
        protected virtual void LoadFromPlayerPrefs()
        {
            if (!saved || string.IsNullOrEmpty(prefKey))
                return;
                
            LoadValueFromPlayerPrefs();
        }
        
        protected abstract void SaveValueToPlayerPrefs();
        protected abstract void LoadValueFromPlayerPrefs();
    }
} 