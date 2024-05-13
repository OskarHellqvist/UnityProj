using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneTimeTrigger : MonoBehaviour
{
    BoxCollider collider;
    void Start()
    {
        collider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (collider != null && other.gameObject.tag == "Player")
        {
            EventManager.manager.entryEvent.Invoke();
            Destroy(gameObject);
        }
    }
}
