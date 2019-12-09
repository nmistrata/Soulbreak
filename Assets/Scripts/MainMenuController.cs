using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {


    void Update()
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
            GameManager.StartGame();
        }
    }
}
