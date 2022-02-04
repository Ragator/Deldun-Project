using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : CharacterStats
{
    [SerializeField] private GameObject healthBarCanvas;

    [SerializeField] private int baseDamage;

    [SerializeField] private int currencyValue = 5;

    protected override void Awake()
    {
        base.Awake();

        InitStatBar(maxHealth, ref currentHealth, healthBar);
    }

    public void Attack(Collider2D collision, EnemyAttack attack)
    {
        int attackDamage = Mathf.CeilToInt(baseDamage * (attack.damagePercent / 100));
        CharacterStats player = collision.GetComponent<CharacterHitbox>().myCharacterStats;

        player.TakeDamage(attackDamage, attack.type);

        player.gameObject.GetComponent<Rigidbody2D>().AddForce((collision.transform.position - transform.position).normalized * attack.knockbackStrength);

        // Knockback away from the player
        // myRigidbody.AddForce((transform.position - playerCollider.transform.position).normalized * selfKnockbackStrength);
    }

    public override void TakeDamage(int damage, DamageType type)
    {
        base.TakeDamage(damage, type);

        healthBarCanvas.SetActive(true);
    }

    protected override void Die()
    {
        base.Die();

        GameObject.FindWithTag(DeldunProject.Tags.gameManager).GetComponent<GameManager>().GainCurrency(currencyValue);

        Destroy(gameObject);
    }
}