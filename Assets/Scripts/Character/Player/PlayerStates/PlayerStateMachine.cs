using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [SerializeField] PlayerState[] playerStates;
    [SerializeField] AudioClip[] playerAudio;

    [Header("事件监听")]
    [SerializeField] private VoidEventSO changeWorldStateEvent;

    Animator animator;

    PlayerController playerController;
    PlayerInput playerInput;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();

        playerInput = GetComponent<PlayerInput>();

        playerController = GetComponent<PlayerController>();
        audioSource = GetComponent<AudioSource>();

        stateTable = new Dictionary<System.Type, IState>(playerStates.Length);
        //audioTable = new Dictionary<System.Type, AudioClip>(playerAudio.Length);

        foreach (PlayerState state in playerStates)
        {
            state.Initialize(animator, playerController, playerInput, this);
            stateTable.Add(state.GetType(), state);
        }

        /*
        foreach (AudioClip audio in playerAudio)
        {
            audioTable.Add(audio.GetType(), audio);
        }
        */
    }

    private void OnEnable()
    {
        changeWorldStateEvent.onEventRaised += OnChangeWorldState;
    }

    void Start()
    {
        SwitchOn(stateTable[typeof(Wait)]); 
    }

    private void OnDisable()
    {
        changeWorldStateEvent.onEventRaised += OnChangeWorldState;
    }

    private void OnChangeWorldState()
    {
        if(currentState.GetType() == typeof(Wait) || currentState.GetType() == typeof(TransformCube)){
            SwitchState(typeof(Sleep));
        }
    }

    public void PlayAudioClip(string audioClipName)
    {
        AudioClip audioClip = FindAudioClipByName(audioClipName);
        if (audioClip != null)
        {
            //audioSource.PlayOneShot(audioClip);
            audioSource.Stop();
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else
        {
            //Debug.LogWarning("没有找到 " + audioClipName + " 的音频");
        }
    }

    private AudioClip FindAudioClipByName(string audioClipName)
    {
        foreach (AudioClip audio in playerAudio)
        {
            if (audio != null && audio.name == audioClipName)
            {
                return audio;
            }
        }
        return null;
    }
}