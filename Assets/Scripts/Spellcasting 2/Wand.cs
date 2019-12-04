using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Wand : MonoBehaviour
{

    public GameObject projectile;
    protected float projectileSpeed;
    public float shotDelay; //seconds

    private int id;

    public void SpawnProjectile()
    {
        GameObject proj = Instantiate(projectile, transform.position, transform.rotation, transform);
        proj.GetComponent<Rigidbody>().velocity = projectileSpeed * transform.forward;
    }

    public int GetId()
    {
        return id;
    }

    public bool Equals(Wand other)
    {
        return other.GetId() == id;
    }

}
