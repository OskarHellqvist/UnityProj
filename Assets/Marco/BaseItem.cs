using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItem : MonoBehaviour
{
    public string itemName;
    public Sprite itemIcon;
    public bool canBePickedUp;

    public virtual void PickUp()
    {
        gameObject.SetActive(false); // Deactivates the object in the scene, pogg
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
