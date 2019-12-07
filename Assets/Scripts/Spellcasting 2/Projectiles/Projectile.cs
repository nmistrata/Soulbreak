using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour {

    protected bool isFriendly;
    protected int damage;

    protected virtual void OnTriggerEnter(Collider collider)
    {
        GameObject other = collider.gameObject;
        if (other.tag == "Projectile" || other.tag == "Invisible" || other.tag == "Player" && isFriendly || other.tag == "Enemy" && !isFriendly)
        {
            return;
        }

        if (other.tag == "Player" && !isFriendly)
        {
            other.GetComponent<Player>().TakeDamage(damage);
        }

        if (other.tag == "Enemy" && isFriendly)
        {
            other.GetComponent<Enemy>().TakeDamage(damage);
        }

        //Play sound effect?
        Destroy(this.gameObject);
    }

}
