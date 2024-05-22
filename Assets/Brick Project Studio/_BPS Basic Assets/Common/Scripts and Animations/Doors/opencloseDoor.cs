using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SojaExiles

{
	public class opencloseDoor : MonoBehaviour, Interactable
	{
		[SerializeField] private AudioClip doorOpen;
		[SerializeField] private AudioClip doorClose;
		[SerializeField] private AudioClip openLockedDoor;
		[SerializeField] private AudioClip doorUnlock;
		[SerializeField] private AudioSource DoorAudio;

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
					DoorAudio.clip = openLockedDoor;
                    FindObjectOfType<AudioManager2>().Play("LockedDoor", transform.position);
                    //DoorAudio.Play();
                }
            }
			else
			{
				if(doorOpen != null)
				{
					DoorAudio.clip = doorOpen;
                    FindObjectOfType<AudioManager2>().Play("OpenDoor", transform.position);
                    //DoorAudio.Play();
                }

                StartCoroutine(opening());
            }
		}
		public void CloseDoor() 
		{
			if(doorClose != null)
			{
				DoorAudio.clip = doorClose;
                //DoorAudio.Play();
                FindObjectOfType<AudioManager2>().Play("CloseDoor", transform.position);
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
            DoorAudio.clip = doorUnlock;
            //DoorAudio.Play();
            locked = false; 
		}
		public void LockUnlockDoor() { locked = !locked; }

		IEnumerator opening()
		{
			//print("you are opening the door");
			openandclose.Play("Opening");
			yield return new WaitForSeconds(.5f);
			open = true;
		}

		IEnumerator closing()
		{
			//print("you are closing the door");
			openandclose.Play("Closing");
            yield return new WaitForSeconds(.5f);
			open = false;
		}


	}
}