using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject head;
    public Animator headAnimator;
    public AudioSource audioSource;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            headAnimator.SetTrigger("SpinTrigger");
            audioSource.Play();
        }
    }
}
