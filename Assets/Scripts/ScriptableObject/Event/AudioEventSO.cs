using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/AudioEventSO")]
public class AudioEventSO : ScriptableObject
{
    public UnityAction<AudioClip> onEventRaised;

    public void RaiseEvent(AudioClip audioClip)
    {
        onEventRaised?.Invoke(audioClip);
    }
}
