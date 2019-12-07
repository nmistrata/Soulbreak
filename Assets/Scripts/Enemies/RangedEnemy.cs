using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy {

    public GameObject projectile;
    public float shotDelay;
    public float projectileSpeed;
    public Vector3 projectileSpawnOffset;
    private float timeSinceLastAttack;

    override protected void Start()
    {
        base.Start();
        timeSinceLastAttack = 0;
    }

    override protected void Update()
    {
        base.Update();
        timeSinceLastAttack += Time.deltaTime;
    }

    override public void Attack()
    {
        if (timeSinceLastAttack > shotDelay*1.5f) //stops instant attacks after getting in attack stance
        {
            timeSinceLastAttack = 0;
        }
        if (timeSinceLastAttack > shotDelay)
        {
            SpawnProjectile();
            timeSinceLastAttack = 0;
        }
    }

    private void SpawnProjectile()
    {
        GameObject proj = Instantiate(projectile, transform.TransformPoint(projectileSpawnOffset), transform.rotation);
        Vector3 launchDirection = (player.position - proj.transform.position).normalized;
        proj.transform.SetParent(null);
        proj.GetComponent<Rigidbody>().velocity = projectileSpeed * launchDirection;
    }
}
