using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutroNote : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    public void Activate()
    {
        Time.timeScale = 1f;
        StartCoroutine(FadeAfterSeconds(2.5f));
    }

    private IEnumerator FadeAfterSeconds(float delay)
    {
        StartCoroutine(FadeSoundInSeconds(delay+0.5f));
        yield return new WaitForSeconds(delay);
        Global.LoadScene_StartMenu();
    }

    private IEnumerator FadeSoundInSeconds(float delay)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / delay;
            yield return null;
        }

        // Ensure volume is set to 0 at the end to avoid any potential rounding errors
        audioSource.volume = 0f;
    }
}
