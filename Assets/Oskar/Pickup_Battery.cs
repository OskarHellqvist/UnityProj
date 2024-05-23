using UnityEngine;

namespace SojaExiles
{
    public class Pickup_Battery : MonoBehaviour, Interactable
    {
        [SerializeField] private GameObject Player;
        private PlayerMovement pMovement; // Assign this in the Inspector or find it on the player GameObject

        public void Interact()
        {
            BatteryPickup();
        }

        private void Start()
        {
            // Ensure that PlayerMovement is assigned correctly
            if (Player == null)
            {
                Debug.LogError("Player Transform is not assigned on Pickup_Battery script.");
            }
            else
            {
                pMovement = Player.GetComponent<PlayerMovement>();
            }
        }

        public void BatteryPickup()
        {
            pMovement.battery = 100; // Recharge the battery
            gameObject.SetActive(false); // Optionally deactivate the battery object after picking up
        }

    }
}