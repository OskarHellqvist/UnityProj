using TMPro;
using UnityEngine;

namespace SojaExiles
{
    public class InventoryScript : MonoBehaviour
    {
        // Reference to the inventory UI game object
        public GameObject inventoryUI;
        // Reference to the mouse look script
        public MouseLook mouseLookScript;
        // Reference to the player movement script
        public PlayerMovement playerMovementScript;

        // Variable to track if the inventory is currently visible
        private bool isInventoryVisible = false;

        [Header("Battery Manager")]
        [SerializeField] private TMP_Text batteryCounter;
        private int batteries = 0;

        void Start()
        {
            // Ensure the inventory UI is initially hidden
            if (inventoryUI != null)
            {
                inventoryUI.SetActive(false);
            }
        }

        void Update()
        {
            if (batteryCounter != null)
            {
                batteryCounter.text = batteries.ToString();
            }

            // Check for the "T" key press
            if (Input.GetKeyDown(KeyCode.T))
            {
                // Use if-else to toggle inventory visibility
                if (isInventoryVisible)
                {
                    HideInventory();
                }
                else
                {
                    ShowInventory();
                }
            }
        }

        void ShowInventory()
        {
            isInventoryVisible = true;

            // Unlock cursor and make it visible
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            // Disable mouse look script
            if (mouseLookScript != null)
            {
                mouseLookScript.enabled = false;
            }

            // Enable inventory UI
            if (inventoryUI != null)
            {
                inventoryUI.SetActive(true);
            }

            // Use Global.UnlockMouse to free up the mouse
            Global.UnlockMouse();
        }

        void HideInventory()
        {
            isInventoryVisible = false;

            // Lock cursor and make it invisible
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            // Enable mouse look script
            if (mouseLookScript != null)
            {
                mouseLookScript.enabled = true;
            }

            // Disable inventory UI
            if (inventoryUI != null)
            {
                inventoryUI.SetActive(false);
            }
        }

        public void IncrementBatteryCount()
        {
            batteries++;
        }

        public void UseBattery()
        {
            // Check if there are batteries available
            if (batteries > 0)
            {
                // Decrease the battery count by 1
                batteries--;

                // Recharge the player's battery
                playerMovementScript.battery = 100;
            }
            else
            {
                Debug.LogWarning("No batteries available.");
            }
        }
    }
}





