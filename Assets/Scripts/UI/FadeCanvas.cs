using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Unity.VisualScripting;

public class FadeCanvas : MonoBehaviour
{
    [SerializeField] Image fadeImage;
    [Header("事件广播")]
    [SerializeField] FadeEventSO fadeEvent;

    private void OnEnable()
    {
        fadeEvent.onEventRaised += OnFadeEvent;
    }

    private void OnDisable()
    {
        fadeEvent.onEventRaised -= OnFadeEvent;
    }

    private void OnFadeEvent(Color color, float duration, bool fadeIn)
    {
        fadeImage.DOBlendableColor(color, duration);
    }
}
