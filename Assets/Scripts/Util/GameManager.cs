using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public WorldStates worldStates;
    public long twoWorldDistance = 10000;
    [HideInInspector] public bool endGame = false;
    [SerializeField] private AudioDefinition BGM;

    [Header("事件监听")]
    [SerializeField] private VoidEventSO playerDeathEvent;

    private void OnEnable()
    { 
        playerDeathEvent.onEventRaised += OnPlayerDeath;
    }

    private void Start()
    {
        worldStates = WorldStates.OUTSIDE;
        BGM.enabled = false;
        BGM.enabled = true;
    }

    private void Update()
    {
    }

    private void OnDisable()
    { 
        playerDeathEvent.onEventRaised -= OnPlayerDeath;
    }

    /// <summary>
    /// 玩家死亡事件回调
    /// </summary>
    private void OnPlayerDeath()
    {
        if(worldStates == WorldStates.OUTSIDE){
            Player.Instance.GetComponent<PlayerStateMachine>().SwitchState(typeof(Death));
        }
        else{
            Player.Instance.GetComponent<PlayerStateMachine>().SwitchState(typeof(Death));
        }
    }


}
