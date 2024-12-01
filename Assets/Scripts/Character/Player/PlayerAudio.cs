using System.CodeDom;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public AudioDefinition jump;
    public AudioDefinition walk;
    public AudioDefinition run;
    public AudioDefinition sprint;
    public AudioDefinition climp;
    public AudioDefinition cube;

    private PlayerStateMachine stateMachine;

    private void Awake()
    {
        stateMachine = GetComponentInParent<PlayerStateMachine>();
    }

    // Update is called once per frame
    void Update()
    {
        var currType = stateMachine.currentState.GetType();
        if(!(sprint.enabled = currType == typeof(Sprint))){
            jump.enabled =  currType == (typeof(JumpUpRun)) || currType == (typeof(JumpUpWalk));
            walk.enabled = currType == typeof(Walk);
            run.enabled = currType == typeof(Run);
            climp.enabled = currType == typeof(Climp);
            cube.enabled = currType == typeof(TransformCube);
        }
    }
}
