using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseController : MonoBehaviour
{
    bool currentPauseState = false;
    [SerializeField] GameObject pauseMenu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            if (!currentPauseState)
            {
                PauseGameTime();
            }
            else
            {
                UnPauseGameTime();
            }
        }
    }

    public void PauseGameTime()
    {
        currentPauseState = !currentPauseState;
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    public void UnPauseGameTime()
    {
        currentPauseState = !currentPauseState;
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    public void UnpauseAndRestart()
    {
        Health_Heart.ResetHealth();
        UnpauseAndLoad(SceneManager.GetActiveScene().name);
    }

    public void UnpauseAndLoad(string nextScene)
    {
        Health_Heart.ResetHealth();

        currentPauseState = !currentPauseState;
        Time.timeScale = 1;
        SceneManager.LoadScene(nextScene);
    }
}