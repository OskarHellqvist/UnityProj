using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllSettings : MonoBehaviour
{
    [SerializeField] private Slider sensSlider;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("mouseSensitivity"))
        {
            LoadMouseSens();
        }
        else
        {
            SetMouseSens();
        }
    }

    public void SetMouseSens()
    {
        float mouseSensitivity = sensSlider.value;
        PlayerPrefs.SetFloat("mouseSensitivity", mouseSensitivity);
    }

    private void LoadMouseSens()
    {
        sensSlider.value = PlayerPrefs.GetFloat("mouseSensitivity");

        SetMouseSens();
    }

}
