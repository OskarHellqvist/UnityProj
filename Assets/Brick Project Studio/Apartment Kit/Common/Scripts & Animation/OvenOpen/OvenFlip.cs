using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

namespace SojaExiles

{
    public class OvenFlip : MonoBehaviour, Interactable
    {

        public Animator openandcloseoven;
        public bool open;
        public Transform Player;

        public bool locked = false;
        public bool isAnimating;

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
            openandcloseoven.Play("OpenOven");
            yield return new WaitForSeconds(1.5f);
            open = true;
            isAnimating = false;
        }

        IEnumerator closing()
        {
            isAnimating = true;
            openandcloseoven.Play("ClosingOven");
            open = false;
            yield return new WaitForSeconds(1.5f);
            isAnimating = false;
        }


    }
}