using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New EnemyAttack", menuName = "Attacks/EnemyAttack")]
public class EnemyAttack : ScriptableObject
{
    public string animationTrigger;

    public DamageType type;

    public int damagePercent;
    public int knockbackStrength;

    public float range;

    public bool stopsMoving;
    public bool stopsTurning;
}
