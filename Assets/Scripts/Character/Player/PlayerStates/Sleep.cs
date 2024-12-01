using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Sleep", fileName = "Sleep")]
public class Sleep : PlayerState
{
    public float fadeInDuration;
    public float fadeOutDruation;
    public PlayerCube playerCube;
    [SerializeField] private FadeEventSO fadeEvent;

    public override void Enter()
    {
        CheckPillow();
        stateStartTime = Time.time;
        // 切换时不能操作
        playerInput.DisableGameplayInputs();
        finishFadeIn = false;
        finishFadeOut = false;
        playerController.StartCoroutine(FadeIn(fadeEvent, fadeInDuration));
    }

    public override void LogicUpdate()
    {
        var info = animator.GetCurrentAnimatorStateInfo(0);
        if (info.normalizedTime < 1) return;
        // 播放完毕后淡入淡出
        if(!finishFadeIn && finishFadeOut){
            stateMachine.SwitchState(typeof(Wait));
        }
        if(finishFadeIn && !finishFadeOut){
            finishFadeIn = false;
            Vector3 pos = playerController.transform.position;
            if(GameManager.Instance.worldStates == WorldStates.INSIDE){ // 从表到里
                playerController.prePlayerPos = pos;
                playerController.transform.position = 
                    new Vector3(pos.x + GameManager.Instance.twoWorldDistance, pos.y, pos.z);
            }
            else{
                playerController.transform.position = playerController.prePlayerPos;
            }
            playerController.StartCoroutine(FadeOut(fadeEvent, fadeOutDruation));
        }
    }

    public override void Exit()
    {
        playerInput.EnableGameplayInputs();
    }


}
