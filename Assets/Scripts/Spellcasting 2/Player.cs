using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private const float MAX_HEALTH = 100f;
    private float health;

    private List<Wand> wands;
    private int curWand;
    private float timeSinceLastShot;

    private float shotDelay;

    private void Awake()
    {
        GameManager.player = gameObject;
    }

    private void Start()
    {
        health = MAX_HEALTH;
        timeSinceLastShot = 0f;
        wands = new List<Wand>();
        //TODO add starting wand
        curWand = 0;
        shotDelay = wands[curWand].GetComponent<Wand>().shotDelay;
    }

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;
        if (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger) && timeSinceLastShot > shotDelay)
        {
            timeSinceLastShot = 0;
            FireProjectile();
        }
    }

    public void AddWand(Wand wand)
    {
        wands.Add(wand);
    }

    public void CycleWandsForward()
    {
        curWand = (curWand + 1) % wands.Count;
        shotDelay = wands[curWand].GetComponent<Wand>().shotDelay;
    }

    public void CycleWandsBackwards()
    {
        curWand = (curWand + wands.Count - 1) % wands.Count;
        shotDelay = wands[curWand].GetComponent<Wand>().shotDelay;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            //TODO gameover
        }
    }

    public void FireProjectile()
    {
        wands[curWand].GetComponent<Wand>().SpawnProjectile();
    }
}
