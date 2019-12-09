using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour {

    public Text floorsClimbedText;

    // Use this for initialization
    private void Awake()
    {
        GameManager.gameOverScreen = gameObject;
    }

    public void UpdateFloorsClimbed(int floorsClimbed)
    {
        floorsClimbedText.text = "You Defeated " + floorsClimbed + " Floors!";
    }
}
