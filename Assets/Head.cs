using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    public Animator scaryEvent;
    public Transform Player;

    void Start()
    {
        
    }

    void OnMouseOver()
    {  
        if (Player)
        {
            float dist = Vector3.Distance(Player.position, transform.position);
            if (dist < 3)
            {
                scaryEvent.SetBool("Play", true);
                print("you got scared");
                scaryEvent.Play("ScaryHeadAnim");
            }
        }
    }
}

