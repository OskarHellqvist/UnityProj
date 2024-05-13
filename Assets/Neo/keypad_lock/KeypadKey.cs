using SojaExiles;
using System;
using UnityEngine;

public class KeypadKey : MonoBehaviour, Interactable // Ensure Interactable is a correct base class that can be used here
{
    public KeypadLock keypadLock;
    public int num = 99; // Public so it can be set from outside, especially in the Unity Inspector

    public void Interact()
    {
        if (keypadLock)
        {
            if (num >= 0 && num <= 9) keypadLock.AddNumber(num.ToString()); // Keys (0-9)
            else if (num == 10) keypadLock.ClearNumbers(); // Clear key
            else if (num == 11) keypadLock.Enter(); // Enter key
            else
            {
                Debug.LogError("No compatible number");
            }
        }
    }
}
