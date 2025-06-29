using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace TomDev.GUI
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class TomPopup : MonoBehaviour
    {
        [Header("TomPopup Settings")]
        [SerializeField] private float fadeDuration = 0.25f;
        [SerializeField] private float scaleDuration = 0.35f;
        [SerializeField] private float backgroundAlpha = 0.8f;

        private CanvasGroup _popupGroup;
        private CanvasGroup _bgGroup;
        private Transform _popupRoot;
        private Sequence _showTween, _hideTween;

        public void SetBackground(CanvasGroup bgGroup)
        {
            _bgGroup = bgGroup;
        }

        protected virtual void Awake()
        {
            _popupGroup = GetComponent<CanvasGroup>();
            _popupRoot = transform;
        }

        public virtual void Show()
        {
            gameObject.SetActive(true);
            _popupGroup = _popupGroup ?? GetComponent<CanvasGroup>();
            _popupRoot = _popupRoot ?? transform;

            if (_bgGroup != null)
            {
                _bgGroup.alpha = 0f;
                _bgGroup.DOFade(backgroundAlpha, fadeDuration).SetUpdate(true);
            }
            _popupGroup.alpha = 0f;
            _popupRoot.localScale = Vector3.one * 0.5f;
            _showTween?.Kill();
            _showTween = DOTween.Sequence()
                .Append(_popupGroup.DOFade(1f, fadeDuration).SetUpdate(true))
                .Join(_popupRoot.DOScale(1f, scaleDuration).SetEase(Ease.OutBack).SetUpdate(true))
                .SetUpdate(true);
        }

        public virtual void HideAndDestroy()
        {
            _hideTween?.Kill();
            _hideTween = DOTween.Sequence()
                .Append(_popupGroup.DOFade(0f, fadeDuration).SetUpdate(true))
                .Join(_popupRoot.DOScale(0.5f, scaleDuration).SetEase(Ease.InBack).SetUpdate(true))
                .Join(_bgGroup != null ? _bgGroup.DOFade(0f, fadeDuration).SetUpdate(true) : null)
                .SetUpdate(true)
                .OnComplete(() =>
                {
                    if (_bgGroup != null)
                        Destroy(_bgGroup.gameObject);
                    Destroy(gameObject);
                });
        }
    }
} 