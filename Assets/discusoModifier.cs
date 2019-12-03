using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class discusoModifier : MonoBehaviour {
    int frameCounter;
    Rigidbody pb;
    System.Random r;
	// Use this for initialization
	void Start () {
        pb = GetComponent<Rigidbody>();
        r = new System.Random();
	}
	
	// Update is called once per frame
	void Update () {
		if(frameCounter % 15 == 0)
        {
            float v = pb.velocity.magnitude;
            float rx = (float)r.NextDouble()*1.15f;
            float ry = (float)r.NextDouble()*1.15f;
            float rz = (float)r.NextDouble()*1.15f;
            pb.velocity = new Vector3(rx, ry, rz) * v;
            pb.angularVelocity = new Vector3(rx, ry, rz) *10;
        }
        if(frameCounter > 150)
        {
            frameCounter = 0;
        }
        frameCounter++;
	}
}
