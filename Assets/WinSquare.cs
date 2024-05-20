using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinSquare : MonoBehaviour
{

    public GameObject amulet;
    BoxCollider collider;
    public GameObject wineffect;

    public GameObject Door;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other = amulet.GetComponent<BoxCollider>())
        {
            EventManager.manager.winEvent.Invoke();
            wineffect.SetActive(true);

            //SFX
            //SFX
        }
    }
}
