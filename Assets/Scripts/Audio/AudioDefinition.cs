using UnityEngine;

public class AudioDefinition : MonoBehaviour
{
    public AudioEventSO audioEventSO;
    public AudioClip audioClip;

    public bool eablePlayAudio;

    private void OnEnable()
    {
        if (eablePlayAudio)
            PlayAudio();
    }

    /// <summary>
    /// 唤起事件
    /// </summary>
    private void PlayAudio()
    {
        audioEventSO.RaiseEvent(audioClip);
    }

}
