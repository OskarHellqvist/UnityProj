using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SojaExiles
{
    public class KeyScript : MonoBehaviour, Interactable
    {
        // Start is called before the first frame update
        [SerializeField] private AudioSource audioSource;
        public GameObject homeKey;
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }
        public void Interact()
        {
            StartCoroutine(PlayAudioAndDisable());
        }

        private IEnumerator PlayAudioAndDisable()
        {
            // Play the audio
            audioSource.Play();

            // Wait for the duration of the audio clip before disabling the item
            yield return new WaitForSeconds(audioSource.clip.length);

            Destroy(homeKey);

            Debug.Log("Item disabled");
        }
    }
}