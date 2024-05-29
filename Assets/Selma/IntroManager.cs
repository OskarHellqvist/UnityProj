using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour
{
    public AudioSource audioSource;
    public TMP_Text infoText;

    bool loadingStarted = false;
    AsyncOperation asyncOperation;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Ensure infoText is initially deactivated
        infoText.gameObject.SetActive(false);

        // Start loading the scene asynchronously when the intro scene starts
        if (!loadingStarted)
        {
            StartCoroutine(LoadScene());
        }
    }

    void Update()
    {
        // Display infoText when the audio stops playing
        if (!audioSource.isPlaying && !infoText.text.Equals("Press E to Continue"))
        {
            infoText.gameObject.SetActive(true);
            infoText.text = "Press E to Continue";
        }

        //// Check for input to activate the scene if loading is complete
        if (asyncOperation != null && asyncOperation.progress >= 0.9f && audioSource.isPlaying)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                Global.LoadScene_Game(); // Trigger the Global scene loading function
            }
            else if (Input.anyKey && !infoText.text.Equals("Press S to Skip Intro"))
            {
                infoText.gameObject.SetActive(true);
                infoText.text = "Press S to Skip Intro";
            }
        }
    }

    IEnumerator LoadScene()
    {
        loadingStarted = true; // Set loadingStarted to true to prevent multiple loading attempts
        asyncOperation = SceneManager.LoadSceneAsync(2);
        asyncOperation.allowSceneActivation = false;

        // Wait until the scene is loaded (90% progress)
        while (asyncOperation.progress < 0.9f)
        {
            yield return null;
        }

        // The scene has finished loading but is not activated
        yield return new WaitUntil(() => !audioSource.isPlaying && Input.GetKeyDown(KeyCode.E));
        Global.LoadScene_Game();
    }
}
