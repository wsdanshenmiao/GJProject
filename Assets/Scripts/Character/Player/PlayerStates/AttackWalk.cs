using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/AttackWalk", fileName = "AttackWalk")]
public class AttackWalk : PlayerState
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
            stateMachine.SwitchState(typeof(JumpUpWalk));
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

        float acceleration = playerData.maxWalkSpeed / playerData.walkAccelerateTime;
        currentSpeed = Mathf.MoveTowards(currentSpeed, playerData.maxWalkSpeed, acceleration * Time.deltaTime);
    }

    public override void PhysicUpdate()
    {
        playerController.Move(currentSpeed);
    }
}
