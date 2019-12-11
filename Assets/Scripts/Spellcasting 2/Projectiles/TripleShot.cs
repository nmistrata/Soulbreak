using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleShot : Projectile {

    public GameObject shrapnel;
    private const float ANGLE = 15; //degrees

	// Use this for initialization
	void Start () {
        GameObject leftShrapnel = Instantiate(shrapnel, transform.position, transform.rotation, null);
        GameObject rightShrapnel = Instantiate(shrapnel, transform.position, transform.rotation, null);
        GameObject centerShrapnel = Instantiate(shrapnel, transform.position, transform.rotation, null);

        leftShrapnel.transform.RotateAround(transform.position, Vector3.up, ANGLE);
        rightShrapnel.transform.RotateAround(transform.position, Vector3.up, -ANGLE);

        float curSpeed = GetComponent<Rigidbody>().velocity.magnitude;
        
        leftShrapnel.GetComponent<Rigidbody>().velocity = curSpeed * leftShrapnel.transform.forward;
        rightShrapnel.GetComponent<Rigidbody>().velocity = curSpeed * rightShrapnel.transform.forward;
        centerShrapnel.GetComponent<Rigidbody>().velocity = curSpeed * centerShrapnel.transform.forward;

        Destroy(gameObject);
    }
}
