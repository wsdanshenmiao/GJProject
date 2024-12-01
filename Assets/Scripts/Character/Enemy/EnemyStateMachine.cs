using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : StateMachine
{
    [SerializeField] EnemyState[] enemyStates;

    Animator animator;

    EnemyController enemyController;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();

        enemyController = GetComponent<EnemyController>();

        stateTable = new Dictionary<System.Type, IState>(enemyStates.Length);

        foreach (EnemyState state in enemyStates)
        {
            state.Initialize(animator, enemyController, this);
            stateTable.Add(state.GetType(), state);
        }
    }

    void Start()
    {
        
    }
}