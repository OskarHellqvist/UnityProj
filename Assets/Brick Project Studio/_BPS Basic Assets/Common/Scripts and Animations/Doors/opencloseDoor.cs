using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SojaExiles

{
	public class opencloseDoor : MonoBehaviour, Interactable
	{
		//[SerializeField] private AudioClip doorOpen;
		//[SerializeField] private AudioClip doorClose;
		//[SerializeField] private AudioClip openLockedDoor;
		//[SerializeField] private AudioClip doorUnlock;
		//[SerializeField] private AudioSource DoorAudio;

		public Animator openandclose;
		public bool open;
		public Transform Player;
		
		public bool isDoor;
		public bool locked = false;

		private bool isOpening, isClosing;

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
            if (InventoryScript.instance.nextDoorToOpen == this)
            {
                InventoryScript.instance.ClearDoor();
                UnlockDoor();

				return;
            }

            if (locked)
			{
				//if(openLockedDoor != null)
				//{
				//	DoorAudio.clip = openLockedDoor;
				//                
				//                //DoorAudio.Play();
				//}
				if (isDoor) FindObjectOfType<AudioManager2>().Play("LockedDoor", transform.position);
            }
			else
			{
				//if(doorOpen != null)
				//{
				//	DoorAudio.clip = doorOpen;

				//                //DoorAudio.Play();
				//            }
				if (!isOpening)
				{
					if (isDoor) FindObjectOfType<AudioManager2>().Play("OpenDoor", transform.position);
					else FindObjectOfType<AudioManager2>().Play("OpenCabinet", transform.position);
				}

                StartCoroutine(opening());
            }
		}
		public void CloseDoor() 
		{
			//if(doorClose != null)
			//{
			//	DoorAudio.clip = doorClose;
			//             //DoorAudio.Play();
			//         }

			if (!isClosing)
			{
				if (isDoor) FindObjectOfType<AudioManager2>().Play("CloseDoor", transform.position);
				else FindObjectOfType<AudioManager2>().Play("CloseCabinet", transform.position);
			}

            StartCoroutine(closing()); 
		}
		public void OpenCloseDoor()
		{
			if (open) { CloseDoor(); }
			else if (!open) { OpenDoor(); }
		}
		public void LockDoor() 
		{ 
			locked = true; 
		}
		public void UnlockDoor() 
		{
            //DoorAudio.clip = doorUnlock;
            //DoorAudio.Play();
            FindObjectOfType<AudioManager2>().Play("DoorUnlock", transform.position);
            locked = false; 
		}
		public void LockUnlockDoor() { locked = !locked; }

		IEnumerator opening()
		{
			//print("you are opening the door");
			isOpening = true;
			openandclose.Play("Opening");
            yield return new WaitForSeconds(.5f);
			open = true;
			isOpening = false;
		}

		IEnumerator closing()
		{
			//print("you are closing the door");
			isClosing = true;
			openandclose.Play("Closing");
            yield return new WaitForSeconds(.5f);
			open = false;
			isClosing = false;
		}


	}
}