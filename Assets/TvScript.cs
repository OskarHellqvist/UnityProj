using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TvScript : MonoBehaviour
{
    BoxCollider collider;
    public GameObject videoScreen;

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
        EventManager.manager.tvEvent.Invoke(); 
        Destroy(gameObject); 
        yield return new WaitForSeconds(5);    
    }
}