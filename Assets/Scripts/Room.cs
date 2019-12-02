using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Room : MonoBehaviour {

    private Door[] doors;
    private bool cleared;

	// Use this for initialization
	private void Start () {
        doors = GetComponentsInChildren<Door>();
        cleared = false;
        SetupRoom();
    }

    protected abstract void SetupRoom();

    protected abstract void OnTriggerStay(Collider other);

    protected void TryToStartRoom()
    {
        if (!cleared)
        {
            LockDoors();
            if (AreDoorsClosed())
            {
                StartRoom();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player")) {
            UnlockDoors();
        }
    }

    protected abstract void StartRoom();

    protected void ClearRoom()
    {
        cleared = true;
        UnlockDoors();
        ClearDoors();
    }

    protected void OpenDoors()
    {
        foreach (Door d in doors)
        {
            d.OpenDoor();
        }
    }
    private void LockDoors()
    {
        foreach (Door d in doors)
        {
            d.locked = true;
        }
    }
    private void UnlockDoors()
    {
        foreach (Door d in doors)
        {
            d.locked = false;
        }
    }
    private void ClearDoors()
    {
        foreach (Door d in doors)
        {
            d.ClearDoor();
        }
    }
    private bool AreDoorsClosed()
    {
        foreach (Door d in doors)
        {
            if (!d.IsClosed()) { return false; }
        }
        return true;
    }
}
