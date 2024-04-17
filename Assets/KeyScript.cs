using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace SojaExiles
{
    public class KeyScript : MonoBehaviour, Interactable
    {
        // Start is called before the first frame update
        [SerializeField] private AudioClip klirrSound;
        [SerializeField] private AudioSource audioSource;
        public GameObject homeKey;

        // Temp--------------------------------
        [SerializeField] private GameObject doorToOpen;
        private opencloseDoor doorScript;
        // ------------------------------------

        void Start()
        {
            // Temp--------------------------------
            doorScript = doorToOpen.GetComponent<opencloseDoor>();
            // ------------------------------------
        }

        public void Interact()
        {
            StartCoroutine(PlayAudioAndDisable());
        }

        private IEnumerator PlayAudioAndDisable()
        {
            // Play the audio
            audioSource.clip = klirrSound;
            audioSource.Play();

            // Temp--------------------------------
            doorScript.UnlockDoor();
            // ------------------------------------

            // Wait for the duration of the audio clip before disabling the item
            //yield return new WaitForSeconds(audioSource.clip.length);
            yield return null;

            Destroy(homeKey);
        }
    }
}