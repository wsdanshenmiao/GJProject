using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Land", fileName = "Land")]
public class Land : PlayerState
{
    public override void Enter()
    {
        CheckPillow();
        stateStartTime = Time.time;
    }

    public override void LogicUpdate()
    {
        //Debug.Log("着陆");
        if(CanTransCube()){
            stateMachine.SwitchState(typeof(TransformCube));
        }
        playerController.canAirJump = true;
        if(playerData.isHurt){
            stateMachine.SwitchState(typeof(Hurt));
        }
        if (playerController.hasJumpInputBuffer || playerInput.isJump)
        {
            stateMachine.SwitchState(typeof(JumpUpWalk));
        }
        if (IsClimp()){
            stateMachine.SwitchState(typeof(Climp));
        }
        if(playerInput.isRun) {
            stateMachine.SwitchState(typeof(Run));
        }
        if(playerInput.isMove) {
            stateMachine.SwitchState(typeof(Walk));
        }
        if(playerInput.isRun) {
            stateMachine.SwitchState(typeof(Run));
        }
        // 切换为冲刺状态
        if(playerController.CanSprint)
        {
            stateMachine.SwitchState(typeof(Sprint));
        }
        // 动画结束后切换回等待状态
        if(playerController.isGrounded){
            stateMachine.SwitchState(typeof(Wait));
        }
    }
}
