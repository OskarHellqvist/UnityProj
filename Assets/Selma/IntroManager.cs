using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject infoText;

    bool loadingStarted = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Start loading the scene asynchronously when the intro scene starts
        if (!loadingStarted)
        {
            StartCoroutine(LoadScene());
        }
    }

    void Update()
    {
        if (!audioSource.isPlaying)
        {
            infoText.SetActive(true);
        }
    }

    IEnumerator LoadScene()
    {
        loadingStarted = true; // Set loadingStarted to true to prevent multiple loading attempts
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Base Scene (Good)");
        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone)
        {
            if (asyncOperation.progress >= 0.9f)
            {
                if ((!audioSource.isPlaying && Input.GetKeyDown(KeyCode.E)) || Input.GetKeyDown(KeyCode.S))
                {
                    asyncOperation.allowSceneActivation = true;
                }
            }

            yield return null;
        }
    }
}
