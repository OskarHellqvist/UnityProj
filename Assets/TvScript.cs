using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TvScript : MonoBehaviour
{
    BoxCollider collider;

    void Start()
    {
        collider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (collider != null)
        {
            StartCoroutine(ActivateTvEventTemporarily());
        }
    }

    IEnumerator ActivateTvEventTemporarily()
    {
        EventManager.manager.tvEvent.Invoke(); // Invoke the event
        yield return new WaitForSeconds(5);    // Wait for 5 seconds
        // You can add code here to deactivate the event or handle the aftermath
        Debug.Log("TV event deactivated");
        Destroy(gameObject); // Optionally destroy the object after the event is over
    }
}