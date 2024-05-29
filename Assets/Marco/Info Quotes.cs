using SojaExiles;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace SojaExiles
{
    public class Quotes : MonoBehaviour
    {
        public TextMeshProUGUI textMeshPro;
        public Camera mainCamera;
        private int textCounterSanity = 0;
        private int textCounterBattery = 0;
        private int textCounterInventory = 0;

        private int delayInSec = 5;

        void Start()
        {
            mainCamera = Camera.main; // Get the main camera
            // Make sure the Canvas is set to World Space and position it correctly
            textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
            textMeshPro.text = " ";
            Debug.Log(textMeshPro);
        }
        void Update()
        {
            // Ensure the canvas is always facing the cameraw
            //if (textMeshPro != null)
            //{
            //    textMeshPro.transform.position = mainCamera.transform.position + mainCamera.transform.forward * 2;
            //    textMeshPro.transform.rotation = mainCamera.transform.rotation;
            //}
        }

        public void UpdateText(string newText, float duration)
        {
            if (textMeshPro != null)
            {
                textMeshPro.text = newText;
                StartCoroutine(HideTextAfterDelay(duration));
            }
        }

        private IEnumerator HideTextAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            if (textMeshPro != null)
            {
                textMeshPro.text = "";
            }
        }

        public void LowSanity()
        {
            if(textCounterSanity < 1)
            {
                UpdateText("Your sanity will go down from darkness. \nMake sure to use your Flashlight [F]!", delayInSec);
                textCounterSanity++;
            }
        }

        public void BatteryLow()
        {
            if (textCounterBattery < 1)
            {
                UpdateText("Flashlights battery is soon depleted! Remember to use it resourcefully", delayInSec);
                textCounterBattery++;
            }
        }

        public void InventoryPickUp()
        {
            if (textCounterInventory < 1)
            {
                UpdateText("Hold [Tab] for Inventory", delayInSec);
                textCounterInventory++;
            }
        }
        
    }
}
