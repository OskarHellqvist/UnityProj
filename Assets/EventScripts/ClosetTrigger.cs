using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosetTrigger : MonoBehaviour
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
            EventManager.manager.mannequinEvent1.Invoke();
            Destroy(gameObject);
        }
    }
}
