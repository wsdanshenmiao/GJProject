using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public IState currentState;
    protected AudioSource audioSource;
    protected Dictionary<System.Type, IState> stateTable;
    protected Dictionary<System.Type, AudioClip> audioTable;

    void Update()
    {
        Debug.Log(currentState.GetType().Name);
        currentState.LogicUpdate();
    }

    void FixedUpdate()
    {
        currentState.PhysicUpdate();
    }

    protected void SwitchOn(IState newState)
    {
        currentState = newState;
        currentState.Enter();
    }

    public void SwitchState(IState newState)
    {
        currentState.Exit();
        SwitchOn(newState);
    }

    public void SwitchState(System.Type newStateType)
    {
        IState state;
        if (!stateTable.TryGetValue(newStateType, out state)) return;
        SwitchState(state);
    }

    public void AddState(System.Type key, IState state)
    {
        if(key == typeof(Attack)){
            Player.Instance.GetComponent<PlayerController>().enablePillow = true;
        }
        stateTable.TryAdd(key, state);
    }

    // 控制音乐
    
    
    

    /*
    public void PlayAudioClip(System.Type audioClipType)
    {
        AudioClip audioClip;
        if (!audioTable.TryGetValue(audioClipType, out audioClip)) return;
        PlayAudioClip(audioClip);
    }
    

    public void PlayAudioClip(AudioClip audioClip)
    {
        audioSource.Stop();
        audioSource.clip = audioClip;
        audioSource.Play();
    }
    */
}
