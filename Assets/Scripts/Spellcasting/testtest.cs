using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testtest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.GetComponent<OVRGrabbable>().isGrabbed)
        {
            Charactor.addWand();
            Destroy(gameObject);
        }
	}
}
