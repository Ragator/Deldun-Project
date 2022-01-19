using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    [SerializeField] private EnemyStats myEnemyStats;
    [SerializeField] private EnemyAttack myAttack;

    private bool targetHit = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(DeldunProject.Tags.playerHitbox) && !targetHit)
        {
            myEnemyStats.Attack(collision, myAttack);
            targetHit = true;
        }
    }

    private void OnEnable()
    {
        targetHit = false;
    }
}