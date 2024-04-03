using System.Collections;
using UnityEngine;

namespace SojaExiles
{
    public class PlayerMovement : MonoBehaviour
    {
        public Transform cam;
        public CharacterController controller;
        public GameObject flashlight;

        private float originalCamY; // Store the original camera Y position


        private float walkingSpeed;
        private float crouchSpeed;
        private float sprintSpeed;

        private float currentSpeed;
        private float gravity = -15f;
        private float crouchDepth = 1f;

        // Headbob variables
        private float headbobSpeed = 14f;
        private float headbobAmount = 0.05f;
        private float headbobTimer = 0.0f;

        Vector3 velocity;
        private bool isCrouching = false;
        private float currentCamY; // Track the current camera Y position, considering crouch

        private void Start()
        {
            originalCamY = cam.localPosition.y; // Store the original Y position of the camera
            currentCamY = originalCamY; // Initialize currentCamY with originalCamY
            walkingSpeed = 3.0f;
            crouchSpeed = 1.5f;
            sprintSpeed = 4.5f;
            currentSpeed = walkingSpeed;
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && !isCrouching)
            {
                currentSpeed = sprintSpeed;
            }

            if (Input.GetKeyUp(KeyCode.LeftShift) && !isCrouching)
            {
                currentSpeed = walkingSpeed;
            }

            if (Input.GetKeyDown(KeyCode.LeftControl) && !isCrouching)
            {
                isCrouching = true;
                currentSpeed = crouchSpeed;
                StartCoroutine(SmoothCrouch(originalCamY - crouchDepth));
            }
            else if (Input.GetKeyUp(KeyCode.LeftControl) && isCrouching)
            {
                isCrouching = false;
                currentSpeed = walkingSpeed;
                StartCoroutine(SmoothCrouch(originalCamY));
            }


            if (Input.GetKeyDown(KeyCode.F))
            {
                flashlight.SetActive(!flashlight.activeSelf);
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                StartCoroutine(FlickerFlashlight());
            }

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;

            // Only apply headbob if there's movement
            if (move.magnitude > 0)
            {
                Headbob();
            }
            else
            {
                headbobTimer = 0.0f; // Reset headbob timer when not moving
            }

            controller.Move(move * currentSpeed * Time.deltaTime);

            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);
        }

        IEnumerator SmoothCrouch(float targetYPosition)
        {
            float time = 0f;
            float duration = 0.3f; // Transition duration in seconds
            Vector3 startCamPos = cam.localPosition;
            Vector3 targetCamPos = new Vector3(cam.localPosition.x, targetYPosition, cam.localPosition.z);

            while (time < duration)
            {
                cam.localPosition = Vector3.Lerp(startCamPos, targetCamPos, time / duration);
                time += Time.deltaTime;
                yield return null;
            }

            cam.localPosition = targetCamPos; // Ensure the camera reaches the target position
            currentCamY = targetYPosition; // Update currentCamY based on crouching or standing
        }

        private void Headbob()
        {
            float waveslice = Mathf.Sin(headbobTimer);
            headbobTimer += Time.deltaTime * headbobSpeed;
            if (headbobTimer > Mathf.PI * 2)
            {
                headbobTimer -= Mathf.PI * 2;
            }

            if (waveslice != 0)
            {
                float translateChange = waveslice * headbobAmount;
                float totalAxes = Mathf.Abs(Input.GetAxis("Horizontal")) + Mathf.Abs(Input.GetAxis("Vertical"));
                totalAxes = Mathf.Clamp(totalAxes, 0.0f, 1.0f);
                translateChange = totalAxes * translateChange;

                // Apply headbob effect based on current camera Y position, considering crouch
                cam.localPosition = new Vector3(cam.localPosition.x, currentCamY + translateChange, cam.localPosition.z);
            }
        }

        IEnumerator FlickerFlashlight()
        {
            Light flashlightLight = flashlight.GetComponent<Light>();
            if (flashlightLight == null) yield break; // Exit if no Light component found

            float flickerDuration = 4.0f; // Total duration of the flicker effect
            float startTime = Time.time;

            while (Time.time - startTime < flickerDuration)
            {
                // Turn the flashlight off for a brief moment
                flashlightLight.enabled = false;
                yield return new WaitForSeconds(Random.Range(0.05f, 0.2f)); // Off duration

                // Turn the flashlight back on
                flashlightLight.enabled = true;
                yield return new WaitForSeconds(Random.Range(0.1f, 0.3f)); // On duration
            }

            // Ensure the flashlight is on after flickering
            flashlightLight.enabled = true;
        }

    }
}