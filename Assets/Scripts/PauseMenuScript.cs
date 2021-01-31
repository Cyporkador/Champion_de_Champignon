using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    public bool isPaused = false;

    public GameObject pauseMenu;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            pause();
        }
    }

    private void pause()
    {
        if (isPaused)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
            isPaused = false;
        } else
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
            isPaused = true;
        }
    }

    public void resume()
    {
        pause();
    }

    public void menu()
    {
        if (isPaused)
        {
            pause();
        }
        SceneManager.LoadScene("MainMenu");
    }
}
