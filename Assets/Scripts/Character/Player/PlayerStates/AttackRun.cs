using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/AttackRun", fileName = "AttackRun")]
public class AttackRun : PlayerState
{
    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        if(playerData.isHurt){
            stateMachine.SwitchState(typeof(Hurt));
        }
        if (playerInput.isJump)
        {
            stateMachine.SwitchState(typeof(JumpUpRun));
        }
        if(playerController.CanSprint)
        {
            stateMachine.SwitchState(typeof(Sprint));
        }

        var info = animator.GetCurrentAnimatorStateInfo(0);
        if (info.normalizedTime < 1) return;
        if(!playerInput.isMove){
            stateMachine.SwitchState(typeof(Wait));
        }
        if(playerInput.isRun){
            stateMachine.SwitchState(typeof(Run));
        }
        if(playerInput.isMove){
            stateMachine.SwitchState(typeof(Walk));
        }

        float acceleration = playerData.maxRunSpeed / playerData.runAccelerateTime;
        currentSpeed = Mathf.MoveTowards(currentSpeed, playerData.maxRunSpeed, acceleration * Time.deltaTime);
    }

    public override void PhysicUpdate()
    {
        playerController.Move(currentSpeed);
    }
}
