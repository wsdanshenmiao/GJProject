using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Hurt", fileName = "Hurt")]
public class Hurt : PlayerState
{
    public override void Enter()
    {
        base.Enter();
        animator.SetTrigger("Hurt");
        stateMachine.PlayAudioClip("Hurt");
    }

    public override void LogicUpdate()
    {
        //Debug.Log("Hurt");
        if(CanTransCube()){
            stateMachine.SwitchState(typeof(TransformCube));
        }
        if(!playerInput.isMove){
            stateMachine.SwitchState(typeof(Wait));
        }
        if (IsClimp()){
            stateMachine.SwitchState(typeof(Climp));
        }
        if(playerInput.isMove) {
            stateMachine.SwitchState(typeof(Walk));
        }
        if(playerInput.isRun) {
            stateMachine.SwitchState(typeof(Run));
        }
        if (playerInput.isJump)
        {
            stateMachine.SwitchState(typeof(JumpUpWalk));
        }
        if (playerController.isFalling)
        {
            stateMachine.SwitchState(typeof(Fall));
        }
        animator.SetTrigger("Hurt");
        float acceleration = playerData.maxWalkSpeed / playerData.walkAccelerateTime;
        currentSpeed = Mathf.MoveTowards(currentSpeed, playerData.maxWalkSpeed, acceleration * Time.deltaTime);
    }

    public override void PhysicUpdate()
    {
        playerController.Move(currentSpeed);
    }

}
