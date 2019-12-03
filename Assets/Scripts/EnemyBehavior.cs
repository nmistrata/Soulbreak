using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour {

    private Transform player;

    private new Rigidbody rigidbody;

    public float velocity = 2f;

    public float maxHealth = 1f;
    private float health;

    public float damage = 1f;

    public float stoppingDistance = 1.5f;
    public float startingDistance = 3f;

    private int state;
    private const int IDLE = 0;
    private const int WALKING = 1;
    private const int ATTACKING = 2;

    private Animator animator;

    void Start () {
        health = maxHealth;
        player = GameManager.player.transform;
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
    }
	
	void Update () {
        if (health <= 0.0f) {
            // TODO: Play a sound effect, etc.
            gameObject.SetActive(false);
        }

        float distance = Vector3.Distance(new Vector3(player.position.x, 0, player.position.z), transform.position);
        transform.LookAt(new Vector3(player.position.x, 0, player.position.z));

        if (state == WALKING && distance > stoppingDistance || state != WALKING && distance > startingDistance) {
            transform.position += transform.forward * Time.deltaTime * velocity;
            state = WALKING;
        } else {
            // Too close to player, stop moving and hit them
            state = ATTACKING;
        }

        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        rigidbody.velocity = Vector3.zero;

        animator.SetInteger("State", state);

        //TEMPORARY FOR TESTING
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            gameObject.SetActive(false);
        }
    }
}
