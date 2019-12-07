using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour {

    protected Transform player;


    public float speed = 2f;

    public float maxHealth = 100f;
    protected float health;

    public float stoppingDistance = 1.5f;
    public float startingDistance = 3f;

    private int state;
    private const int IDLE = 0;
    private const int WALKING = 1;
    private const int ATTACKING = 2;

    private new Rigidbody rigidbody;
    private Animator animator;

    protected virtual void Start () {
        health = maxHealth;
        player = GameManager.player.transform;
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
    }
	
	protected virtual void Update () {

        float distance = Vector3.Distance(new Vector3(player.position.x, 0, player.position.z), transform.position);
        transform.LookAt(new Vector3(player.position.x, 0, player.position.z));

        if (state == WALKING && distance > stoppingDistance || state != WALKING && distance > startingDistance) {
            transform.position += transform.forward * Time.deltaTime * speed;
            state = WALKING;
        } else {
            // Too close to player, stop moving and hit them
            Attack();
            
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

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            //TODO gameover
            gameObject.SetActive(false);
        }
    }

    public abstract void Attack();
}
