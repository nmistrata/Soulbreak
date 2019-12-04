using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private const float MAX_HEALTH = 100f;

    private float health;

    private void Awake()
    {
        GameManager.player = gameObject;
    }
}
