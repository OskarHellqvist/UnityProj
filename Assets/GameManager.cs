using UnityEngine;
using UnityEngine.SceneManagement;

    public class GameManager : MonoBehaviour
    {
        // You can expose this method to the Unity Inspector.
        public void LoadSceneStartMenu()
        {
            SojaExiles.Global.LoadSceneStartMenu();
        }
        public void LoadSceneIntro()
        {
            SojaExiles.Global.LoadSceneIntro();
        }
        public static void LoadSceneGame()
        {
            SojaExiles.Global.LoadSceneGame();
        }
        public static void LoadSceneWin()
        {
            SojaExiles.Global.LoadSceneWin();
        }
        public static void LoadSceneGameOver()
        {
            SojaExiles.Global.LoadSceneGameOver();
        }

        public static void UnlockMouse()
        {
            SojaExiles.Global.UnlockMouse();
        }

        public static void LockMouse()
        {
            SojaExiles.Global.LockMouse();
        }
    }

