using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject flashlight;

    public Slider slider;

    public float sanity = 100;
    private float decreaseValue = 2;
    private float increaseValue = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        if (!flashlight.active)
        {
            sanity -= Time.deltaTime * decreaseValue;
        } 
        else if (sanity < 100)
        {
            sanity += Time.deltaTime * increaseValue;
        }

        if (sanity > 100) sanity = 100;

        slider.value = sanity;

    }
}
