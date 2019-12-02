using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : Room
{
    protected override void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            TryToStartRoom();
        }
    }

    protected override void SetupRoom()
    {
        ClearRoom();
    }
    protected override void StartRoom()
    {
        //TODO: spawn boss
    }
}

