using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour {

    public float teleportDelay = 1.5f;
    private float teleportTimer;
    private Vector3 destination;
    private bool destinationSet = false;

    private void Start()
    {
        teleportTimer = teleportDelay;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && enabled)
        {
            if (teleportTimer < 0)
            {
                teleportTimer = teleportDelay;
                Teleport(other.gameObject);
                return;
            } else
            {
                teleportTimer -= Time.deltaTime;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            teleportTimer = teleportDelay;
        }
    }

    private void Teleport(GameObject o)
    {
        if (!destinationSet) return;
        o.transform.position = destination;
        GetComponentInParent<DungeonControllerModularRooms>().AdvanceLevel();
    }

    public void SetDestination(Vector3 destination)
    {
        this.destination = destination;
        destinationSet = true;
    }
    public void SetDestination(float x, float y, float z)
    {
        this.destination = new Vector3(x,y,z);
        destinationSet = true;
    }

}
