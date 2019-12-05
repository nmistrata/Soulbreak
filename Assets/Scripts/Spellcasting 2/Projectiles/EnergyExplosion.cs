using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyExplosion : Projectile {

    public GameObject explosionProjectile;
    private const float EXPLOSION_VELOCITY = 16f;
    private const int NUM_SHRAPNEL = 8*6;

	// Use this for initialization
	void Start () {
        damage = 0;
        isFriendly = true;
	}

    protected override void OnTriggerEnter(Collider collider)
    {
        GameObject other = collider.gameObject;
        if (other.tag == "Projectile" || other.tag == "Invisible" || (other.tag == "Player" && isFriendly))
        {
            return;
        }

        Explode();
        base.OnTriggerEnter(collider);
    }

    private void Explode()
    {

        GameObject[] projectiles = new GameObject[NUM_SHRAPNEL];

        for (int i  = 0; i < NUM_SHRAPNEL; i++)
        {
            projectiles[i] = Instantiate(explosionProjectile, transform.position, Quaternion.identity, null);
        }

        int index = 0;
        for (int x = -1; x <= 1; x+=2)
        {
            for (int y = -1; y <= 1; y += 2)
            {
                for (int z = -1; z <= 1; z += 2)
                {
                    for (int i = 0; i < NUM_SHRAPNEL / 8; i++)
                    {
                        Vector3 velocity = new Vector3(x * Random.Range(0f, 1f), y * Random.Range(0f, 1f), z * Random.Range(0f, 1f));
                        projectiles[index].GetComponent<Rigidbody>().velocity = velocity.normalized * EXPLOSION_VELOCITY;
                        index++;
                    }
                }
            }
        }
    }
}
