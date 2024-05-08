using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoText : MonoBehaviour
{
    [SerializeField] GameObject text;
    [SerializeField] KeyCode key;

    private void Update()
    {
        if (Input.GetKeyDown(key))
        {
            Destroy(text);
        }
    }
}
