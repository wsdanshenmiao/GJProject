using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Death", fileName = "Death")]
public class Death : PlayerState
{
    public float fadeInDuration;
    public float fadeOutDruation;
    [SerializeField] private FadeEventSO fadeEvent;

    public override void Enter()
    {
        CheckPillow();
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
            if (GameManager.Instance.worldStates == WorldStates.INSIDE) {
                playerController.transform.position = playerController.prePlayerPos;
                playerData.currentHP = playerData.maxHP;
                GameManager.Instance.worldStates = WorldStates.OUTSIDE;
            }
            else{
                DataManager.Instance.LoadData();
            }
            playerController.StartCoroutine(FadeOut(fadeEvent, fadeOutDruation));
        }
    }

    public override void Exit()
    {
        playerInput.EnableGameplayInputs();
        playerData.isDead = false;
    }

}
