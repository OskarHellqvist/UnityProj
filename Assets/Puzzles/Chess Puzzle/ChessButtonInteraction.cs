using SojaExiles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessButtonInteraction : MonoBehaviour, Interactable
{
    private ChessButton button;

    public void Start()
    {
        button = GetComponent<ChessButton>();
    }

    public void Interact()
    {
        button.Pressed();
    }
}
