using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseMenuUI;
    public GameObject backgroundPanel;
    public GameObject notePanel;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) //TODO: pause camera movement
        {
            if (isPaused)
            {
                Resume();
            }
            else if (!isPaused && notePanel.activeInHierarchy == false)
            {
                Pause();
            }
        }

        if(notePanel.activeInHierarchy == true)
        {
            Time.timeScale = 0f;
            isPaused = true;
        }
    }

    void Resume()
    {
        pauseMenuUI.SetActive(false);
        backgroundPanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        backgroundPanel.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Exit()
    {
        Application.Quit();
    }

}
