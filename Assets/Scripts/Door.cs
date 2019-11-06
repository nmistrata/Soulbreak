using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    private int openShapeKey = 0;
    private SkinnedMeshRenderer mesh;
    public float openTime = 2f;
    private float closedZ = 0f;
    private float openZ = 2.2f;

    private Transform leftDoor;
    private Transform rightDoor;

    // Use this for initialization
    void Start () {
        openShapeKey = 0;
        mesh = GetComponent<SkinnedMeshRenderer>();
        leftDoor = transform.GetChild(0);
        rightDoor = transform.GetChild(1);
    }
	
	// Update is called once per frame
	void Update () {
/*
        if (Input.GetKeyDown(KeyCode.Q) && !IsInvoking())
        {
            InvokeRepeating("Open", 0f, openTime / 100f);
        }
        else if (Input.GetKeyDown(KeyCode.W) && !IsInvoking())
        {
            InvokeRepeating("Close", 0f, openTime / 100f);
        }*/
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
        Debug.Log("triggered");
        if (other.gameObject.tag.Equals("Player") && !IsInvoking("Open"))
        {
            CancelInvoke("Close");
            InvokeRepeating("Open", 0f, openTime / 100f);
        } 
    }
}
