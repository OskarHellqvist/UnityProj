using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

namespace SojaExiles

{

	public class Drawer_Pull_X : MonoBehaviour, Interactable
	{

		public Animator pull_01;
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
			pull_01.Play("openpull_01");
			open = true;
            FindObjectOfType<AudioManager>().Play("Drawer");
            yield return new WaitForSeconds(.5f);
		}

		IEnumerator closing()
		{
			pull_01.Play("closepush_01");
			open = false;
            FindObjectOfType<AudioManager>().Play("Drawer");
            yield return new WaitForSeconds(.5f);
		}


	}
}