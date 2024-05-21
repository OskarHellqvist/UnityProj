using SojaExiles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhatIsThatStain : MonoBehaviour, Interactable
{
    public void Start()
    {
        
    }

    private void Update()
    {
        
    }

    public void Interact()
    {
        AudioManager2.instance.Play("MarcoStain");
    }
}
