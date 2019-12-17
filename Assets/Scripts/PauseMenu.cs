using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    public bool dissapeared = false;
    public bool appeared = false;

    private bool wasToggled = false;

    private void FixedUpdate()
    {
        if(wasToggled)
        {
            if (appeared)
            {
                GameManager.PauseAction();
            }
            wasToggled = false;
            appeared = false;
            dissapeared = false;
        } else if (appeared || dissapeared)
        {
            wasToggled = true;
        }
    }

    private void Awake()
    {
        GameManager.pauseScreen = gameObject;
        GameManager.SetMenuOffset(transform.localPosition);
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("triggered");
        if (appeared)
        {
            foreach (MeshRenderer m in other.gameObject.GetComponentsInChildren<MeshRenderer>())
            {
                m.enabled = false;
            }
        }
        else if (dissapeared)
        {
            Debug.Log("disappeared");
            foreach (MeshRenderer m in other.gameObject.GetComponentsInChildren<MeshRenderer>())
            {
                m.enabled = true;
            }

        }
    }
}
