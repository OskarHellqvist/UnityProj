using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SojaExiles

{
    public class PlayerMovement : MonoBehaviour
    {

        public CharacterController controller;

        public GameObject camera;

        public float walkingSpeed;
        public float crouchSpeed;
        public float currentSpeed;

        public float gravity = -15f;

        Vector3 velocity;

        bool isGrounded;

        private void Start()
        {
            walkingSpeed = 5f;
            crouchSpeed = 2f;
            currentSpeed = walkingSpeed;
        }

        // Update is called once per frame
        void Update()
        {

            if (Input.GetKeyDown(KeyCode.LeftControl)) 
            {
                camera.transform.position -= new Vector3(0,1f,0);

                currentSpeed = crouchSpeed;
            }

            else if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                camera.transform.position += new Vector3(0,1f,0);

                currentSpeed = walkingSpeed;
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