using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SojaExiles

{
	public class opencloseDoor : MonoBehaviour, Interactable
	{

		public Animator openandclose;
		public bool open;
		public Transform Player;

		public bool locked = false;

		void Start()
		{
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

		IEnumerator opening()
		{
			print("you are opening the door");
			openandclose.Play("Opening");
			yield return new WaitForSeconds(.5f);
			open = true;
		}

		IEnumerator closing()
		{
			print("you are closing the door");
			openandclose.Play("Closing");
			yield return new WaitForSeconds(.5f);
			open = false;
		}


	}
}