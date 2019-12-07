using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private const float MAX_HEALTH = 100f;
    private float health;

    public List<Wand> wands;
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
        startingWand.SetActive(true);
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

        if (OVRInput.GetDown(OVRInput.Button.Four))
        {
            Debug.Log("swapping forward");
            CycleWandsForward();
        }

        if (OVRInput.GetDown(OVRInput.Button.Three))
        {
            Debug.Log("swapping backwards");
            CycleWandsBackwards();
        }

    }

    public void AddWand(GameObject wand)
    {
        wand.transform.parent = wandContainer;
        wand.transform.localPosition = Vector3.zero;
        wand.transform.localScale = Vector3.one;
        wand.transform.localRotation = Quaternion.identity;
        wand.SetActive(false);
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
        wands[curWand].gameObject.SetActive(false);
        curWand = wandIndex;
        wands[curWand].gameObject.SetActive(true);
    }

    public void TakeDamage(float damage)
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
