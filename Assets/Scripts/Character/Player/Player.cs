using System;
using UnityEngine;

public class Player : Character
{
    private static Player instance;
    public static Player Instance {  get { return instance; } }

    [SerializeField] private PlayerDataSO templatePlayerData;
    [SerializeField] private PlayerDataSO playerData;

    public float currInSideWorldHP = 0;

    public static bool IsInitialized()
    {
        return instance != null;
    }

    protected override void Awake()
    {
        base.Awake();
        if (instance != null) {
            Destroy(gameObject);
        }
        else {
            instance = (Player)this;
        }
    }

    protected virtual void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }

    override protected void OnNewGameEvent() 
    {
        base.OnNewGameEvent();
        if(!playerData){
            ResetData();
        }
        else{
            currInSideWorldHP = maxHP;
        }
    }

    private void ResetData()
    {
        playerData = Instantiate(templatePlayerData);
        currInSideWorldHP = maxHP;
    }

    public float GetPlayerMaxHealth()
    {
        return maxHP;
    }

    public override float currentHP { 
        get => GameManager.Instance.worldStates == WorldStates.OUTSIDE ? base.currentHP : currInSideWorldHP; 
        set {   if (GameManager.Instance.worldStates == WorldStates.OUTSIDE) base.currentHP = value;
                else currInSideWorldHP = value;
                onHealthChangeEvent.Invoke(this);
        }
    }

    #region Read From PlayerDataSO
    // 加速到最大走路速度所耗时间
    public float walkAccelerateTime {
        get{ return playerData ? playerData.walkAccelerateTime : 0; }
        set{ playerData.walkAccelerateTime = value; }
    }
    // 加速到最大跑步速度所耗时间
    public float runAccelerateTime {
        get{ return playerData ? playerData.runAccelerateTime : 0; }
        set{ playerData.runAccelerateTime = value; }
    }
    public float jumpHeight {
        get{ return playerData ? playerData.jumpHeight : 0; }
        set{ playerData.jumpHeight = value; }
    }
    public float jumpTime {
        get{ return playerData ? playerData.jumpTime : 0; }
        set{ playerData.jumpTime = value; }
    }
    public float downTime {
        get{ return playerData ? playerData.downTime : 0; }
        set{ playerData.downTime = value; }
    }
    public float walkSkySpeed {
        get{ return playerData ? playerData.walkSkySpeed : 0; }
        set{ playerData.walkSkySpeed = value; }
    }
    public float runSkySpeed {
        get{ return playerData ? playerData.runSkySpeed : 0; }
        set{ playerData.runSkySpeed = value; }
    }
    public float sprintDistance {
        get{ return playerData ? playerData.sprintDistance : 0; }
        set{ playerData.sprintDistance = value; }
    }
    public float sprintCD {
        get{ return playerData ? playerData.sprintCD : 0; }
        set{ playerData.sprintCD = value; }
    }
    public float doubleJumpHeight {
        get{ return playerData ? playerData.doubleJumpHeight : 0; }
        set{ playerData.doubleJumpHeight = value; }
    }
    // 攀爬速度
    public float climpSpeed {
        get{ return playerData ? playerData.climpSpeed : 0; }
        set{ playerData.climpSpeed = value; }
    }
    // 攀爬横向速度
    public float climpHorizontalSpeed {
        get{ return playerData ? playerData.climpHorizontalSpeed : 0; }
        set{ playerData.climpHorizontalSpeed = value; }
    }
    public float normalSize {
        get{ return playerData ? playerData.normalSize : 0; }
        set{ playerData.normalSize = value; }
    }
    public float bigSize {
        get{ return playerData ? playerData.bigSize : 0; }
        set{ playerData.bigSize = value; }
    }
    public float smallSize {
        get{ return playerData ? playerData.smallSize : 0; }
        set{ playerData.smallSize = value; }
    }
    public float sprintDurationTime {
        get{ return playerData ? playerData.sprintDurationTime : 0; }
        set{ playerData.sprintDurationTime = value; }
    }
    public float cubeSize {
        get{ return playerData ? playerData.cubeSize : 0; }
        set{ playerData.cubeSize = value; }
    }
    public float walkCoyoteTime {
        get{ return playerData ? playerData.walkCoyoteTime : 0; }
        set{ playerData.walkCoyoteTime = value; }
    }
    public float runCoyoteTime {
        get{ return playerData ? playerData.runCoyoteTime : 0; }
        set{ playerData.runCoyoteTime = value; }
    }
    public float hasJumpInputBufferTime {
        get{ return playerData ? playerData.runCoyoteTime : 0; }
        set{ playerData.runCoyoteTime = value; }
    }
    #endregion

}
