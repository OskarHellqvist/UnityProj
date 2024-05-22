using SojaExiles;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SojaExiles
{
    public class Quotes : MonoBehaviour
    {
        [SerializeField] GameObject text;
        [SerializeField] KeyCode key;
        private TextMesh textMesh;

        void Start()
        {
            GameObject textObject = new GameObject("DynamicTextMesh");
            textMesh = textObject.AddComponent<TextMesh>();
            textMesh.text = "empty";
            textMesh.color = Color.white;
            textMesh.fontSize = 18;
            // textObject.transform.position = new Vector3(0, 0, 0);
        }
        public void UpdateText(string newText, float duration)
        {
            if (textMesh != null)
            {
                textMesh.text = newText;
                StartCoroutine(HideTextAfterDelay(duration));
            }
        }
        private IEnumerator HideTextAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            if (textMesh != null)
            {
                textMesh.text = "";
            }
        }
        public void LowSanity()
        {
            UpdateText("Detective Moore's sanity level will go down from darkness. Make sure to use your Flashlight [F] to keep his sanity at a healthy level!", 5);

        }
        public void BatteryLow()
        {
            UpdateText("Flashlights battery is soon depleted! Remember to use it resourcefully", 5);
        }
    }
}
