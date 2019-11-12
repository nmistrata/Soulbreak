using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    private int openShapeKey = 0;
    public float openTime = 1f;
    private float closedZ = 0f;
    private float openZ = 2.2f;

    private Transform leftDoor;
    private Transform rightDoor;

    private bool cleared;
    public bool locked;
    private bool playerPresent;

    void Start () {
        openShapeKey = 0;
        leftDoor = transform.GetChild(0);
        rightDoor = transform.GetChild(1);
        cleared = false;
        locked = false;
    }

    void Open()
    {
        if (openShapeKey == 100)
        {
            CancelInvoke("Open");
        }
        else
        {
            openShapeKey++;
            leftDoor.localPosition = new Vector3(0, 0, -(closedZ + (openShapeKey / 100f) * (openZ - closedZ)));
            rightDoor.localPosition = new Vector3(0, 0, (closedZ + (openShapeKey / 100f) * (openZ - closedZ)));
        }
    }

    void Close()
    {
        if (openShapeKey == 0)
        {
            CancelInvoke("Close");
        }
        else
        {
            openShapeKey--;
            leftDoor.localPosition = new Vector3(0, 0, -(closedZ + (openShapeKey / 100f) * (openZ - closedZ)));
            rightDoor.localPosition = new Vector3(0, 0, (closedZ + (openShapeKey / 100f) * (openZ - closedZ)));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            playerPresent = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            playerPresent = false;
        }
    }

    private void Update()
    {
        if (!playerPresent && (!cleared || locked))
        {
            CloseDoor();
        }

        if (playerPresent && (!isClosed() || !locked))
        {
            OpenDoor();
        }
    }

    public bool isCleared() { return cleared;  }
    public void clearDoor() {
        cleared = true;
    }

    public bool isClosed()
    {
        return openShapeKey == 0;
    }

    public void CloseDoor()
    {
        if (!IsInvoking("Close") && !playerPresent)
        {
            CancelInvoke("Open");
            InvokeRepeating("Close", 0f, openTime / 100f);
        }
    }

    public void OpenDoor()
    {
        if (!IsInvoking("Open"))
        {
            CancelInvoke("Close");
            InvokeRepeating("Open", 0f, openTime / 100f);
        }
    }
}
