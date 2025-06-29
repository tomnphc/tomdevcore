using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TomDev.GUI
{
    public class PopupManager : MonoBehaviour
    {
        public static PopupManager Instance { get; private set; }
        [Header("Reference to the Canvas for popups")]
        public Canvas popupCanvas;

        [Header("Background Settings")]
        public Color backgroundColor = Color.black;
        [Range(0, 1)] public float backgroundAlpha = 0.8f;
        
        private readonly Dictionary<Type, TomPopup> _popupDict = new();
        private readonly Dictionary<TomPopup, GameObject> _backgroundDict = new();

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public T Show<T>() where T : TomPopup
        {
            var popup = CreatePopup<T>();
            if (popup != null)
                popup.Show();
            return popup;
        }

        public void DestroyPopup<T>() where T : TomPopup
        {
            var type = typeof(T);
            if (_popupDict.TryGetValue(type, out var popup) && popup != null)
            {
                if (_backgroundDict.TryGetValue(popup, out var bg) && bg != null)
                {
                    Destroy(bg);
                    _backgroundDict.Remove(popup);
                }
                Destroy(popup.gameObject);
                _popupDict.Remove(type);
            }
        }

        private T CreatePopup<T>() where T : TomPopup
        {
            var type = typeof(T);
            if (_popupDict.TryGetValue(type, out var popup) && popup != null)
                return (T)popup;

            // Chỉ load từ Resources
            var prefab = Resources.Load<T>(type.Name);
            if (prefab != null)
            {
                popup = Instantiate(prefab);
            }
            else
            {
                Debug.LogError($"[PopupManager] Prefab '{type.Name}' not found in Resources! Please create a prefab named '{type.Name}' in a Resources folder.");
                return null;
            }

            // Set parent to canvas
            if (popupCanvas != null)
            {
                popup.transform.SetParent(popupCanvas.transform, false);
            }
            else
            {
                Debug.LogWarning("PopupManager: popupCanvas is not assigned!");
            }

            // Create background (custom or default)
            var bg = CreateBackground(popup.transform, popup);
            _backgroundDict[popup] = bg;
            popup.SetBackground(bg.GetComponent<CanvasGroup>());

            _popupDict[type] = popup;
            return (T)popup;
        }

        private GameObject CreateBackground(Transform popupTransform, TomPopup popup)
        {
            GameObject bgGo = null;
            if (popup.useCustomBackground && popup.customBackgroundPrefab != null)
            {
                bgGo = Instantiate(popup.customBackgroundPrefab);
            }
            else
            {
                bgGo = new GameObject("TomPopup_Background", typeof(RectTransform), typeof(CanvasGroup), typeof(Image));
                var img = bgGo.GetComponent<Image>();
                img.color = new Color(backgroundColor.r, backgroundColor.g, backgroundColor.b, 1f);
                img.raycastTarget = true;
            }
            var bgRect = bgGo.GetComponent<RectTransform>();
            bgRect.SetParent(popupCanvas != null ? popupCanvas.transform : popupTransform.parent, false);
            bgRect.SetAsLastSibling();
            bgRect.anchorMin = Vector2.zero;
            bgRect.anchorMax = Vector2.one;
            bgRect.offsetMin = Vector2.zero;
            bgRect.offsetMax = Vector2.zero;
            var group = bgGo.GetComponent<CanvasGroup>();
            group.alpha = 0f;
            group.interactable = true;
            group.blocksRaycasts = true;
            // Gắn script background
            var bgScript = bgGo.GetComponent<TomPopupBackground>() ?? bgGo.AddComponent<TomPopupBackground>();
            bgScript.SetPopup(popup);
            // Đảm bảo background nằm dưới popup
            popupTransform.SetAsLastSibling();
            bgGo.transform.SetSiblingIndex(popupTransform.GetSiblingIndex() - 1);
            return bgGo;
        }
    }
}