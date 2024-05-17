using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteItem : BaseItem
{
    public string noteText;
    public override void PickUp()
    {
        base.PickUp(); // Call the base class method first then add additional behaviour
        //Debug.Log("Note content: " + noteText);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
