using SojaExiles;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Pickup : MonoBehaviour, Interactable
{
    public Transform holdPos;

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
        isHeld = true;
        gameObject.transform.position = holdPos.position; // Move object to hold position
        gameObject.transform.parent = holdPos; // Optionally parent it to keep it relative to player/camera movement
    }

    public void ItemDrop()
    {
        isHeld = false;
        gameObject.transform.parent = null; // Detach the object from the hold position

        // Optionally apply physics if needed
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false; // Ensure physics takes over again if it was previously set to kinematic
            // Optionally apply some force or simply let gravity take its course
        }
    }
}