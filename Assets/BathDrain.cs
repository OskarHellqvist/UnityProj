using SojaExiles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BathDrain : MonoBehaviour, Interactable
{
    public float moveAmount = 2f;  // Distance the painting will move
    public float moveDuration = 15f; // Duration of the movement in seconds

    [SerializeField] private GameObject Note;

    public bool hasMoved = false;
    public bool isMoving = false;

    public void Interact()
    {
        if (Note.activeSelf && !isMoving)
        {
            Vector3 moveDirection = hasMoved ? Vector3.up * moveAmount : Vector3.down  * moveAmount;
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
            StartCoroutine(MovePillowSmoothly(Vector3.down * moveAmount, moveDuration));
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
        
    }

}
