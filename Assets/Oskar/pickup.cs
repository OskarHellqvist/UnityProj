using SojaExiles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour, Interactable
{
    public Transform holdPos;  // The position where the object is held when picked up

    private static Pickup currentlyHeldItem = null;  // Static variable to track the currently held item
    private bool isHeld = false; // To track if this specific object is currently being held

    void Update()
    {
        if (isHeld && Input.GetKeyDown(KeyCode.Q))
        {
            ItemDrop();
        }
    }

    public void Interact()
    {
        // Check if nothing is held or if this item is already the one being held
        if (!isHeld && currentlyHeldItem == null)
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

        currentlyHeldItem = this;  // Set this item as the currently held item
        isHeld = true;
        gameObject.transform.position = holdPos.position;
        gameObject.transform.rotation = holdPos.rotation;
        gameObject.transform.parent = holdPos;
    }

    public void ItemDrop()
    {
        if (isHeld)
        {
            currentlyHeldItem = null;  // Clear the currently held item
            isHeld = false;
            gameObject.transform.parent = null;

            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
            }
        }
    }

    public bool IsHeld()
    {
        return isHeld;
    }
}