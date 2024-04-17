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

        [SerializeField] private AudioSource audioSource;

        public void Interact()
        {
            // Toggle the movement of the painting smoothly
            if (Note.activeSelf && !isLifted)
            {
                StartCoroutine(MovePaintingSmoothly(Vector3.up * moveAmount, moveDuration));
                audioSource.Play();
                isLifted = true;
            }
            else if (Note.activeSelf && isLifted)
            {
                StartCoroutine(MovePaintingSmoothly(Vector3.down * moveAmount, moveDuration));
                audioSource.Play();
                isLifted = false;
            }
        }

        void Update()
        {
            // Move the painting down only once when the note becomes inactive
            if (!Note.activeSelf && isLifted)
            {
                StartCoroutine(MovePaintingSmoothly(Vector3.down * moveAmount, moveDuration));
                audioSource.Play();
                isLifted = false;
            }
        }

        private IEnumerator MovePaintingSmoothly(Vector3 target, float duration)
        {
            Vector3 startPosition = transform.position;  // Get the current position
            Vector3 endPosition = startPosition + target;  // Calculate the target position
            float time = 0;

            while (time < duration)
            {
                transform.position = Vector3.Lerp(startPosition, endPosition, time / duration);
                time += Time.deltaTime;  // Increment the time passed
                yield return null;  // Wait until next frame
            }

            transform.position = endPosition;  // Ensure the painting exactly reaches the target position
        }
    }
}