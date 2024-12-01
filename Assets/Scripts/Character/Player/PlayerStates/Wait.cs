using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Wait", fileName = "Wait")]
public class Wait : PlayerState
{
    public override void Enter()
    {
        CheckPillow();
        stateStartTime = Time.time;
        playerController.SetVelocityX(0f);
    }

    public override void LogicUpdate()
    {
        //Debug.Log("Wait");
        if(playerInput.isAttack){
            stateMachine.SwitchState(typeof(AttackWalk));
        }
        if(CanTransCube()){
            stateMachine.SwitchState(typeof(TransformCube));
        }
        if(playerData.isHurt){
            stateMachine.SwitchState(typeof(Hurt));
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
            //Debug.Log("切换为掉落状态");
            stateMachine.SwitchState(typeof(Fall));
        }
    }
}
