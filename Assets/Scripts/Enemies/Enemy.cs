using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour {

    protected Transform player;


    public float speed = 2f;

    public float baseHealth = 100f;
    public float healthPerLevel = 30f;

    protected float maxHealth = 100f;
    protected float health;

    public float stoppingDistance = 1.5f;
    public float startingDistance = 3f;

    public AudioClip walking;
    public AudioClip spawn;
    public AudioClip attackSound;
    public AudioClip getHit;
    public AudioClip death;
    protected AudioSource audioSource;

    private int state;
    private const int IDLE = 0;
    private const int WALKING = 1;
    private const int ATTACKING = 2;

    private float timeSinceLastHit = 0;

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
            //PlaySound(walking, true);
        } else {
            StopLoopingSound();
            // Too close to player, stop moving and hit them
            Attack();
            state = ATTACKING;
        }

        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        rigidbody.velocity = Vector3.zero;

        animator.SetInteger("State", state);
        timeSinceLastHit += Time.deltaTime;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if(timeSinceLastHit > .2f)
        {
            PlaySound(getHit);
        }
        if (health <= 0)
        {
            Die();
        }
        timeSinceLastHit = 0;
    }

    public abstract void Attack();

    public virtual void SetLevel(int level)
    {
        return;
    }

    protected void PlaySound(AudioClip clip, bool shouldLoop = false)
    {
        Debug.Log(clip.name);
        Debug.Log(audioSource);
        if (shouldLoop)
        {
            if (audioSource.clip == clip)
            {
                return;
            }
            StopLoopingSound();
            audioSource.clip = clip;
            audioSource.loop = true;
            audioSource.Play();
        }
        else {
            audioSource.PlayOneShot(clip, 1f);
        }
    }

    public void Die()
    {
        //PlaySound(death);
        gameObject.SetActive(false);
    }

    public void Spawn()
    {
        gameObject.SetActive(true);
        audioSource = GetComponent<AudioSource>();
        PlaySound(spawn);
    }

    private void StopLoopingSound()
    {
        audioSource.Stop();
        audioSource.clip = null;
    }
}
