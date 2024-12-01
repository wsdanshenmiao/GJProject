using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class PlayerInput : MonoBehaviour
{
    [Header("事件广播")]
    [SerializeField] private VoidEventSO changeWorldStateEvent;

    PlayerInputControl playerInputActions;

    // 获取输入系统的各个值
    Vector2 move => playerInputActions.GamePlay.Move.ReadValue<Vector2>();
    //public float hasJumpInputBufferTime = 0.5f;   // 预输入的持续时间
    //WaitForSeconds waitJumpBufferTime;            // 配合预输入等待时间的协程
    public float moveX => move.x;

    //public bool hasJumpInputBuffer{ get; set; }   // 跳跃预输入
    public bool isJump => playerInputActions.GamePlay.Jump.WasPressedThisFrame();
    public bool isStopJump => playerInputActions.GamePlay.Jump.WasReleasedThisFrame();
    
    public bool isMove => moveX != 0f;

    public bool isRun => playerInputActions.GamePlay.Run.IsPressed() && isMove;
    public bool isStopRun => playerInputActions.GamePlay.Run.WasReleasedThisFrame();
    public bool isSprint => playerInputActions.GamePlay.Sprint.IsPressed();

    // 能够攀爬
    [HideInInspector]public bool canClimp = false;
    public float climpDir => playerInputActions.GamePlay.Climp.ReadValue<float>();

    public bool isChangeSize => playerInputActions.GamePlay.ChangeSize.WasPressedThisFrame();

    public bool transCubeState => playerInputActions.GamePlay.TransformCube.WasPressedThisFrame();

    public bool isConfirm => playerInputActions.GamePlay.Confirm.WasPerformedThisFrame();

    public bool isAttack => playerInputActions.GamePlay.Attack.WasPressedThisFrame();

    void Awake(){
        playerInputActions = new PlayerInputControl();
        //waitJumpBufferTime = new WaitForSeconds(hasJumpInputBufferTime);
    }

    void OnEnable()
    {
        playerInputActions.GamePlay.ChangeStats.performed += ChangePlayerStats;
    }

    private void OnDisable()
    {
        playerInputActions.GamePlay.ChangeStats.performed -= ChangePlayerStats;
    }

    private void ChangePlayerStats(InputAction.CallbackContext context)
    {
        if (!(context.interaction is HoldInteraction)) return;
        changeWorldStateEvent.onEventRaised?.Invoke();
        switch(GameManager.Instance.worldStates){
            case WorldStates.INSIDE:{
                    GameManager.Instance.worldStates = WorldStates.OUTSIDE; break;
            }
            case WorldStates.OUTSIDE:{
                    GameManager.Instance.worldStates = WorldStates.INSIDE; break;
            }
        }
    }


    public void EnableGameplayInputs()
    {
        playerInputActions.GamePlay.Enable();
    }

    public void DisableGameplayInputs()
    {
        playerInputActions.GamePlay.Disable();
    }


}


/*
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    PlayerInputControl playerInputActions;

    // 获取输入系统的各个值
    Vector2 move => playerInputActions.GamePlay.Move.ReadValue<Vector2>();
    public float moveX => move.x;

    public bool hasJumpInputBuffer{ get; set; }
    public bool isJump => playerInputActions.GamePlay.Jump.WasPressedThisFrame();
    public bool isStopJump => playerInputActions.GamePlay.Jump.WasReleasedThisFrame();
    
    public bool isMove => moveX != 0f;

    public bool isRun => playerInputActions.GamePlay.Run.IsPressed() && isMove;
    public bool isStopRun => playerInputActions.GamePlay.Run.WasReleasedThisFrame();
    public bool isSprint => playerInputActions.GamePlay.Sprint.IsPressed();

    // 能够攀爬
    [HideInInspector]public bool canClimp = false;
    public float climpDir => playerInputActions.GamePlay.Climp.ReadValue<float>();

    public bool isChangeSize => playerInputActions.GamePlay.ChangeSize.WasPressedThisFrame();

    void Awake(){
        playerInputActions = new PlayerInputControl();
    }

    public void EnableGameplayInputs()
    {
        playerInputActions.GamePlay.Enable();
    }

    public void DisableGameplayInputs()
    {
        playerInputActions.GamePlay.Disable();
    }
}
*/