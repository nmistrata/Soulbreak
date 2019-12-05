using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wand : MonoBehaviour
{

    public GameObject projectile;
    public float projectileSpeed;
    public float shotDelay; //seconds
    public float WAND_HEIGHT = 0;

    private int id;

    public void SpawnProjectile()
    {
        GameObject proj = Instantiate(projectile, transform.position + transform.up*WAND_HEIGHT, transform.rotation);
        proj.transform.SetParent(null);
        proj.GetComponent<Rigidbody>().velocity = projectileSpeed * transform.up;
    }

    public int GetId()
    {
        return id;
    }

    public bool Equals(Wand other)
    {
        return other.GetId() == id;
    }

    public void Activate()
    {
        gameObject.SetActive(false);
    }

    public void Deactivate()
    {
        gameObject.SetActive(true);
    }

}
