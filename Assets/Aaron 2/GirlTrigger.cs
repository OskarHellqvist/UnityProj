using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirlTrigger : MonoBehaviour
{
    BoxCollider collider;
    void Start()
    {
        collider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter");
        if (collider != null && other.gameObject.tag == "Player")
        {
            //EventManager.playerInstance.girlEvent.Invoke();
            Destroy(gameObject);
        }
    }
}
