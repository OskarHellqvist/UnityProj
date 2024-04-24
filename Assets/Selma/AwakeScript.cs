using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AwakeScript : MonoBehaviour
{
    public GameObject backgroundPanel;
    public Image backgroundImage;
    private float backgroundAlpha;
    // Start is called before the first frame update
    void Start()
    {

        Time.timeScale = 0f;
        backgroundAlpha = backgroundImage.color.a;
    }

    // Update is called once per frame
    void Update()
    {
        backgroundAlpha -= 0.4f * Time.deltaTime;
        backgroundImage.color = new Color(backgroundImage.color.r, backgroundImage.color.g, backgroundImage.color.b, backgroundAlpha);

        if (backgroundAlpha <= 1f)
        {
            Time.timeScale = 1f;
            Destroy(backgroundPanel, 10);
        }
    }
}