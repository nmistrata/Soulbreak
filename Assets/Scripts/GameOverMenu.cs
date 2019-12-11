using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour {

    public bool dissapeared;
    public bool appeared;
    private bool wasToggled;
    public Text floorsClimbedText;

    // Use this for initialization
    private void Awake()
    {
        GameManager.gameOverScreen = gameObject;
    }

    private void FixedUpdate()
    {
        if (wasToggled)
        {
            if (appeared)
            {
                GameManager.PauseAction();
            }
            wasToggled = false;
            appeared = false;
            dissapeared = false;
        }
        else if (appeared || dissapeared)
        {
            wasToggled = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (appeared)
        {
            foreach (MeshRenderer m in other.gameObject.GetComponentsInChildren<MeshRenderer>())
            {
                m.enabled = false;
            }
        }
        else if (dissapeared)
        {
            foreach (MeshRenderer m in other.gameObject.GetComponentsInChildren<MeshRenderer>())
            {
                m.enabled = true;
            }

        }
    }
}
