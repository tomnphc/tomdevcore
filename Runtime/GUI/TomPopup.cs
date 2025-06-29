using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Cysharp.Threading.Tasks;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace TomDev.GUI
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class TomPopup : MonoBehaviour
    {
        [Header("TomPopup Settings")]
        [SerializeField] private float fadeDuration = 0.25f;
        [SerializeField] private float scaleDuration = 0.35f;
        [SerializeField] private float backgroundAlpha = 0.8f;
        [SerializeField] public bool isShowBackground = true;
        [SerializeField] public Ease popupEasing = Ease.OutBack;

#if ODIN_INSPECTOR
        [Title("Sound Option")]
        [SerializeField, ToggleLeft] public bool playSound = true;
        [ShowIf("playSound")]
        [SerializeField] private AudioClip soundIn;
        [ShowIf("playSound")]
        [SerializeField] private AudioClip soundOut;
#else
        [Header("Sound Option")]
        [SerializeField] public bool playSound = true;
        [SerializeField] private AudioClip soundIn;
        [SerializeField] private AudioClip soundOut;
#endif

        [Header("Background Option")]
        public bool useCustomBackground = false;
        [Tooltip("Prefab background riêng cho popup này (nếu không dùng default)")]
        public GameObject customBackgroundPrefab;

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

            if (isShowBackground && _bgGroup != null)
            {
                _bgGroup.alpha = 0f;
                _bgGroup.DOFade(backgroundAlpha, fadeDuration).SetUpdate(true);
            }
            _popupGroup.alpha = 0f;
            _popupRoot.localScale = Vector3.one * 0.5f;
            _showTween?.Kill();
            _showTween = DOTween.Sequence()
                .Append(_popupGroup.DOFade(1f, fadeDuration).SetUpdate(true))
                .Join(_popupRoot.DOScale(1f, scaleDuration).SetEase(popupEasing).SetUpdate(true))
                .SetUpdate(true);

            if (playSound && soundIn != null)
            {
                AudioSource.PlayClipAtPoint(soundIn, Camera.main != null ? Camera.main.transform.position : Vector3.zero);
            }
        }

        public virtual async UniTask HideAndDestroy()
        {
            if (playSound && soundOut != null)
            {
                AudioSource.PlayClipAtPoint(soundOut, Camera.main != null ? Camera.main.transform.position : Vector3.zero);
            }
            
            _hideTween?.Kill();
            _hideTween = DOTween.Sequence()
                .Append(_popupGroup.DOFade(0f, fadeDuration).SetUpdate(true))
                .Join(_popupRoot.DOScale(0.5f, scaleDuration).SetEase(popupEasing == Ease.OutBack ? Ease.InBack : popupEasing).SetUpdate(true))
                .Join((isShowBackground && _bgGroup != null) ? _bgGroup.DOFade(0f, fadeDuration).SetUpdate(true) : null)
                .SetUpdate(true);

            await _hideTween.AsyncWaitForCompletion().AsUniTask();
            
            if (_bgGroup != null)
                Destroy(_bgGroup.gameObject);
            Destroy(gameObject);
        }
    }
} 