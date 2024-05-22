using System.Collections;
using UnityEngine;

namespace SojaExiles
{
    public class PlayerMovement : MonoBehaviour
    {
        public Transform cam;
        public CharacterController controller;
        public GameObject flashlight;
        public GameObject ambLight;

        private float originalCamY; // Store the original camera Y position
        private float currentCamY; // Track the current camera Y position, considering crouch

        public float battery = 100;
        private float batteryDepletionRate = 0.5f;
        private bool isFlickering = false;
        private float maxIntensity = 1f; // Max intensity of the flashlight
        private float minIntensity = 0.05f; // Min intensity of the flashlight when battery is low

        private float walkingSpeed;
        private float crouchSpeed;
        private float sprintSpeed;
        private bool isSprinting = false;

        private float stamina = 100;
        private float staminaDepletionRate = 20;
        private float staminaIncreseRate = 20;

        private float standingHeight; // Height when standing
        private float crouchingHeight; // Height when crouching

        private float currentSpeed;
        private float gravity = -15f;
        private float crouchDepth = 0.8f;

        // Headbob variables
        private float headbobSpeed = 14f;
        private float headbobAmount = 0.05f;
        private float headbobTimer = 0.0f;

        Vector3 velocity;

        private bool crouched = false, isCurrentlyCrouching = false;

        private void Start()
        {
            originalCamY = cam.localPosition.y; // Store the original Y position of the camera
            currentCamY = originalCamY; // Initialize currentCamY with originalCamY
            walkingSpeed = 3.0f;
            crouchSpeed = 1.5f;
            sprintSpeed = 4.5f;
            currentSpeed = walkingSpeed;
            standingHeight = originalCamY; // Set standing height to the original Y position of the camera
            crouchingHeight = originalCamY - crouchDepth; // Set crouching height based on crouch depth
        }

        void Update()
        {
            Debug.Log($"Sanity: {SanityManager.playerInstance.Sanity}");

            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                ToggleCrouch();
            }

            if (Input.GetKeyDown(KeyCode.LeftShift) && !crouched)
            {
                isSprinting = true;
                currentSpeed = sprintSpeed;
            }

            if (Input.GetKeyUp(KeyCode.LeftShift) && !crouched)
            {
                isSprinting = false;
                currentSpeed = walkingSpeed;
            }

            if (isSprinting)
            {
                stamina -= staminaDepletionRate * Time.deltaTime;

                if (stamina < 0)
                {
                    isSprinting = false;
                    currentSpeed = walkingSpeed;
                }
            }

            if (!isSprinting)
            {
                stamina += staminaIncreseRate * Time.deltaTime;
            }

            //--------------------------------------------------------------------------------I

            if (Input.GetKeyDown(KeyCode.F))
            {
                ToggleLights(!flashlight.activeSelf);  // Pass the opposite of the current state
            }

            if (flashlight.activeSelf)
            {
                // Decrease battery only when flashlight is on
                battery -= batteryDepletionRate * Time.deltaTime;
                battery = Mathf.Max(battery, 0); // Ensure battery doesn't go below 0

                // Adjust flashlight intensity based on battery level
                Light flashlightLight = flashlight.GetComponent<Light>();

                if (flashlightLight != null)
                {
                    float batteryPct = battery / 100f; // Convert battery to a 0-1 scale
                    flashlightLight.intensity = Mathf.Lerp(minIntensity, maxIntensity, batteryPct);

                    if (ambLight)
                    {
                        Light ambientLight = ambLight.GetComponent<Light>();

                        ambientLight.intensity = Mathf.Lerp(minIntensity, maxIntensity, batteryPct);
                    }
                }

                if (battery <= 0 && !isFlickering)
                {
                    StartCoroutine(FlickerFlashlight(4));
                }

                if (battery <= 50 && battery >= 49 && !isFlickering)
                {
                    StartCoroutine(FlickerFlashlight(2));
                }
            }

            //--------------------------------------------------------------------------------I

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

        private void ToggleCrouch()
        {
            if (isCurrentlyCrouching) return;
            isCurrentlyCrouching = true;

            crouched = !crouched;
            currentSpeed = crouched ? crouchSpeed : walkingSpeed;
            float targetYPosition = crouched ? crouchingHeight : standingHeight;
            StartCoroutine(SmoothCrouch(targetYPosition));
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

            isCurrentlyCrouching = false;
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

        private void ToggleLights(bool state)
        {
            flashlight.SetActive(state);
            if (ambLight)
                ambLight.SetActive(state);
        }


        IEnumerator FlickerFlashlight(int duration)
        {
            if (isFlickering) yield break;
            isFlickering = true;

            Light flashlightLight = flashlight.GetComponent<Light>();
            Light ambientLight = ambLight.GetComponent<Light>();

            if (flashlightLight == null)
            {
                isFlickering = false;
                yield break; // Exit if no Light component found
            }

            float startTime = Time.time;

            while (Time.time - startTime < duration)
            {
                // Turn the flashlight off for a brief moment
                flashlightLight.enabled = false;
                ambientLight.enabled = false;
                yield return new WaitForSeconds(Random.Range(0.05f, 0.5f)); // Off duration

                // Turn the flashlight back on
                flashlightLight.enabled = true;
                ambientLight.enabled = true;
                yield return new WaitForSeconds(Random.Range(0.05f, 0.15f)); // On duration
            }

            // Ensure the flashlight is on after flickering
            flashlightLight.enabled = true;
            ambientLight.enabled = true;
            isFlickering = false;
        }

    }
}