using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

public class GlobalVolumeManager : MonoBehaviour
{
    [Header("事件监听")]
    [SerializeField] private VoidEventSO findPlayerEvent;
    [SerializeField] private VoidEventSO lostPlayerEvent;
    [SerializeField] private GameObject findPlayerVolume;
    public Volume volume;

    long enemyCount = 0;

    private void Awake()
    {
        //findPlayerVolume.SetActive(false);
    }

    private void OnEnable()
    {
        findPlayerEvent.onEventRaised += OnEnemyFindPlayerEvent;
        lostPlayerEvent.onEventRaised += OnEnemyLostPlayerEvent;
    }

    private void OnDisable()
    {
        findPlayerEvent.onEventRaised -= OnEnemyFindPlayerEvent;
        lostPlayerEvent.onEventRaised -= OnEnemyLostPlayerEvent;
    }

    private void Update()
    {
        if(GameManager.Instance.worldStates == WorldStates.INSIDE){
            volume.weight = 0.8f;
        }
        else {
            volume.weight = 0;
        }
    }

    private void OnEnemyLostPlayerEvent()
    {
        enemyCount--;
        if(enemyCount <= 0){
            //findPlayerVolume.SetActive(false);
            volume.weight = 0.8f;
        }
    }

    private void OnEnemyFindPlayerEvent()
    {
        volume.weight = 1;
        enemyCount++;
        //findPlayerVolume.SetActive(true);
    }
}
