using SojaExiles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeypadLock : MonoBehaviour
{
    // Element 10 & 11 in Keys are Clear - and Enter key in that order
    [SerializeField] private List<GameObject> Keys;
    [SerializeField] private Drawer_Pull_X drawer;

    [SerializeField] private TextMeshProUGUI screen;
    public string correctCode = "5555", inputCode = "";
    private bool unlocked;

    private int damageOnWrong = 10; // Sanity (0 - 100)

    private Color initialColor;        

    void Start()
    {
        initialColor = screen.color;

        int num = 0;
        foreach (var key in Keys)
        { 
            KeypadKey keypadKey = key.AddComponent<KeypadKey>();
            keypadKey.keypadLock = this;
            keypadKey.num = num; // Assign the number directly
            num = num + 1; // Increment num and wrap around after 9
        }
    }

    public void AddNumber(string number)
    {
        if (!unlocked && inputCode.Length < 4)
        {
            if (screen.text.Length > 4)
            {
                ClearNumbers();
                screen.color = initialColor;
            }

            inputCode += number;
            screen.SetText(inputCode);
            FindObjectOfType<AudioManager2>().Play("KeypadButtonPress", transform.position);
        }
    }

    public void ClearNumbers()
    {
        if (!unlocked)
        {
            inputCode = "";
            screen.SetText(inputCode);
        }
    }

    public void Enter()
    {
        if (!unlocked)
        {
            if (inputCode == correctCode)
            {
                screen.SetText("Unlocked");
                unlocked = true;
                drawer.locked = false;
                drawer.Interact();
                FindObjectOfType<AudioManager2>().Play("CorrectPassword", transform.position);
            }
            else
            {
                ClearNumbers();
                screen.color = Color.red;
                screen.SetText("Incorrect");
                FindObjectOfType<AudioManager2>().Play("WrongPassword", transform.position);

                SanityManager.manager.DamageSanity();
            }
        }
    }
}
