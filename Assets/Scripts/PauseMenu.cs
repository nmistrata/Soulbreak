using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    private void Awake()
    {
        GameManager.pauseScreen = gameObject;
    }
}
