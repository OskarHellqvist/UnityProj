using SojaExiles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableFlipL: MonoBehaviour, Interactable
{

	public Animator FlipL;
	public bool open;
	public Transform Player;

    public bool locked = false;

    void Start (){
		open = false;
	}

    public void Interact()
    {
        if (open == false && !locked)
        {
            StartCoroutine(opening());
        }
        else if (open == true)
        {
            StartCoroutine(closing());
        }
    }

    public void OpenDoor() { StartCoroutine(opening()); }
    public void CloseDoor() { StartCoroutine(closing()); }
    public void LockDoor() { locked = true; }
    public void UnlockDoor() { locked = false; }
    public void LockUnlockDoor() { locked = !locked; }


    IEnumerator opening(){
		print ("you are opening the door");
        FlipL.Play ("Lup");
		open = true;
		yield return new WaitForSeconds (.5f);
	}

	IEnumerator closing(){
		print ("you are closing the door");
        FlipL.Play ("Ldown");
		open = false;
		yield return new WaitForSeconds (.5f);
	}


}

