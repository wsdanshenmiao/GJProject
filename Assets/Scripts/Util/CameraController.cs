using UnityEngine;
using Cinemachine;
using System;

public class CameraController : MonoBehaviour
{
    [Header("事件监听")]
    [SerializeField]VoidEventSO afterSceneLoad;
    [SerializeField] private VoidEventSO shakeEvent;
    [SerializeField] private VoidEventSO newGameEvent;
    
    [SerializeField]private CinemachineImpulseSource impulseSource;

    private CinemachineConfiner2D confiner2D;
    private CinemachineVirtualCamera cinemachineVirtualCamera;

    private void Awake()
    {
        confiner2D = GetComponent<CinemachineConfiner2D>();
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    private void OnEnable()
    {
        shakeEvent.onEventRaised += OnShakeEvent;
        afterSceneLoad.onEventRaised += AfterSceneLoadEvent;
        newGameEvent.onEventRaised += FocusPlayer;
    }

    private void Start()
    {
        GetNewCamaraBound();
    }

    private void OnDisable()
    {
        shakeEvent.onEventRaised -= OnShakeEvent;
        afterSceneLoad.onEventRaised -= AfterSceneLoadEvent;
        newGameEvent.onEventRaised -= FocusPlayer;
    }

    private void FocusPlayer()
    {
        cinemachineVirtualCamera.Follow = Player.Instance.transform;
        cinemachineVirtualCamera.LookAt = Player.Instance.transform;
    }

    private void AfterSceneLoadEvent()
    {
        GetNewCamaraBound();
    }

    private void OnShakeEvent()
    {
        impulseSource.GenerateImpulse();
    }

    // 获取新的相机边界
    private void GetNewCamaraBound()
    {
        GameObject obj;
        if(GameManager.Instance.worldStates == WorldStates.OUTSIDE){
            obj = GameObject.FindGameObjectWithTag("CameraBoundOutSide");
        }
        else{
            obj = GameObject.FindGameObjectWithTag("CameraBoundInSide");        
        }
        if (!obj) return;
        confiner2D.m_BoundingShape2D = obj.GetComponent<Collider2D>();
        // 清除先前的缓存
        confiner2D.InvalidateCache();
    }

}
