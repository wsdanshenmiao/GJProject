using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character/CharacterData")]
public class CharacterDataSO : ScriptableObject
{
    [Header("角色属性")]
    // 最大血量
    public float maxHP = 3;
    // 最大走路速度
    public float maxWalkSpeed = 5;
    // 最大跑步速度
    public float maxRunSpeed = 10;
    // 无敌帧
    public float invalidFrame = 0.1f;
    // 防御
    public float defence = 0;
    // 抗击退系数
    public float resistKnockFactor = .2f;
    // 击退系数
    public float knockFactor = 2;
}
