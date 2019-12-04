using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyExplosion : Projectile {

    public GameObject explosionProjectile;
    private const float EXPLOSION_VELOCITY = 3f;

	// Use this for initialization
	void Start () {
        damage = 0;
        isFriendly = false;
	}

    private void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.gameObject;
        if (other.tag == "Projectile" || other.tag == "Player" && isFriendly)
        {
            return;
        }

        Explode();

        base.OnCollisionEnter(collision);
    }

    private void Explode()
    {
        GameObject explosion = new GameObject("Explosion");

        GameObject[] projectiles = new GameObject[32];

        for (int i  = 0; i < 32; i++)
        {
            projectiles[i] = Instantiate(explosionProjectile, transform.position, Quaternion.identity, explosion.transform);
        }

        int index = 0;
        for (int x = -1; x <= 1; x+=2)
        {
            for (int y = -1; y <= 1; y += 2)
            {
                for (int z = -1; z <= 1; z += 2)
                {
                    Vector3 velocity = new Vector3(x * Random.Range(0f, 1f), y * Random.Range(0f, 1f), z * Random.Range(0f, 1f));
                    projectiles[index].GetComponent<Rigidbody>().velocity = velocity.normalized * EXPLOSION_VELOCITY;
                    index++;
                }
            }
        }
    }
}
