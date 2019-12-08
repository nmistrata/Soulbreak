using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    public float baseDamage = 10f; //dps
    private float damage = 10f;
    public float damagePerLevel = 1f;

    override public void Attack()
    {
        player.GetComponent<Player>().TakeDamage(damage * Time.deltaTime);
    }

    override public void SetLevel(int level)
    {
        maxHealth = baseHealth + (level - 1) * healthPerLevel;
        damagePerLevel = baseDamage + (level - 1) * damagePerLevel;
    }
}
