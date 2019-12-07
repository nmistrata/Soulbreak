using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    public float damage = 5; //dps

    override public void Attack()
    {
        player.GetComponent<Player>().TakeDamage(damage * Time.deltaTime);
    }
}
