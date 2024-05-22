using SojaExiles;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace SojaExiles
{
    public class SanityManager : MonoBehaviour
    {
        public static SanityManager playerInstance;

        public GameObject flashlight;
        private PlayerMovement pMovement;
        public Image insanityImage;
        public Image Fader;

        [SerializeField] private float sanity = 100;
        private float decreaseValue = 2;
        private float increaseValue = 0.5f;

        public float Sanity { 
            get { return sanity; }
            set 
            { 
                sanity = Mathf.Clamp(value, 0, 100);
            }
        }

        private bool inSpawnArea;
        private bool isDraining;

        // Start is called before the first frame update
        void Start()
        {
            playerInstance = this;
            pMovement = GetComponent<PlayerMovement>();
        }

        // Update is called once per frame
        void Update()
        {


            if((flashlight.activeInHierarchy && pMovement.battery > 30) || inSpawnArea)
            {
                isDraining = false;
            }
            else
            {
                isDraining = true;
            }

            if (isDraining)
            {
                sanity -= Time.deltaTime * decreaseValue;
            } 
            else if (sanity < 100)
            {
                sanity += Time.deltaTime * increaseValue;
            }
            
            if (sanity <= 0)
            {
                StartCoroutine(FadeToBlack());
            }

            // Limits sanity between 0 - 100
            sanity = Mathf.Clamp(sanity, 0, 100);

            float insTransparency = 1 - sanity / 100;
            insanityImage.color = new Color(insanityImage.color.r, insanityImage.color.g, insanityImage.color.b, insTransparency);

        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.tag == "Respawn")
            {
                inSpawnArea = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Respawn")
            {
                inSpawnArea = false;
            }
        }

        IEnumerator FadeToBlack()
        {
            float fadeSpeed = 0.005f;  // Speed of the fade
            while (Fader.color.a < 1.0f)
            {
                Color color = Fader.color;
                color.a += fadeSpeed * Time.deltaTime;
                Fader.color = color;

                // Ensure it doesn't exceed 1
                if (Fader.color.a >= 1.0f)  
                {
                    color.a = 1.0f;
                    Fader.color = color;
                    break;
                }

                yield return null; // Yield execution to the next frame.
            }

            Global.LoadSceneGameOver();
            Global.UnlockMouse();
        }

    }
}
