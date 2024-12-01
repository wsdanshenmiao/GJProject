using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/RunCoyoteTime", fileName = "RunCoyoteTime")]
public class RunCoyoteTime : PlayerState
{
    public override void Enter()
    {
        base.Enter();
        currentSpeed = playerController.moveSpeed;
        playerController.rigidBody.gravityScale = 0f;                           // 将重力设为0
    }

    public override void Exit()
    {
        base.Exit();
        playerController.rigidBody.gravityScale = playerController.gravityRatio;                           // 将重力设为1
    }

    public override void LogicUpdate()
    {
        
        if(playerInput.transCubeState){
            stateMachine.SwitchState(typeof(TransformCube));
        }
        if(playerData.isHurt){
            stateMachine.SwitchState(typeof(Hurt));
        }
        if (IsClimp()){
            stateMachine.SwitchState(typeof(Climp));
        }
        if (playerInput.isJump)
        {
            stateMachine.SwitchState(typeof(JumpUpRun));
        }
        // 切换为冲刺状态
        if(playerController.CanSprint)
        {
            stateMachine.SwitchState(typeof(Sprint));
        }

        if(stateDuration >= playerData.runCoyoteTime || !playerInput.isMove)
        {
            stateMachine.SwitchState(typeof(Fall));
        }

        float acceleration = playerData.maxRunSpeed / playerData.runAccelerateTime;
        currentSpeed = Mathf.MoveTowards(currentSpeed, playerData.maxRunSpeed, acceleration * Time.deltaTime);
    }

    public override void PhysicUpdate()
    {
        playerController.Move(currentSpeed);
    }
}
