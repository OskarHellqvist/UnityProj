using SojaExiles;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace SojaExiles
{
    public class MovePillow : MonoBehaviour, Interactable
    {
        public float moveAmount = 1f;  // Distance the painting will move
        public float moveDuration = 0.5f; // Duration of the movement in seconds

        [SerializeField] private GameObject Note;

        public bool hasMoved = false;
        public bool isMoving = false;

        public void Interact()
        {
            if (Note.activeSelf && !isMoving)
            {
                Vector3 moveDirection = hasMoved ? Vector3.right * moveAmount : Vector3.left * moveAmount;
                StartCoroutine(MovePillowSmoothly(moveDirection, moveDuration));
                hasMoved = !hasMoved; // Toggle state
            }
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (!Note.activeSelf && hasMoved && !isMoving)
            {
                StartCoroutine(MovePillowSmoothly(Vector3.right * moveAmount, moveDuration));
                hasMoved = false;
            }
        }

        private IEnumerator MovePillowSmoothly(Vector3 target, float duration)
        {
            isMoving = true;
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
            Note.SetActive(true);
        }

    }
}
