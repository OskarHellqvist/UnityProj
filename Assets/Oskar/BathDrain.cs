using SojaExiles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BathDrain : MonoBehaviour
{
    public float moveAmount = 2f;  // Distance the painting will move
    public float moveDuration = 15f; // Duration of the movement in seconds

    [SerializeField] private GameObject Note;
    [SerializeField] private AudioSource drainAudio;

    public bool hasMoved = false;
    public bool isMoving = false;

    public void Interact()
    {
        if (Note.activeSelf && !isMoving)
        {
            Vector3 moveDirection = hasMoved ? Vector3.up * moveAmount : Vector3.down  * moveAmount;
            StartCoroutine(DrainAndDissapear(moveDirection, moveDuration));
            hasMoved = !hasMoved; // Toggle state
        }
    }

    public void Update()
    {
        if (hasMoved && Time.timeScale != 0 && !drainAudio.isPlaying)
        {
            drainAudio.Play();
        }
    }

    private IEnumerator DrainAndDissapear(Vector3 target, float duration)
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
        Destroy(gameObject);
    }

}
