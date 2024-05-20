using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterBedroom : MonoBehaviour
{
    // Start is called before the first frame update
    BoxCollider collider;

    public GameObject pentagram;

    void Start()
    {
        collider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (collider != null && other.gameObject.tag == "Player")
        {
            EventManager.manager.masterBedroom.Invoke();
            Destroy(gameObject);

            pentagram.SetActive(true);
        }
    }
}
