using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomZigZagProjectile : Projectile {

    private Rigidbody rb;
    private const float RANDOMNESS = .14f; //0 to 1
    private const float DIRECTION_CHANGE_INTERVAL = .2f; //in seconds

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        InvokeRepeating("RandomizeDirection", 0, DIRECTION_CHANGE_INTERVAL);

        damage = 20;
        isFriendly = true;

    }

    private void RandomizeDirection()
    {
        Vector3 movementModifier = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        movementModifier.Normalize();
        Vector3 velocity = rb.velocity;
        float curSpeed = velocity.magnitude;
        movementModifier *= curSpeed * RANDOMNESS;
        velocity += movementModifier;

        rb.velocity = velocity;
    }
}
