using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeScript : MonoBehaviour
{
    public static FadeScript instance;

    private static Image FaderImage;

    [HideInInspector] public bool isFading;

    private static Color blackAlpha;

    public static float fadeSpeed = 0.5f;  // Speed of the fade

    void Awake()
    { 
        if (instance == null)
        {
            this.GetComponent<Canvas>().sortingOrder = 1;

            instance = this;

            FaderImage = gameObject.AddComponent<Image>();
            FaderImage.rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);  // Set the size to cover the entire screen
            
            FaderImage.raycastTarget = false;

            blackAlpha = Color.black;
            blackAlpha.a = 0f;

            FaderImage.color = blackAlpha;
        }
        else
        {
            // Makes sure there can only be one
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

    }

    public void Update()
    {
        if (Input.GetKey(KeyCode.X))
        {
            Global.LoadScene_GameOver();
        }
    }

    public void FadeIntoScene(int sceneIndex)
    {
        if (isFading) 
        {
            Debug.LogError("Trying to fade while fading!");
        }
        else
        {
            StartCoroutine(SceneFade(sceneIndex));
        }
    }

    private IEnumerator SceneFade(int sceneIndex)
    {

        Debug.Log("Fading?");

        isFading = true;

        // Start fading out
        yield return StartCoroutine(FadeOutCoroutine());

        // Load the specified scene
        SceneManager.LoadScene(sceneIndex);

        // Start fading in
        yield return StartCoroutine(FadeInCoroutine());

        isFading = false;
    }

    public IEnumerator FadeOutCoroutine()
    {
        // Fade to black
        while (FaderImage.color.a < 1.0f)
        {
            Color color = FaderImage.color;
            color.a += fadeSpeed * Time.deltaTime;
            FaderImage.color = color;

            // Ensure it doesn't exceed 1
            if (FaderImage.color.a >= 1.0f)
            {
                color.a = 1.0f;
                FaderImage.color = color;
                break;
            }

            yield return null; // Yield execution to the next frame.
        }
    }

    public IEnumerator FadeInCoroutine()
    {
        // Fade from black
        while (FaderImage.color.a > 0f)
        {
            Color color = FaderImage.color;
            color.a -= fadeSpeed * Time.deltaTime;
            FaderImage.color = color;

            // Ensure it doesn't go below 0
            if (FaderImage.color.a <= 0f)
            {
                color.a = 0f;
                FaderImage.color = color;
                break;
            }

            yield return null; // Yield execution to the next frame.
        }
    }
}
