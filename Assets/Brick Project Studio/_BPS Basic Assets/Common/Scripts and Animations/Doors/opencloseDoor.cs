using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SojaExiles

{
	public class opencloseDoor : MonoBehaviour, Interactable
	{
		private bool doorOpen;
		private bool doorClose;
		private bool openLockedDoor;
		private bool doorUnlock;


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
            if (open == false)
            {
                OpenDoor();
            }
            else if (open == true)
            {
                CloseDoor();
            }
        }

		public void OpenDoor() 
		{
			if (locked)
			{
				if(openLockedDoor != null)
				{
                    FindObjectOfType<AudioManager>().Play("LockedDoor");
                }
            }
			else
			{
				if(doorOpen != null)
				{
					FindObjectOfType<AudioManager>().Play("OpenDoor");
				}
                
                StartCoroutine(opening());
            }
		}
		public void CloseDoor() 
		{
			if(doorClose != null)
			{
                FindObjectOfType<AudioManager>().Play("CloseDoor");
            }

            StartCoroutine(closing()); 
		}
		public void LockDoor() 
		{ 
			locked = true; 
		}
		public void UnlockDoor() 
		{

            locked = false; 
		}
		public void LockUnlockDoor() { locked = !locked; }

		IEnumerator opening()
		{
			//print("you are opening the door");
			openandclose.Play("Opening");
            FindObjectOfType<AudioManager>().Play("OpenDoor");
            yield return new WaitForSeconds(.5f);
			open = true;
		}

		IEnumerator closing()
		{
			//print("you are closing the door");
			openandclose.Play("Closing");
            FindObjectOfType<AudioManager>().Play("CloseDoor");
            yield return new WaitForSeconds(.5f);
			open = false;
		}


	}
}