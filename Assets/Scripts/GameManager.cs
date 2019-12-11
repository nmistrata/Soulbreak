using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameManager
{

    public static bool gameOver = false;
    public static bool isPaused = false;
    private static Vector3 menuOffset;

    public static void TogglePause()
    {
        if (isPaused)
        {
            HidePauseScreen();
            ResumeAction();
        } else
        {
            AppearPauseScreen();
            SlowAction();
        }
        isPaused = !isPaused;
    }

    private static void SlowAction()
    {
        Time.timeScale = .1f;
        Time.fixedDeltaTime = .002f;
        if (player != null) player.GetComponent<OVRPlayerController>().EnableRotation = false;
        AudioListener.pause = true;
    }

    public static void PauseAction()
    {
        Time.timeScale = 0f;
        Time.fixedDeltaTime = 0f;
        if (player != null) player.GetComponent<OVRPlayerController>().EnableRotation = false;
        AudioListener.pause = true;
    }

    private static void ResumeAction()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = .02f;
        if (player != null) player.GetComponent<OVRPlayerController>().EnableRotation = true;
        AudioListener.pause = false;
    }
    private static void HidePauseScreen()
    {
        pauseScreen.GetComponent<Canvas>().enabled = false;
        pauseScreen.transform.parent = player.transform;
        pauseScreen.transform.localRotation = Quaternion.identity;
        pauseScreen.transform.localPosition = menuOffset;
        pauseScreen.GetComponent<PauseMenu>().dissapeared = true;
        pauseScreen.GetComponent<PauseMenu>().appeared = false;
    }

    private static void AppearPauseScreen()
    {
        pauseScreen.transform.parent = null;
        pauseScreen.GetComponent<Canvas>().enabled = true;
        pauseScreen.GetComponent<PauseMenu>().dissapeared = false;
        pauseScreen.GetComponent<PauseMenu>().appeared = true;
    }

    private static void HideGameOverScreen()
    {
        gameOverScreen.GetComponent<Canvas>().enabled = false;
        gameOverScreen.transform.parent = player.transform;
        gameOverScreen.transform.localRotation = Quaternion.identity;
        gameOverScreen.transform.localPosition = menuOffset;
        gameOverScreen.GetComponent<GameOverMenu>().dissapeared = true;
        gameOverScreen.GetComponent<GameOverMenu>().appeared = false;
    }

    private static void AppearGameOverScreen()
    {
        gameOverScreen.transform.parent = null;
        gameOverScreen.GetComponent<Canvas>().enabled = true;
        gameOverScreen.GetComponent<GameOverMenu>().dissapeared = false;
        gameOverScreen.GetComponent<GameOverMenu>().appeared = true;
    }

    public static void EndGame()
    {
        gameOver = true;
        PauseAction();
        HidePauseScreen();
        AppearGameOverScreen();

        foreach (GameObject e in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            e.SetActive(false);
        }

        foreach (GameObject p in GameObject.FindGameObjectsWithTag("Projectile"))
        {
            p.SetActive(false);
        }

    }

    public static void Restart()
    {
        dungeon.Restart();
        HidePauseScreen();
        HideGameOverScreen();
        gameOver = false;
        isPaused = false;
        ResumeAction();
    }

    public static void ExitToMenu()
    {
        gameOver = false;
        isPaused = false;
        ResumeAction();
        SceneManager.LoadScene("MainMenu");
    }

    public static void StartGame()
    {
        gameOver = false;
        isPaused = false;
        ResumeAction();
        SceneManager.LoadScene("Main");
    }

    public static void SetMenuOffset(Vector3 offset)
    {
        menuOffset = offset;
    }

    public static GameObject player;
    public static GameObject gameOverScreen;
    public static GameObject pauseScreen;
    public static GameObject playerUI;
    public static DungeonControllerModularRooms dungeon;
}
