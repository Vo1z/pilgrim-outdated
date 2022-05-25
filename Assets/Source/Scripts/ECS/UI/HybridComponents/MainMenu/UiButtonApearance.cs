using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button), typeof(CanvasGroup))]
public class UiButtonApearance : MonoBehaviour
{
    [SerializeField] [Range(0, 1)] private float fadedOutAlpha = .3f; 
    [SerializeField] private float blinkingAnimationDuration = .5f;
    
    private CanvasGroup _canvasGroup;
    private Sequence _blinkingSequence;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void StartBlinking()
    {
        _blinkingSequence = DOTween.Sequence()
            .Append(_canvasGroup.DOFade(1, blinkingAnimationDuration))
            .Append(_canvasGroup.DOFade(fadedOutAlpha, blinkingAnimationDuration))
            .Append(_canvasGroup.DOFade(1, blinkingAnimationDuration))
            .SetLoops(-1);
    }

    public void StopBlinking()
    {
        if (_blinkingSequence != null)
        {
            _blinkingSequence.Kill();
            _blinkingSequence = null;
        }

        _canvasGroup.alpha = 1;
    }
}
