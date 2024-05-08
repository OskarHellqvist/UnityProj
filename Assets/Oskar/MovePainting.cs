using UnityEngine;
using System.Collections;

namespace SojaExiles
{
    public class MovePainting : MonoBehaviour, Interactable
    {
        public float moveAmount = 0.5f;  // Distance the painting will move
        public float moveDuration = 0.5f; // Duration of the movement in seconds

        [SerializeField] private GameObject Note;
        [SerializeField] private Material transMaterial; // Material with the transparent shader

        public bool isLifted = false;
        public bool isMoving = false;

        [SerializeField] private AudioSource audioSource;
        [SerializeField] private SanityManager sanity; // Reference to the SanityManager script

        public void Interact()
        {
            if (Note.activeSelf && !isMoving)
            {
                Vector3 moveDirection = isLifted ? Vector3.down * moveAmount : Vector3.up * moveAmount;
                StartCoroutine(MovePaintingSmoothly(moveDirection, moveDuration));
                isLifted = !isLifted; // Toggle state
            }
        }

        void Start()
        {
            SetInitialTransparency(); // Set initial transparency of the painting
        }

        void Update()
        {
            if (Note)
            {
                if (!Note.activeSelf && isLifted && !isMoving)
                {
                    StartCoroutine(MovePaintingSmoothly(Vector3.down * moveAmount, moveDuration));
                    isLifted = false;
                }
            }

            Debug.Log(sanity.Sanity);

            if (sanity != null) // Check if the insanityImage component is attached
            {
                UpdateTransparencyBasedOnSanity(); // Continuously update transparency based on sanity
            }
            else
            {
                Debug.Log("Insanity component not found!");
            }
        }


        private IEnumerator MovePaintingSmoothly(Vector3 target, float duration)
        {
            isMoving = true;
            audioSource.Play();
            Vector3 startPosition = transform.position;
            Vector3 endPosition = startPosition + target;
            float time = 0;

            while (time < duration)
            {
                transform.position = Vector3.Lerp(startPosition, endPosition, time / duration);
                time += Time.deltaTime;
                yield return null;
            }

            transform.position = endPosition;
            isMoving = false;
        }

        private void UpdateTransparencyBasedOnSanity()
        {
            float sanity = this.sanity.Sanity;
            float visibility = 1 - (sanity / 100);
            Color color = transMaterial.color;
            
            color.a = Mathf.Clamp(visibility, 0, 1);
            transMaterial.color = color;
            Debug.Log("Updated Transparency: " + color.a); // Debugging output
        }

        private void SetInitialTransparency()
        {
            // Set initial alpha to 0 (completely transparent)
            Color initialColor = transMaterial.color;
            initialColor.a = 0;
            transMaterial.color = initialColor;
        }
    }
}