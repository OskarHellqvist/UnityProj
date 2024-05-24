using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void Exit()
    {
        Application.Quit();
    }
    public void StartIntro()
    {
        Global.LoadScene_Intro();
    }
    public void StartGame()
    {
        Global.LoadScene_Game();
    }
    public void GoToStartMenu()
    {
        Global.LoadScene_StartMenu();
    }
    public static void GoToWinScene()
    {
        Global.LoadScene_Win();
    }
    public static void GoToGameOverScene()
    {
        Global.LoadScene_GameOver();
    }
}
