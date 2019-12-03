using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempPlayer : MonoBehaviour {

    private void Awake()
    {
        GameManager.player = gameObject;
    }
}
