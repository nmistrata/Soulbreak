using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class energyLargoExplosion : MonoBehaviour {
    public GameObject proj;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
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

        tempProj.transform.position = transform.position;
        tempProj1.transform.position = transform.position;
        tempProj2.transform.position = transform.position;
        tempProj3.transform.position = transform.position;
        tempProj4.transform.position = transform.position;
        tempProj5.transform.position = transform.position;

        Rigidbody p = tempProj.GetComponent<Rigidbody>();
        Rigidbody p1 = tempProj1.GetComponent<Rigidbody>();
        Rigidbody p2 = tempProj2.GetComponent<Rigidbody>();
        Rigidbody p3 = tempProj3.GetComponent<Rigidbody>();
        Rigidbody p4 = tempProj4.GetComponent<Rigidbody>();
        Rigidbody p5 = tempProj5.GetComponent<Rigidbody>();

        p.velocity = new Vector3(0.5f, 0.5f, 0) * 6;
        p1.velocity = new Vector3(0.5f, 0, 0.5f) * 6;
        p2.velocity = new Vector3(0, 0.5f, 0) * 6;
        p3.velocity = new Vector3(0.5f, 0, 0) * 6;
        p4.velocity = new Vector3(0,0 , 0.5f) * 6;
        p5.velocity = new Vector3(0, 0.5f, 0.5f) * 6;
        Destroy(gameObject);
    }
}
