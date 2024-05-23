using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;

public class MirrorFall : MonoBehaviour
{
    public GameObject mirror;
    public GameObject brokeFx;

    public float moveAmount = 0.75f;  // Distance the painting will move
    public float moveDuration = 0.15f; // Duration of the movement in seconds

    public bool hasMoved = false;
    public bool isMoving = false;

    BoxCollider collider;

    public AudioSource mBreak;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (collider != null)
        {
            Vector3 moveDirection = Vector3.down * moveAmount;
            StartCoroutine(MoveMirrorSmoothly(moveDirection, moveDuration));
            hasMoved = true;
        }
    }


    private IEnumerator MoveMirrorSmoothly(Vector3 target, float duration)
    {
        isMoving = true;
        Vector3 startPosition = mirror.transform.position;
        Vector3 endPosition = startPosition + target;
        float time = 0;

        while (time < duration)
        {
            mirror.transform.position = Vector3.Lerp(startPosition, endPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        mirror.transform.position = endPosition;
        brokeFx.SetActive(true);
        mBreak.Play();
        Destroy(gameObject);
        isMoving = false;
    }


}
