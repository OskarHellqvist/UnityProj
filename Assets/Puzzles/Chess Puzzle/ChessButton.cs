using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessButton : MonoBehaviour
{
    private ChessManager chessManager;

    private float y;
    private float pressedY;

    private bool isNumPanel;
    public string buttonName;

    void Start()
    {
        chessManager = transform.parent.parent.GetComponentInChildren<ChessManager>();
        chessManager.destroyInteraction += DestroyInteraction;
        
        y = transform.localPosition.y;
        pressedY = 0.002f;

        isNumPanel = transform.parent.name == "Panel:Num";

        string name = gameObject.name;
        buttonName = name.Split(':')[1];

        //if (buttonName == "3" || buttonName == "f") { EventManager.manager.AddTimer(3f, Pressed); }

        //EventManager.manager.AddTimer(3f, Pressed);
        //EventManager.manager.AddTimer(6f, Unpressed);

        gameObject.AddComponent<ChessButtonInteraction>();
    }

    public void Pressed()
    {
        chessManager.SelectButton(this, isNumPanel);
        StartCoroutine(ButtonUpDown(pressedY));
    }

    public void Unpressed()
    {
        if (isNumPanel) { chessManager.numButtonUnSelect -= Unpressed; }
        else { chessManager.letterButtonUnSelect -= Unpressed; }
        StartCoroutine(ButtonUpDown(y));
    }

    private IEnumerator ButtonUpDown(float targetY)
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

    private void DestroyInteraction()
    {
        Destroy(GetComponent<ChessButtonInteraction>());
    }
}
