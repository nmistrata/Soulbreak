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
		if(frameCounter % 100 == 0)
        {
            float v = pb.velocity.magnitude;
            float rx = (float)r.NextDouble()*5;
            float ry = (float)r.NextDouble()*5;
            float rz = (float)r.NextDouble()*5;
            pb.velocity = new Vector3(rx, ry, rz) * v;
            pb.angularVelocity = new Vector3(rx, ry, rz) *10;
        }
        if(frameCounter > 300)
        {
            frameCounter = 0;
        }
        frameCounter++;
	}
}
