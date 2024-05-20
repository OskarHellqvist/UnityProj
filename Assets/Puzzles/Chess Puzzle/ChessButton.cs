using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ChessButton written by Vilmer Juvin
//This script handles the actions of the buttons on the chessboard
public class ChessButton : MonoBehaviour
{
    private ChessManager chessManager;

    private float y;
    private float pressedY;

    private bool isNumPanel;
    [HideInInspector]public string buttonName;

    void Start()
    {
        chessManager = transform.parent.parent.GetComponentInChildren<ChessManager>();
        chessManager.destroyInteraction += DestroyInteraction;
        
        y = transform.localPosition.y;
        pressedY = 0.002f;

        isNumPanel = transform.parent.name == "Panel:Num";

        string name = gameObject.name;
        buttonName = name.Split(':')[1];

        gameObject.AddComponent<ChessButtonInteraction>();
    }

    public void Pressed() //This method moves the button down and tells the ChessManager to select this button
    {
        chessManager.SelectButton(this, isNumPanel);
        StartCoroutine(ButtonUpDown(pressedY));
    }

    public void Unpressed() //This method moves the button up and removes itself from one of the buttonUnSelect events in the ChessManager
    {
        if (isNumPanel) { chessManager.numButtonUnSelect -= Unpressed; }
        else { chessManager.letterButtonUnSelect -= Unpressed; }
        StartCoroutine(ButtonUpDown(y));
    }

    private IEnumerator ButtonUpDown(float targetY) //This method moves the button in local space
    {
        float time = 0f;
        float duration = 0.3f;
        Vector3 startPos = transform.localPosition;
        Vector3 targetPos = new Vector3(transform.localPosition.x, targetY, transform.localPosition.z);

        while (time < duration)
        {
            transform.localPosition = Vector3.Lerp(startPos, targetPos, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = targetPos;
    }

    private void DestroyInteraction() //Destroys the interaction script, activate this after the puzzle is finished
    {
        Destroy(GetComponent<ChessButtonInteraction>());
    }
}
