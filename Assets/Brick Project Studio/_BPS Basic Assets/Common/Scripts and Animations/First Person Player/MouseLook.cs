﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SojaExiles

{
    public class MouseLook : MonoBehaviour
    {
        public float mouseXSensitivity;
        [SerializeField] private Slider sensSlider;

        public Transform playerBody;

        float xRotation = 0f;

        // Start is called before the first frame update
        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            if (sensSlider) mouseXSensitivity = sensSlider.value;
        }

        // Update is called once per frame
        void Update()
        {
            if (Time.deltaTime != 0)
            {
                float mouseX = Input.GetAxis("Mouse X") * mouseXSensitivity;
                float mouseY = Input.GetAxis("Mouse Y") * mouseXSensitivity;

                xRotation -= mouseY;
                xRotation = Mathf.Clamp(xRotation, -90f, 90f);

                transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
                playerBody.Rotate(Vector3.up * mouseX);
            }
        }

        public void ChangeSensitivity()
        {
            mouseXSensitivity = sensSlider.value;
        }
            
    }
}