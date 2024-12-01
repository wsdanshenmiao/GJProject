using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Walk", fileName = "Walk")]
public class Walk : PlayerState
{
    public override void Enter()
    {
        CheckPillow();
        stateStartTime = Time.time;
        currentSpeed = playerController.moveSpeed;
        stateMachine.PlayAudioClip("Walk");
    }

    public override void LogicUpdate()
    {
        //Debug.Log("Walk");
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
        if(!playerInput.isMove) {
            stateMachine.SwitchState(typeof(Wait));
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
            stateMachine.SwitchState(typeof(WalkCoyoteTime));
        }
        // 切换为冲刺状态
        if(playerController.CanSprint)
        {
            stateMachine.SwitchState(typeof(Sprint));
        }
        float acceleration = playerData.maxWalkSpeed / playerData.walkAccelerateTime;
        currentSpeed = Mathf.MoveTowards(currentSpeed, playerData.maxWalkSpeed, acceleration * Time.deltaTime);
    }

    public override void PhysicUpdate()
    {
        playerController.Move(currentSpeed);
    }
}
