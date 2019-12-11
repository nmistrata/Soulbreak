using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    public bool dissapeared;
    public bool appeared;

    private bool wasToggled;

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
        if (appeared)
        {
            Debug.Log("Hiding obstructions");
            foreach (MeshRenderer m in other.gameObject.GetComponentsInChildren<MeshRenderer>())
            {
                m.enabled = false;
            }
        }
        else if (dissapeared)
        {
            Debug.Log("Reappearing obstructions");
            foreach (MeshRenderer m in other.gameObject.GetComponentsInChildren<MeshRenderer>())
            {
                m.enabled = true;
            }

        }
    }
}
