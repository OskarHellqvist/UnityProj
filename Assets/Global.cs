using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Global
{
    public static void LoadScene_StartMenu()
    {
        UnlockMouse();
        FadeScript.instance.FadeIntoScene(0);
    }
    public static void LoadScene_Intro()
    {
        FadeScript.instance.FadeIntoScene(1);
    }
    public static void LoadScene_Game()
    {
        LockMouse();
        FadeScript.instance.FadeIntoScene(2);
    }
    public static void LoadScene_GameOver()
    {
        UnlockMouse();
        FadeScript.instance.FadeIntoScene(3);
    }
    public static void LoadScene_WinArea()
    {
        UnlockMouse();
        FadeScript.instance.FadeIntoSceneWakeUp(4);
    }
    //public static void LoadScene_Win()
    //{
    //    UnlockMouse();
    //    FadeScript.instance.FadeIntoScene(5);
    //}


    public static void UnlockMouse()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public static void LockMouse()
    {
        if (Cursor.lockState == CursorLockMode.None)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
