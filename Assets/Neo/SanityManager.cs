using SojaExiles;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.UI;

namespace SojaExiles
{
    public class SanityManager : MonoBehaviour
    {
        public GameObject flashlight;
        private PlayerMovement pMovement;
        //public Slider slider;
        public Image insanityImage;

        private float sanity = 100;
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

            // Limits sanity between 0 - 100
            sanity = Mathf.Clamp(sanity, 0, 100);

            float insTransparency = 1 - sanity / 100;
            insanityImage.color = new Color(insanityImage.color.r, insanityImage.color.g, insanityImage.color.b, insTransparency);

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
