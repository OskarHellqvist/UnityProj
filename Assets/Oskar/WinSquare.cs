using SojaExiles;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WinSquare : MonoBehaviour
{

    public GameObject amulet;
    public GameObject falseAmulet1;
    public GameObject falseAmulet2;

    public GameObject wineffect;
    public AudioSource winSound;
    public GameObject burnFx;
    public Transform burnPos;

    private BoxCollider collider;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == amulet)
        {
            Pickup pickupScript = amulet.GetComponent<Pickup>();

            if (pickupScript != null && !pickupScript.IsHeld())
            {
                wineffect.SetActive(true);
                burnFx.SetActive(true);
                winSound.Play();
                amulet.transform.position = burnPos.position;

                collider.enabled = false;

                Invoke("DestroyAmulet", 1);
            }
        }
        else if (other.gameObject == (falseAmulet1 || falseAmulet2))
        {
            SanityManager.manager.DamageSanity(25f);
        }


    }
    void DestroyAmulet()
    {
        if (amulet != null)
        {

            Destroy(amulet);
            EventManager.manager.winEvent.Invoke();
        }
    }
}
