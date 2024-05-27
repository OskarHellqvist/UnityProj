using TMPro;
using UnityEngine;

namespace SojaExiles
{

    // Made by Didrik
    public class InventoryScript : MonoBehaviour
    {
        public static InventoryScript instance;
        // Reference to the inventory UI game object
        public GameObject inventoryUI;
        // Reference to the player movement script
        public PlayerMovement playerMovementScript;

        [Header("Battery Manager")]
        [SerializeField] private TMP_Text batteryCounter;
        private int batteries = 0;        
        
        // References to the key images UI elements
        [SerializeField] private GameObject[] keyImagesUI = new GameObject[2];

        [SerializeField] private KeyCode inventoryKey = KeyCode.Tab;
        [SerializeField] private KeyCode useBatteryKey = KeyCode.R;

        // Variable to track if the inventory is currently visible
        private bool isInventoryVisible = false;
        public opencloseDoor nextDoorToOpen {  get; private set; }



        void Start()
        {
            // Ensure the inventory UI is initially hidden
            if (inventoryUI != null)
            {
                inventoryUI.SetActive(false);
            }

            // Ensure all key images UI are initially hidden
            foreach (var keyImage in keyImagesUI)
            {
                if (keyImage != null)
                {
                    keyImage.SetActive(false);
                }
            }

            instance = this;

        }

        void Update()
        {
            if (batteryCounter != null)
            {
                batteryCounter.text = batteries.ToString();
            }

            // Check for the "T" key press
            if (Input.GetKeyDown(inventoryKey))
            {
                
                ShowInventory();
                
            }
            else if (Input.GetKeyUp(inventoryKey))
            {
                HideInventory();
            }
            
            if (Input.GetKeyDown(useBatteryKey))
            {
                UseBattery();
            }
        }


        public void ClearDoor()
        {
            nextDoorToOpen = null;
        }

        void ShowInventory()
        {
            isInventoryVisible = true;

            // Enable inventory UI
            if (inventoryUI != null)
            {
                inventoryUI.SetActive(true);
            }

        }

        void HideInventory()
        {
            isInventoryVisible = false;

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
                //Debug.LogWarning("No batteries available.");
            }
        }

        // Method to toggle a specific key image visibility
        public void PickUpKey(int keyIndex, opencloseDoor Doorscript)
        {
            if (keyIndex >= 0 && keyIndex < keyImagesUI.Length && keyImagesUI[keyIndex] != null)
            {
                keyImagesUI[keyIndex].SetActive(true);

            }
            nextDoorToOpen = Doorscript;
        }
    }
}
