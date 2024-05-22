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
			openandclose1.Play("Opening 1");
            FindObjectOfType<AudioManager2>().Play("OpenCabinet", transform.position);
            yield return new WaitForSeconds(.5f);
			open = true;
		}

		IEnumerator closing()
		{
			openandclose1.Play("Closing 1");
            FindObjectOfType<AudioManager2>().Play("CloseCabinet", transform.position);
            yield return new WaitForSeconds(.5f);
			open = false;
		}


	}
}