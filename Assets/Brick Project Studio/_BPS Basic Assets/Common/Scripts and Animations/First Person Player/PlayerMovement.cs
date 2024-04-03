using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SojaExiles

{
    public class PlayerMovement : MonoBehaviour
    {
        public Transform cam;

        public CharacterController controller;

        public GameObject flashlight;

        public float walkingSpeed;
        public float crouchSpeed;
        public float currentSpeed;

        public float gravity = -15f;

        Vector3 velocity;

        private void Start()
        {
            walkingSpeed = 3f;
            crouchSpeed = 1.5f;
            currentSpeed = walkingSpeed;
        }

        // Update is called once per frame
        void Update()
        {

            if (Input.GetKeyDown(KeyCode.LeftControl)) 
            {
                cam.transform.position -= new Vector3(0,1f,0);

                currentSpeed = crouchSpeed;
            }

            else if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                cam.transform.position += new Vector3(0,1f,0);

                currentSpeed = walkingSpeed;
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                if(flashlight.active)
                {
                    flashlight.SetActive(false);
                }
                else
                {
                    flashlight.SetActive(true);
                }
            }

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;

            controller.Move(move * currentSpeed * Time.deltaTime);

            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);

        }
    }
}