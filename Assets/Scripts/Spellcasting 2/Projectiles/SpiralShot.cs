using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralShot : Projectile {

    private const float DAMAGE_INCREASE_PER_SECOND = 1.7f; //measured in percent of base damage
    private const float SCALE_MULTIPLY_PER_SECOND = 3f;
    private float baseDamage = 30;
    private float baseScale;
    public float scale;

    void Start()
    {
        damage = baseDamage;
        isFriendly = true;
        baseScale = gameObject.transform.localScale.x;
        scale = baseScale;
    }

    void Update () {
        damage += baseDamage * DAMAGE_INCREASE_PER_SECOND * Time.deltaTime;
        scale *= 1 + (SCALE_MULTIPLY_PER_SECOND-1) * Time.deltaTime;
        gameObject.transform.localScale = new Vector3(scale, scale, scale);
        Debug.Log(damage);
	}

    public override void MultiplyDamage(float multiplier)
    {
        base.MultiplyDamage(multiplier);
        baseDamage *= multiplier;
    }
}
