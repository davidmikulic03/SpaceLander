using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private GameManager gameManager;

    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    public void PauseGame()
    {
        gameManager.Pause();
    }

    public void ResumeGame()
    {
        gameManager.ResumeGame();
    }

    public void ResetGame()
    {
        gameManager.ResetGame();
    }

    public void QuitToMainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
}
