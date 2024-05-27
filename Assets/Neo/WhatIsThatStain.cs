using SojaExiles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhatIsThatStain : MonoBehaviour, Interactable
{
    public void Interact()
    {
       FindObjectOfType<AudioManager2>().Play("MarcoStain", transform.position);
    }
}
