using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour {

    public Transform player;

    public float velocity = 2f;

    public float maxHealth = 1f;
    private float health;

    public float damage = 1f;

	void Start () {
        health = maxHealth;
	}
	
	void Update () {
        if (health <= 0.0f) {
            // TODO: Play a sound effect, etc.
            gameObject.SetActive(false);
        }


        if (Mathf.Abs(Vector3.Distance(transform.position, player.position)) > 0.5) {
            transform.LookAt(player);
            transform.position += transform.forward * Time.deltaTime * velocity;
        } else {
            // Too close to player, stop moving and hit them
            Debug.Log("Hitting the player!");
            velocity = 0;
        }
	}
}
