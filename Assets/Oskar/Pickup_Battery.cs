using UnityEngine;

namespace SojaExiles
{
    public class Pickup_Battery : MonoBehaviour, Interactable
    {
        [SerializeField] private GameObject player;
        private PlayerMovement pMovement; // Assign this in the Inspector or find it on the player GameObject
        private InventoryScript inventoryScript;

        public Quotes quotes;

        public void Interact()
        {
            BatteryPickup();

        }

        private void Start()
        {
            quotes = FindObjectOfType<Quotes>();

            // Ensure that PlayerMovement is assigned correctly
            if (player == null)
            {
                Debug.LogError("Player GameObject is not assigned on Pickup_Battery script.");
            }
            else
            {
                pMovement = player.GetComponent<PlayerMovement>();
            }

            // Find InventoryScript in the scene
            inventoryScript = FindObjectOfType<InventoryScript>();
            if (inventoryScript == null)
            {
                Debug.LogError("InventoryScript is not found in the scene.");
            }
        }

        public void BatteryPickup()
        {
            gameObject.SetActive(false); // Optionally deactivate the battery object after picking up

            if (inventoryScript != null)
            {
                inventoryScript.IncrementBatteryCount();
            }

            if (quotes) quotes.InventoryPickUp();
        }
    }
}