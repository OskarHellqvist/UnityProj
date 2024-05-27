using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

namespace SojaExiles

{
	public class opencloseDoor1 : MonoBehaviour, Interactable
	{

		public Animator openandclose1;
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
			openandclose1.Play("Opening 1");
			yield return new WaitForSeconds(.5f);
			open = true;
		}

		IEnumerator closing()
		{
			openandclose1.Play("Closing 1");
			yield return new WaitForSeconds(.5f);
			open = false;
		}


	}
}