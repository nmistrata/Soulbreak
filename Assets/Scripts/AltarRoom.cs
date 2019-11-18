using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltarRoom : Room
{
    protected override void OnTriggerStay(Collider other)
    {
        return;
    }

    protected override void SetupRoom()
    {
        ClearRoom();
    }
    protected override void StartRoom()
    {
        return;
    }
}

