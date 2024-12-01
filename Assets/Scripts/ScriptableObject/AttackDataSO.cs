using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Attack/Data")]

public class AttackDataSO : ScriptableObject
{
    public float attackRange;

    public float coolDown;

    public float minDamage;
    public float maxDamage;
}
