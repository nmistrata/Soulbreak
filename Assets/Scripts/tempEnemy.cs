using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempEnemy : MonoBehaviour {

    private void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            gameObject.SetActive(false);
        }
    }
}
