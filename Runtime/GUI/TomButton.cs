using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using DG.Tweening;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace TomDev.GUI
{
    public class TomButton : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
    {
        [Header("TomButton Options")]
        [SerializeField] public bool isScale = true;
#if ODIN_INSPECTOR
        [SerializeField, ToggleLeft] public bool isPlaySound = false;
        [ShowIf("isPlaySound")]
        [SerializeField] private AudioClip clickSound;
        [SerializeField, ToggleLeft] public bool isUrlButton = false;
        [ShowIf("isUrlButton")]
        [SerializeField] private string url = "";
#else
        [SerializeField] public bool isPlaySound = false;
        [SerializeField] private AudioClip clickSound;
        [SerializeField] public bool isUrlButton = false;
        [SerializeField] private string url = "";
#endif
        [SerializeField] public bool isVibrate = false;
        [SerializeField] private float scaleValue = 0.9f;
        [SerializeField] private float scaleDuration = 0.08f;
        [SerializeField] private float clickThreshold = 0.25f;

        private Vector3 _originalScale;
        private UnityAction _onClickAction;
        private bool _isPointerDown = false;
        private float _lastClickTime = 0f;

        private void Awake()
        {
            _originalScale = transform.localScale;
        }

        public void RegisterOnClick(UnityAction action)
        {
            _onClickAction += action;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            // Check threshold
            if (Time.time - _lastClickTime < clickThreshold)
            {
                return;
            }
            _lastClickTime = Time.time;

            if (isPlaySound && clickSound != null)
            {
                AudioSource.PlayClipAtPoint(clickSound, Camera.main != null ? Camera.main.transform.position : Vector3.zero);
            }
            if (isVibrate)
            {
#if UNITY_ANDROID || UNITY_IOS
                Handheld.Vibrate();
#endif
            }
            if (isUrlButton && !string.IsNullOrEmpty(url))
            {
                Application.OpenURL(url);
            }
            _onClickAction?.Invoke();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (isScale && !_isPointerDown)
            {
                _isPointerDown = true;
                transform.DOScale(_originalScale * scaleValue, scaleDuration).SetUpdate(true);
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (isScale && _isPointerDown)
            {
                _isPointerDown = false;
                transform.DOScale(_originalScale, scaleDuration).SetUpdate(true);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (isScale && _isPointerDown)
            {
                _isPointerDown = false;
                transform.DOScale(_originalScale, scaleDuration).SetUpdate(true);
            }
        }
    }
} 