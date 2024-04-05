using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace UnityProj
{
    public class Insanity : MonoBehaviour
    {
        public GameObject flashlight;

        public Slider slider;
        public Image insanity;

        public float sanity = 100;
        private float decreaseValue = 2;
        private float increaseValue = 0.5f;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

            if (!flashlight.active)
            {
                sanity -= Time.deltaTime * decreaseValue;
            } 
            else if (sanity < 100)
            {
                sanity += Time.deltaTime * increaseValue;
            }

            if (sanity > 100) sanity = 100;
            else if (sanity < 0) sanity = 0;

            float insTransparency = 1 - sanity / 100;
            insanity.color = new Color(insanity.color.r, insanity.color.g, insanity.color.b, insTransparency);

            slider.value = sanity;
        }
    }
}
