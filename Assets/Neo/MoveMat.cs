using System.Collections;
using UnityEngine;

namespace SojaExiles
{
    public class MoveMat : MonoBehaviour, Interactable
    {
        [SerializeField]
        GameObject Key;

        private float rotationAmount = 20f; // Amount to rotate in degrees per key press
        private float rotationSpeed = 100f; // Speed of the rotation
        private float moveDistance = 0.5f; // Distance to move per key press
        private float moveSpeed = 2f; // Speed of the movement

        private bool m_IsRotatingAndMoving = false; // Tracks if the mat is currently rotating and moving
        public bool Active = false;
        private bool Done = false;

        // Update is called once per frame
        public void Interact()
            {
                //Set (Active = true) to move mat and show key
                Active = true;
            }
        void Update()
        {
            
            // Check for the E key press and ensure the mat is not already rotating and moving
            if (!Done && Active && !m_IsRotatingAndMoving)
            {
                StartCoroutine(RotateAndMoveMat(rotationAmount, moveDistance));
                Done = true;
            }

            if(Done && (Key.active == false))
            {
                Key.active = true;
            }
        }

        private IEnumerator RotateAndMoveMat(float rotationInDegrees, float moveDistance)
        {
            m_IsRotatingAndMoving = true;

            Quaternion targetRotation = transform.rotation * Quaternion.Euler(0, rotationInDegrees, 0); // Calculate the target rotation
            Vector3 targetPosition = transform.position + (transform.right * moveDistance); // Calculate the target position

            // Keep rotating and moving until the rotation and the position are close enough to their targets
            while (Quaternion.Angle(transform.rotation, targetRotation) > 0.01f || Vector3.Distance(transform.position, targetPosition) > 0.01f)
            {
                // Perform the rotation
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                // Perform the movement
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                yield return null; // Wait for the next frame
            }

            transform.rotation = targetRotation; // Ensure the final rotation matches exactly
            transform.position = targetPosition; // Ensure the final position matches exactly
            m_IsRotatingAndMoving = false; // Reset the flag
        }
    }
}