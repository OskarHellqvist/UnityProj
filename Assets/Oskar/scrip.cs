using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    BoxCollider collider;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (collider != null)
        {
            FadeToEndScreen();
        }
    }

    public void FadeToEndScreen()
    {
        Global.LoadSceneWin();
        Destroy(gameObject);
    }

}
