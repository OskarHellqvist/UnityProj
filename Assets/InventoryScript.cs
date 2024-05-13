using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{
    // Reference to the inventory UI game object
    public GameObject inventoryUI;

    // Variable to track if the inventory is currently visible
    private bool isInventoryVisible = false;

    void Start()
    {
        // Ensure the inventory UI is initially hidden
        if (inventoryUI != null)
            inventoryUI.SetActive(false);
    }

    void Update()
    {
        // Check for the "T" key press
        if (Input.GetKeyDown(KeyCode.T))
        {
            // Toggle inventory visibility
            isInventoryVisible = !isInventoryVisible;

            Debug.Log("T key pressed"); // Add debug log
                                        // Toggle inventory visibility
        
            // Set the inventory UI active state based on the visibility flag
            if (inventoryUI != null)
                inventoryUI.SetActive(isInventoryVisible);
        }
    }
}