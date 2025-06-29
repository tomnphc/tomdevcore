using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

namespace TomDev.GUI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class TomNotification : MonoBehaviour
    {
        [Header("Notification Settings")]
        [SerializeField] private TMP_Text contentText;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private RectTransform rectTransform;
        
        [Header("Animation Settings")]
        [SerializeField] private float fadeInDuration = 0.3f;
        [SerializeField] private float moveUpDistance = 100f;
        [SerializeField] private float totalDuration = 2f;
        [SerializeField] private float fadeOutDuration = 0.3f;

        private Vector3 _originalPosition;
        private Sequence _animationSequence;

        private void Awake()
        {
            if (contentText == null)
                contentText = GetComponentInChildren<TMP_Text>();
            if (canvasGroup == null)
                canvasGroup = GetComponent<CanvasGroup>();
            if (rectTransform == null)
                rectTransform = GetComponent<RectTransform>();
            
            _originalPosition = rectTransform.anchoredPosition;
        }

        public void ShowNotification(string content)
        {
            if (contentText != null)
                contentText.text = content;

            // Reset position and alpha
            rectTransform.anchoredPosition = _originalPosition;
            canvasGroup.alpha = 0f;

            // Kill previous animation if any
            _animationSequence?.Kill();

            // Create new animation sequence
            _animationSequence = DOTween.Sequence();

            // Fade in and move up at the same time
            float moveAndFadeDuration = totalDuration - fadeOutDuration;
            _animationSequence.Append(canvasGroup.DOFade(1f, fadeInDuration));
            _animationSequence.Join(rectTransform.DOAnchorPosY(_originalPosition.y + moveUpDistance, moveAndFadeDuration));

            // Fade out at the end
            _animationSequence.Join(canvasGroup.DOFade(0f, fadeOutDuration).SetDelay(moveAndFadeDuration - fadeOutDuration));

            // Destroy when complete
            _animationSequence.OnComplete(() => {
                Destroy(gameObject);
            });
        }

        private void OnDestroy()
        {
            _animationSequence?.Kill();
        }
    }
} 