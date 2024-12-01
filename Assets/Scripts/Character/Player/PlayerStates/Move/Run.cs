using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Run", fileName = "Run")]
public class Run : PlayerState
{
    public override void Enter()
    {
        CheckPillow();
        stateStartTime = Time.time;
        currentSpeed = playerController.moveSpeed;
        stateMachine.PlayAudioClip("Run");
    }

    public override void LogicUpdate()
    {
        //Debug.Log("Run");
        if(playerInput.isAttack){
            stateMachine.SwitchState(typeof(AttackRun));
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
        if(playerInput.isStopRun) {
            stateMachine.SwitchState(typeof(Walk));
        }
        if (playerInput.isJump)
        {
            stateMachine.SwitchState(typeof(JumpUpRun));
        }
        if (playerController.isFalling)
        {
            //Debug.Log("切换为掉落状态");
            stateMachine.SwitchState(typeof(RunCoyoteTime));
        }
        // 切换为冲刺状态
        if(playerController.CanSprint)
        {
            stateMachine.SwitchState(typeof(Sprint));
        }

        float acceleration = playerData.maxRunSpeed / playerData.runAccelerateTime;
        currentSpeed = Mathf.MoveTowards(currentSpeed, playerData.maxRunSpeed, acceleration * Time.deltaTime);
    }

    public override void PhysicUpdate()
    {
        playerController.Move(currentSpeed);
    }
}
