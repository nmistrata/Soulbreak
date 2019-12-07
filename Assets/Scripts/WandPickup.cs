using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandPickup : MonoBehaviour {

    private GameObject wand = null;
    private const float ROTATION_RATE = 90; //in degrees/s
    private Vector3 wandOffset = new Vector3(0, -3.7f, 0);
    private Quaternion wandRotation = Quaternion.Euler(35, 0, 0);

    public void SetWand(GameObject wand)
    {
        this.wand = Instantiate(wand, transform.position, Quaternion.identity, transform);
        this.wand.transform.localPosition = wandOffset;
        this.wand.transform.localRotation = wandRotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Player>().AddWand(wand);
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        transform.RotateAround(transform.position, Vector3.up, ROTATION_RATE * Time.deltaTime);
    }
}
