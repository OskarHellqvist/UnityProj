using SojaExiles;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{
    // Reference to the inventory UI game object
    public GameObject inventoryUI;
    // Reference to the mouse look script
    public MouseLook mouseLookScript;
    // Variable to track if the inventory is currently visible
    private bool isInventoryVisible = false;


    [Header("Battary Manager")]
    [SerializeField] private TMP_Text battaryCounter;
    int battareis = 0;

    //[Header("Key Manager")]
    


    void Start()
    {
        // Ensure the inventory UI is initially hidden
       inventoryUI.SetActive(false);
    }

    void Update()
    {
        battaryCounter.text = battareis.ToString();

        // Check for the "T" key press
        if (Input.GetKeyDown(KeyCode.T))
        {
            // Toggle inventory visibility
            isInventoryVisible = !isInventoryVisible;

            // Toggle cursor lock state and visibility
            Cursor.lockState = isInventoryVisible ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = isInventoryVisible;

            Debug.Log("T key pressed"); // Add debug log

            // Disable/enable mouse look script
            if (mouseLookScript != null)
                mouseLookScript.enabled = !isInventoryVisible;

            // Set the inventory UI active state based on the visibility flag
            if (inventoryUI != null)
                inventoryUI.SetActive(isInventoryVisible);
        }
    }
}