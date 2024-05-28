using UnityEngine;
using System.Collections;

namespace SojaExiles
{
    public class SoundFader : MonoBehaviour
    {
        public AudioSource audioSource;
        public float fadeDuration = 2f;
        public Transform player; // Reference to the player GameObject
        public float maxVolume = 0.5f; // Maximum volume when player is closest
        public float minVolume = 0.1f; // Minimum volume when player is farthest
        public float maxAudibleDistance = 6f; // Maximum distance at which the sound is audible

        void Start()
        {
            // Assuming you've assigned the AudioSource component in the inspector
            if (audioSource == null)
            {
                audioSource = GetComponent<AudioSource>();
            }

            // If player reference is not set, try to find the player GameObject by tag
            if (player == null)
            {
                player = GameObject.FindGameObjectWithTag("Player").transform;
            }
        }

        public void StartFadeOut()
        {
            StartCoroutine(FadeOut(audioSource, fadeDuration));
        }

        IEnumerator FadeOut(AudioSource audioSource, float fadeDuration)
        {
            float startVolume = audioSource.volume;

            while (audioSource.volume > 0)
            {
                audioSource.volume -= startVolume * Time.deltaTime / fadeDuration;
                yield return null;
            }

            // Ensure volume is completely zero
            audioSource.volume = 0;

            // Stop the audio
            audioSource.Stop();
        }

        void Update()
        {
            // Check if player reference is not set
            if (player == null)
            {
                Debug.LogError("Player reference is not set in SoundFader.");
                return;
            }

            // Calculate the distance between the player and the audio source
            float distance = Vector3.Distance(transform.position, player.position);

            // Calculate the volume based on the distance
            float volume = Mathf.Lerp(maxVolume, minVolume, distance / maxAudibleDistance);

            if (Time.timeScale == 0)
            {
                audioSource.volume = 0;
            }
            else
            {
                // Set the volume of the audio source
                audioSource.volume = volume;
            }
        }
    }
}
