using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private const float MAX_HEALTH = 100f;
    private float health;

    private List<Wand> wands;
    private int curWand;
    private float timeSinceLastShot;

    private Transform wandContainer;

    private void Awake()
    {
        GameManager.player = gameObject;
    }

    private void Start()
    {
        health = MAX_HEALTH;
        timeSinceLastShot = 0f;
        wands = new List<Wand>();

        GameObject startingWand = GetComponentInChildren<Wand>().gameObject;
        wandContainer = startingWand.transform.parent;
        AddWand(startingWand);
        curWand = 0;
    }

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;
        if (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger) && timeSinceLastShot > wands[curWand].shotDelay)
        {
            timeSinceLastShot = 0;
            FireProjectile();
        }
    }

    public void AddWand(GameObject wand)
    {
        wands.Add(wand.GetComponent<Wand>());
    }

    public void CycleWandsForward()
    {
        SwapWands((curWand + 1) % wands.Count);
    }

    public void CycleWandsBackwards()
    {
        SwapWands((curWand + wands.Count - 1) % wands.Count);
    }

    private void SwapWands(int wandIndex)
    {
        wands[curWand].Deactivate();
        curWand = wandIndex;
        wands[curWand].Activate();
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
        wands[curWand].SpawnProjectile();
    }
}
