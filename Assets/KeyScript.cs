using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace SojaExiles
{ 
    // Made by Neo Ferrari
    public class KeyScript : MonoBehaviour, Interactable
    {
        private GameObject keyObject;

        // Temp--------------------------------
        [SerializeField] private GameObject doorToOpen;
        private opencloseDoor doorScript;
        // ------------------------------------

        private InventoryScript inventoryScript;

        // Index of the key image to toggle
        [SerializeField] private int keyIndex;

        void Start()
        {
            keyObject = GetComponent<GameObject>();

            // Temp--------------------------------
            doorScript = doorToOpen.GetComponent<opencloseDoor>();
            // ------------------------------------

            // Find the InventoryScript component
            inventoryScript = FindObjectOfType<InventoryScript>();
        }

        public void Interact()
        {
            StartCoroutine(PlayAudioAndDisable());
        }


        private IEnumerator PlayAudioAndDisable()
        {
            // Play the audio
            //audioSource.clip = klirrSound;
            //audioSource.Play();

            FindObjectOfType<AudioManager2>().Play("KeysPickup", transform.position);

            // Toggle the key image in the inventory UI
            if (inventoryScript != null)
            {
                inventoryScript.PickUpKey(keyIndex, doorScript);
            }

            Destroy(gameObject);

            yield return null;
        }
    }
}