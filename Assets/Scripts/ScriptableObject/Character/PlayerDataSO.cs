using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character/Player/PlayerData")]
public class PlayerDataSO : ScriptableObject
{
    [Header("玩家移动")]
    // 加速到最大走路速度所耗时间
    public float walkAccelerateTime = 1;
    // 加速到最大跑步速度所耗时间
    public float runAccelerateTime = 1;
    // 跳跃高度
    public float jumpHeight = 5;
    // 上升时间
    public float jumpTime = 1;
    // 下落时间
    public float downTime = 1;
    // 空中移动速度
    public float walkSkySpeed = 5;
    // 空中跑步速度
    public float runSkySpeed = 6;
    // 冲刺距离
    public float sprintDistance = 5;
    // 冲刺cd时长
    public float sprintCD = 5;
    // 冲刺时间
    public float sprintDurationTime = 1;
    // 二段跳高度
    public float doubleJumpHeight = 5;
    // 攀爬速度
    public float climpSpeed = 10;
    // 攀爬横向速度
    public float climpHorizontalSpeed = 10;
    [Header("土狼跳")]
    public float runCoyoteTime = 0.15f;
    public float walkCoyoteTime = 0.15f;
    [Header("预输入的持续时间")]
    public float hasJumpInputBufferTime = 0.5f;
    [Header("角色大小")]
    // 正常大小
    public float normalSize = 6;
    // 大形态角色大小
    public float bigSize = 9;
    // 小形态角色大小
    public float smallSize = 4;
    // 角色变成方块后的大小
    public float cubeSize = 1;
}
