using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/WalkCoyoteTime", fileName = "WalkCoyoteTime")]
public class WalkCoyoteTime : PlayerState
{
    public override void Enter()
    {
        base.Enter();
        
        currentSpeed = playerController.moveSpeed;
        playerController.rigidBody.gravityScale = 0f;  // 将重力设为0
    }

    public override void Exit()
    {
        base.Exit();
        playerController.rigidBody.gravityScale = playerController.gravityRatio;                           // 将重力设为1
    }

    public override void LogicUpdate()
    {
        //Debug.Log("Walk");
        if(playerInput.transCubeState){
            stateMachine.SwitchState(typeof(TransformCube));
        }
        if(playerData.isHurt){
            stateMachine.SwitchState(typeof(Hurt));
        }
        if (IsClimp()){
            stateMachine.SwitchState(typeof(Climp));
        }
        if(playerInput.isRun) {
            stateMachine.SwitchState(typeof(Run));
        }
        if (playerInput.isJump)
        {
            stateMachine.SwitchState(typeof(JumpUpWalk));
        }
        if(stateDuration >= playerData.walkCoyoteTime || !playerInput.isMove)
        {
            stateMachine.SwitchState(typeof(Fall));
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
