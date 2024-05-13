using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

namespace SojaExiles

{
	public class opencloseStallDoor : MonoBehaviour, Interactable
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
			openandclose.Play("OpeningStall");
			yield return new WaitForSeconds(.5f);
			open = true;
		}

		IEnumerator closing()
		{
			openandclose.Play("ClosingStall");
			yield return new WaitForSeconds(.5f);
			open = false;
		}


	}
}