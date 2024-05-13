using SojaExiles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceInteraction : MonoBehaviour, Interactable
{
    private PieceScript piece;

    public void Start()
    {
        piece = GetComponent<PieceScript>();
    }

    public void Interact()
    {
        piece.Select();
    }
}
