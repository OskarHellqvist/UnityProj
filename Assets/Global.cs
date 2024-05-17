using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SojaExiles
{
    public static class Global
    {
        private static bool mouseLocked = false;


        public static void LoadSceneStartMenu()
        {
            SceneManager.LoadScene(0);
        }
        public static void LoadSceneIntro()
        {
            SceneManager.LoadScene(1);
        }
        public static void LoadSceneGame()
        {
            SceneManager.LoadScene(2);
        }
        public static void LoadSceneWin()
        {
            SceneManager.LoadScene(3);
        }
        public static void LoadSceneGameOver()
        {
            SceneManager.LoadScene(3);
        }

        public static void UnlockMouse()
        {
            if (Cursor.lockState == CursorLockMode.Locked)
                Cursor.lockState = CursorLockMode.None;
        }

        public static void LockMouse()
        {
            if (Cursor.lockState == CursorLockMode.None)
                Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
