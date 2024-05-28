using SojaExiles;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.UI;

namespace SojaExiles
{
    public class SanityManager : MonoBehaviour
    {
        private Quotes quotes;
        public static SanityManager manager;

        public GameObject flashlight;
        private PlayerMovement pMovement;
        //public Slider slider;
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

        [HideInInspector] private bool inSpawnArea;
        
        public void SetInSpawnAreaToFalse()
        {
            inSpawnArea = false;
        }

        private bool isDraining, isRegaining;

        [SerializeField] private AudioSource heartBeat;

        // Start is called before the first frame update
        void Start()
        {
            manager = this;
            pMovement = GetComponent<PlayerMovement>();
            quotes = FindObjectOfType<Quotes>();

            heartBeat = GetComponent<AudioSource>();
        }

        // Update is called once per frame
        void Update()
        {
            // Reference to
            if ( quotes != null && sanity < 40) { quotes.LowSanity(); }
            if (inSpawnArea)
            {
                isRegaining = false;
                isDraining = false;
            }
            else if (flashlight.activeInHierarchy && pMovement.battery > 30)
            {
                isDraining = false;
                isRegaining = true;
            }
            else
            {
                isRegaining = false;
                isDraining = true;
            }
            

            if (isDraining)
            {
                sanity -= Time.deltaTime * decreaseValue;
            } 
            else if (sanity < 100 && isRegaining)
            {
                sanity += Time.deltaTime * increaseValue;
            }
            
            if (sanity <= 0 && !FadeScript.instance.isFading)
            {
                Global.LoadScene_GameOver();
            }

            // Limits sanity between 0 - 100
            sanity = Mathf.Clamp(sanity, 0, 100);

            float insTransparency = 1 - sanity / 100;
            insanityImage.color = new Color(insanityImage.color.r, insanityImage.color.g, insanityImage.color.b, insTransparency);

            if (Time.timeScale == 0f)
            {
                heartBeat.volume = 0;
            }
            else
            {
                heartBeat.volume = Mathf.Pow((100 - sanity) / 100, 2);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.tag == "Respawn")
            {
                inSpawnArea = true;
            }
        }

        public void OnTriggerExit(Collider other)
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

            Global.LoadScene_GameOver();
            Global.UnlockMouse();
        }

    }
}
