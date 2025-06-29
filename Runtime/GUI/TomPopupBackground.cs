using UnityEngine;
using UnityEngine.EventSystems;

namespace TomDev.GUI
{
    public class TomPopupBackground : MonoBehaviour, IPointerClickHandler
    {
        [Header("Background Option")]
        public bool useDefaultBackground = true;
        [Tooltip("Prefab background riêng cho popup này (nếu không dùng default)")]
        public GameObject customBackgroundPrefab;

        private TomPopup _popup;

        public void SetPopup(TomPopup popup)
        {
            _popup = popup;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_popup != null)
            {
                _popup.HideAndDestroy();
            }
        }
    }
} 