using UnityEngine;
using System.Collections;

namespace SojaExiles
{
    public class MovePainting : MonoBehaviour, Interactable
    {
        public float moveAmount = 0.5f;  // Distance the painting will move
        public float moveDuration = 0.5f; // Duration of the movement in seconds

        [SerializeField] private GameObject Note;

        public bool isLifted = false;
        public bool isMoving = false;

        [SerializeField] private AudioSource audioSource;

        public void Interact()
        {
            // Check if a note is active and no other movement is in progress
            if (Note.activeSelf && !isMoving)
            {
                // Determine direction based on whether the painting is already lifted
                Vector3 moveDirection = isLifted ? Vector3.down * moveAmount : Vector3.up * moveAmount;
                StartCoroutine(MovePaintingSmoothly(moveDirection, moveDuration));
                isLifted = !isLifted; // Toggle state
            }
        }



        void Update()
        {
            // Move the painting down only once when the note becomes inactive

            if (!Note.activeSelf && isLifted && !isMoving)
            {
                StartCoroutine(MovePaintingSmoothly(Vector3.down * moveAmount, moveDuration));
                isLifted = false;

            }


        }

        private IEnumerator MovePaintingSmoothly(Vector3 target, float duration)
        {
            isMoving = true; // Lock further interactions
            audioSource.Play();
            Vector3 startPosition = transform.position;
            Vector3 endPosition = startPosition + target;
            float time = 0;

            while (time < duration)
            {
                transform.position = Vector3.Lerp(startPosition, endPosition, time / duration);
                time += Time.deltaTime;
                yield return null; // Wait until next frame
            }

            transform.position = endPosition; // Ensure the painting exactly reaches the target position

            isMoving = false; // Unlock interaction
        }
    }
}