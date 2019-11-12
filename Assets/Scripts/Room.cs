using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

    private Door[] doors;
    private bool cleared;

	// Use this for initialization
	void Start () {
        doors = GetComponentsInChildren<Door>();
        cleared = false;
    }


    private void OnTriggerStay(Collider other)
    {
        if (!other.gameObject.tag.Equals("Player")) {return;}

        if (!cleared)
        {
            LockDoors();
            if (AreDoorsClosed())
            {
                startRoom();
            }

            if (OVRInput.GetDown(OVRInput.Button.One)) //DEBUGGING PURPOSES
            {
                clearRoom();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.tag.Equals("Player")) { return; }
        UnlockDoors();
    }

    public void startRoom()
    {
    }

    public void clearRoom()
    {
        cleared = true;
        UnlockDoors();
        ClearDoors();
        OpenDoors();
    }

    public void OpenDoors()
    {
        foreach (Door d in doors)
        {
            d.OpenDoor();
        }
    }
    public void LockDoors()
    {
        foreach (Door d in doors)
        {
            d.locked = true;
        }
    }
    public void UnlockDoors()
    {
        foreach (Door d in doors)
        {
            d.locked = false;
        }
    }
    public void ClearDoors()
    {
        foreach (Door d in doors)
        {
            d.clearDoor();
        }
    }

    private bool AreDoorsClosed()
    {
        foreach (Door d in doors)
        {
            if (!d.isClosed()) { return false; }
        }
        return true;
    }
}
