using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager_Script : MonoBehaviour
{
    public GameObject GameOverScreen;
    public GameObject LevelEndScreen;
    private PauseManager_Script _pauseManager;

    void Start()
    {
        Time.timeScale = 1;
        _pauseManager = GetComponent<PauseManager_Script>();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadLevel00()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadLevel01()
    {
        SceneManager.LoadScene(2);
    }

    public void GameOver()
    {
        GameOverScreen.SetActive(true);
        _pauseManager.ToggleControls();
        Time.timeScale = 0;
    }

    public void LevelEnd()
    {
        LevelEndScreen.SetActive(true);
        _pauseManager.ToggleControls();
        Time.timeScale = 0;
    }
}
