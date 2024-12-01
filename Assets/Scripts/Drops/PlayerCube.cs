using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCube : BaseDrops
{
    [Header("事件监听")]
    [SerializeField] VoidEventSO changeWorldStateEvent;

    private void OnEnable()
    {
        changeWorldStateEvent.onEventRaised += RealseThis;
    }

    private void OnDisable()
    {
        changeWorldStateEvent.onEventRaised -= RealseThis;
    }

    private void RealseThis()
    {
        if(GameManager.Instance.worldStates == WorldStates.OUTSIDE){
            m_DropsPool.Release(this);
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
    }
}
