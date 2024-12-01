using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character/Enemy/EnemyData")]
public class EnemyDataSO : ScriptableObject
{
    // 巡逻时左右移动范围
    public float moveRange = 4;
    // 警戒范围
    public float guardingRange = 10;
    // 每次的停留时间
    public float lookUpTime = 1;
    // 到达目的地前的空余距离
    public float stopDistance = 1;
}
