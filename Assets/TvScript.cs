using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TvScript : MonoBehaviour
{
    BoxCollider collider;
    public GameObject videoScreen;
    public GameObject blackScreen;

    void Start()
    {
        collider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (collider != null)
        {
            ActivateTvEventTemporarily();
        }
    }

    public void ActivateTvEventTemporarily()
    {
        EventManager.manager.tvEvent.Invoke();
        Invoke("ReplaceTv", 5);


        Destroy(collider);
    }

    private void ReplaceTv()
    {
        Destroy(videoScreen);
        blackScreen.SetActive(true);
    }

}