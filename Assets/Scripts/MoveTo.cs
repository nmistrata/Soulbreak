using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveTo : MonoBehaviour {

    public Transform player;

    public float velocity = 1.5f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(player);
        transform.position += transform.forward * Time.deltaTime * velocity;
	}
}
