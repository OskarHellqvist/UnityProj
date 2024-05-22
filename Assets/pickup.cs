using SojaExiles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour, Interactable
{
    public Transform holdPos;  // The position where the object is held when picked up

    private bool isHeld = false; // To track if the object is currently being held

    void Update()
    {
        if (isHeld && Input.GetKeyDown(KeyCode.Q))
        {
            ItemDrop();
        }

    }

    public void Interact()
    {
        if (!isHeld)
        {
            ItemPickup();
        }
    }

    public void ItemPickup()
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }

        isHeld = true;
        gameObject.transform.position = holdPos.position;
        gameObject.transform.rotation = holdPos.rotation;
        gameObject.transform.parent = holdPos;
    }

    public void ItemDrop()
    {
        isHeld = false;
        gameObject.transform.parent = null;

        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
        }
    }

    public bool IsHeld()
    {
        return isHeld;
    }

}