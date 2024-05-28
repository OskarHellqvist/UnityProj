using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

namespace SojaExiles

{
	public class BRGlassDoor : MonoBehaviour, Interactable
	{

		public Animator openandclose;
		public bool open;
		public Transform Player;

        public bool locked = false;

		private bool isMoving;

        void Start()
		{
			open = false;
		}

        public void Interact()
        {
			if (!isMoving)
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
        }

        public void OpenDoor() { StartCoroutine(opening()); }
        public void CloseDoor() { StartCoroutine(closing()); }
        public void LockDoor() { locked = true; }
        public void UnlockDoor() { locked = false; }
        public void LockUnlockDoor() { locked = !locked; }

        IEnumerator opening()
		{
			isMoving = true;
			openandclose.Play("BRGlassDoorOpen");
			open = true;
			yield return new WaitForSeconds(1f);
			isMoving = false;
		}

		IEnumerator closing()
		{
            isMoving = true;
            openandclose.Play("BRGlassDoorClose");
			open = false;
			yield return new WaitForSeconds(1f);
            isMoving = false;
        }


	}
}