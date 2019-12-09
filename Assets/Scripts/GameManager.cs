using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameManager
{

    public static bool gameOver = false;
    public static bool isPaused = false;

    public static void TogglePause()
    {
        if (isPaused)
        {
            resumeAction();
        } else
        {
            pauseAction();
        }
        isPaused = !isPaused;
        pauseScreen.GetComponent<Canvas>().enabled = isPaused;
    }

    private static void pauseAction()
    {
        Time.timeScale = 0;
        if (player != null) player.GetComponent<OVRPlayerController>().EnableRotation = false;
        AudioListener.pause = true;
    }

    private static void resumeAction()
    {
        Time.timeScale = 1;
        if (player != null) player.GetComponent<OVRPlayerController>().EnableRotation = true;
        AudioListener.pause = false;
    }

    public static void EndGame()
    {
        gameOver = true;
        pauseAction();
        pauseScreen.GetComponent<Canvas>().enabled = false;
        gameOverScreen.GetComponent<Canvas>().enabled = true;
        gameOverScreen.GetComponent<GameOverMenu>().UpdateFloorsClimbed(dungeon.curLevel);

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
        player.GetComponent<Player>().Restart();
        pauseScreen.GetComponent<Canvas>().enabled = false;
        gameOverScreen.GetComponent<Canvas>().enabled = false;
        gameOver = false;
        isPaused = false;
        resumeAction();
    }

    public static void ExitToMenu()
    {
        gameOver = false;
        isPaused = false;
        resumeAction();
        SceneManager.LoadScene("MainMenu");
    }

    public static void StartGame()
    {
        gameOver = false;
        isPaused = false;
        resumeAction();
        SceneManager.LoadScene("Main");
    }

    public static GameObject player;
    public static GameObject gameOverScreen;
    public static GameObject pauseScreen;
    public static DungeonControllerModularRooms dungeon;
}
