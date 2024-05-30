using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class Brightness : MonoBehaviour
{
    public Slider brightnessSlider;

    public PostProcessProfile brightness;
    public PostProcessLayer layer;

    AutoExposure exposure;
    // Start is called before the first frame update
    void Start()
    {
        if (brightness && brightnessSlider)
        {
            brightness.TryGetSettings(out exposure);
            AdjustBrightness(brightnessSlider.value);
        }
    }

    public void AdjustBrightness(float value)
    {
        if (!exposure) {
            Debug.LogError("Exposure could not be found");
            return; 
        }

        if(value != 0)
        {
            exposure.keyValue.value = value;
        }
        else
        {
            exposure.keyValue.value = .05f;
        }
    }
}
