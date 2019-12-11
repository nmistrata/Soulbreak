using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

    public Canvas rulesCanvas;
    private Canvas canvas;

    public bool inRules = false;

    private void Start()
    {
        canvas = GetComponent<Canvas>();
    }

    void Update()
    {
        if (inRules)
        {
            if (OVRInput.GetDown(OVRInput.Button.Two))
            {
                canvas.enabled = true;
                rulesCanvas.enabled = false;
                inRules = false;
            }
        } else
        {
            if (OVRInput.GetDown(OVRInput.Button.Two))
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
            }

            if (OVRInput.GetDown(OVRInput.Button.One))
            {
                Debug.Log("startingGame");
                GameManager.StartGame();
            }

            if (OVRInput.GetDown(OVRInput.Button.Three))
            {
                canvas.enabled = false;
                rulesCanvas.enabled = true;
                inRules = true;
            }
        }

    }
}
