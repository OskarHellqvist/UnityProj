using SojaExiles;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.UI;

namespace SojaExiles
{
    public class Insanity : MonoBehaviour
    {
        public GameObject flashlight;
        private PlayerMovement pMovement;
        //public Slider slider;
        public Image insanity;

        public static float sanity = 100;
        private float decreaseValue = 2;
        private float increaseValue = 0.5f;

        private bool inSpawnArea;
        private bool isDraining;

        // Start is called before the first frame update
        void Start()
        {
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

                if (sanity <= 50)
                {
                    FindObjectOfType<AudioManager>().Play("HeartBeat");
                    Debug.Log("Heart");
                }
            }

            // Limits sanity between 0 - 100
            sanity = Mathf.Clamp(sanity, 0, 100);

            float insTransparency = 1 - sanity / 100;
            insanity.color = new Color(insanity.color.r, insanity.color.g, insanity.color.b, insTransparency);

            //if (slider != null)
            //{
            //    slider.value = sanity;
            //}
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

    }
}
