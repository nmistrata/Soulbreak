using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : Room
{
    public GameObject teleporter;
    private GameObject boss = null;
    private bool active = false;

    protected override void ClearRoom()
    {
        base.ClearRoom();
        teleporter.SetActive(true);

    }

    protected override void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && !active)
        {
            TryToStartRoom();
        }

        if (active)
        {
            if (!boss.activeSelf) { ClearRoom(); OpenDoors(); }
        }
    }

    protected override void SetupRoom()
    {
        return;
    }

    protected override void StartRoom()
    {
        active = true;
        boss.SetActive(true);

    }

    IEnumerator SpawnBoss(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        active = true;
        boss.SetActive(true);
    }

    public virtual void generateBoss(GameObject boss)
    {
        this.boss = Instantiate(boss, transform.position, Quaternion.identity, transform);
        this.boss.SetActive(false);
    }
}

