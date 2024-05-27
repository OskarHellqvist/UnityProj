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
        private bool isAnimating;

        public bool locked = false;

        void Start()
        {
            open = false;
        }

        public void Interact()
        {
            if (!isAnimating)
            {
                if (!open && !locked)
                {
                    StartCoroutine(opening());
                }
                else if (open)
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
            isAnimating = true;
            pull_01.Play("openpull_01");
            open = true;
            FindObjectOfType<AudioManager2>().Play("OpenDrawer", transform.position);
            Debug.Log("Open");
            yield return new WaitForSeconds(1f);
            isAnimating = false;
        }

        IEnumerator closing()
        {
            isAnimating = true;
            pull_01.Play("closepush_01");
            open = false;
            FindObjectOfType<AudioManager2>().Play("CloseDrawer", transform.position);
            yield return new WaitForSeconds(1f);
            isAnimating = false;
        }


    }
}