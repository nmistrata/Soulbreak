using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spellHandler : MonoBehaviour {
    public GameObject proj;
    public GameObject proj2;
    public GameObject proj3;
    public GameObject src;
    bool shotRecentlysoDontShootAgain;
    int frameCounter = 0;
	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        if(frameCounter == 100)
        {
            shotRecentlysoDontShootAgain = false;
        }
        if (!shotRecentlysoDontShootAgain)
        {
            if (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) > 0.0f && Charactor.equippedWand == 0)
            {
                GameObject tempProj;

                tempProj = Instantiate(proj) as GameObject;

                tempProj.transform.position = transform.position + 1 * src.transform.forward;


                Rigidbody p = tempProj.GetComponent<Rigidbody>();

                p.velocity = src.transform.forward * 3;
                shotRecentlysoDontShootAgain = true;
            }
        }
        if (!shotRecentlysoDontShootAgain)
        {
            if (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) > 0.0f && Charactor.equippedWand == 1)
            {
                GameObject tempProj;
                GameObject tempProj1;
                GameObject tempProj2;
                GameObject tempProj3;
                GameObject tempProj4;
                GameObject tempProj5;

                tempProj = Instantiate(proj) as GameObject;
                tempProj1 = Instantiate(proj) as GameObject;
                tempProj2 = Instantiate(proj) as GameObject;
                tempProj3 = Instantiate(proj) as GameObject;
                tempProj4 = Instantiate(proj) as GameObject;
                tempProj5 = Instantiate(proj) as GameObject;

                tempProj.transform.position = transform.position + 1 * src.transform.forward + new Vector3(0, 0, 0);
                tempProj1.transform.position = transform.position + 1 * src.transform.forward + new Vector3(0, 0.5f, 0);
                tempProj2.transform.position = transform.position + 1 * src.transform.forward + new Vector3(0, 0, 0.5f);
                tempProj3.transform.position = transform.position + 1 * src.transform.forward + new Vector3(0.5f, 0, 0);
                tempProj4.transform.position = transform.position + 1 * src.transform.forward + new Vector3(0, 0.5f, 0.5f);
                tempProj5.transform.position = transform.position + 1 * src.transform.forward + new Vector3(0.5f, 0.5f, 0);

                Rigidbody p = tempProj.GetComponent<Rigidbody>();
                Rigidbody p1 = tempProj1.GetComponent<Rigidbody>();
                Rigidbody p2 = tempProj2.GetComponent<Rigidbody>();
                Rigidbody p3 = tempProj3.GetComponent<Rigidbody>();
                Rigidbody p4 = tempProj4.GetComponent<Rigidbody>();
                Rigidbody p5 = tempProj5.GetComponent<Rigidbody>();

                p.velocity = src.transform.forward * 1;
                p1.velocity = src.transform.forward * 2;
                p2.velocity = src.transform.forward * 3;
                p3.velocity = src.transform.forward * 4;
                p4.velocity = src.transform.forward * 5;
                p5.velocity = src.transform.forward * 6;
                shotRecentlysoDontShootAgain = true;
            }
        }
        if (!shotRecentlysoDontShootAgain)
        {
            if (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) > 0.0f && Charactor.equippedWand == 2)
            {
                GameObject tempProj;

                tempProj = Instantiate(proj2) as GameObject;

                tempProj.transform.position = transform.position + 4 * src.transform.forward;


                Rigidbody p = tempProj.GetComponent<Rigidbody>();

                p.velocity = src.transform.forward * 15;
                shotRecentlysoDontShootAgain = true;
            }
        }
        if (!shotRecentlysoDontShootAgain)
        {
            if (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) > 0.0f && Charactor.equippedWand == 3)
            {
                GameObject tempProj;

                tempProj = Instantiate(proj3) as GameObject;

                tempProj.transform.position = transform.position + 2 * src.transform.forward;


                Rigidbody p = tempProj.GetComponent<Rigidbody>();

                p.velocity = src.transform.forward * 5;
                shotRecentlysoDontShootAgain = true;
            }
        }
        ++frameCounter;
        if (frameCounter > 100)
        {
            frameCounter = 0;
        }
    }
}

