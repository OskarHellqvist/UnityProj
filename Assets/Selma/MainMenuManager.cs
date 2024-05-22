using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void Exit()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        Global.LoadSceneGame();
    }
    public void LoadSceneStartMenu()
    {
        Global.LoadSceneStartMenu();
    }
    public void LoadSceneIntro()
    {
        Global.LoadSceneIntro();
    }
    public static void LoadSceneWin()
    {
        Global.LoadSceneWin();
    }
    public static void LoadSceneGameOver()
    {
        Global.LoadSceneGameOver();
    }
}
