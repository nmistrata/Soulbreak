using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wand : MonoBehaviour
{

    public GameObject projectile;
    public float projectileSpeed;
    public float shotDelay; //seconds
    public const float WAND_HEIGHT = .5f;

    public float damageMultiplierPerLevel = 1.2f;
    public float delayMultiplierPerLevel = .8f;

    private float damageMultiplier = 1f;

    private int level = 1;

    public int id;

    private AudioSource audioSource;
    public AudioClip shootSound;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = shootSound;
    }

    public void SpawnProjectile()
    {
        GameObject proj = Instantiate(projectile, transform.position + transform.up*WAND_HEIGHT, Quaternion.LookRotation(transform.up, transform.forward), null);
        proj.GetComponent<Projectile>().MultiplyDamage(damageMultiplier);
        proj.GetComponent<Rigidbody>().velocity = projectileSpeed * transform.up;
        audioSource.Play();
    }

    public int GetId()
    {
        return id;
    }

    public bool Equals(Wand other)
    {
        return other.GetId() == id;
    }

    public void LevelUp()
    {
        damageMultiplier *= damageMultiplierPerLevel;
        shotDelay *= delayMultiplierPerLevel;
        level++;
    }

    public int GetLevel()
    {
        return level;
    }
}
