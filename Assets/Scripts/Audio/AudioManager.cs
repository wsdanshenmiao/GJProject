using System;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// 用于管理不同的音效
/// </summary>
public class AudioManager : MonoBehaviour
{
    public FloatEventSO masterVolumeEvent;
    public FloatEventSO BGMVolumeEvent;
    public FloatEventSO FXVolumeEvent;

    public AudioEventSO BGMEvent;
    public AudioEventSO FXEvent;

    public AudioSource BGMSource;
    public AudioSource FXSource;
    public AudioMixer audioMixer;

    /// <summary>
    /// 导入事件
    /// </summary>
    private void OnEnable()
    {
        BGMEvent.onEventRaised += OnBGMEvent;
        FXEvent.onEventRaised += OnFXEvent;
        masterVolumeEvent.onEventRaised += OnMasterVolumeChangeEvent;
        BGMVolumeEvent.onEventRaised += OnBGMVolumeChangeEvent;
        FXVolumeEvent.onEventRaised += OnFXVolumeChangeEvent;
    }

    private void OnDisable()
    {
        BGMEvent.onEventRaised -= OnBGMEvent;
        FXEvent.onEventRaised -= OnFXEvent;
        masterVolumeEvent.onEventRaised -= OnMasterVolumeChangeEvent;
        BGMVolumeEvent.onEventRaised -= OnBGMVolumeChangeEvent;
        FXVolumeEvent.onEventRaised -= OnFXVolumeChangeEvent;
    }

    private void OnFXVolumeChangeEvent(float value)
    {
        value *= 100;
        audioMixer.SetFloat("FXVolume", value - 80);
    }

    private void OnBGMVolumeChangeEvent(float value)
    {
        value *= 100;
        audioMixer.SetFloat("BGMVolume", value - 80);
    }

    private void OnMasterVolumeChangeEvent(float value)
    {
        value *= 100;
        audioMixer.SetFloat("MasterVolume", value - 80);
    }

    /// <summary>
    /// 接收广播执行事件
    /// </summary>
    /// <param name="clip"></param>
    private void OnFXEvent(AudioClip clip)
    {
        FXSource.clip = clip;
        FXSource.Play();
    }

    private void OnBGMEvent(AudioClip clip)
    {
        BGMSource.clip = clip;
        BGMSource.Play();
    }
}
