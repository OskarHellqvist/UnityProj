using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceScript : MonoBehaviour
{
    private ChessManager chessManager;
    private PieceScript script;

    public int position;
    public float y;
    readonly float hoverHeight = 0.03f;

    void Start()
    {
        chessManager = gameObject.GetComponentInParent<ChessManager>();
        y = transform.localPosition.y;

        if (position == 5) { EventManager.manager.AddTimer(3f, Select); EventManager.manager.AddTimer(6f, UnSelect); }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Select()
    {
        chessManager.SelectPiece(this);
        StartCoroutine(HoverUp());
    }

    public void UnSelect()
    {
        chessManager.unSelect -= UnSelect;
        StartCoroutine(HoverDown());
    }

    private IEnumerator HoverUp()
    {
        float time = 0f;
        float duration = 0.3f;
        Vector3 startPos = transform.localPosition;
        Vector3 targetPos = new Vector3(transform.localPosition.x, hoverHeight, transform.localPosition.z);

        while (time < duration)
        {
            transform.localPosition = Vector3.Lerp(startPos, targetPos, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = targetPos;
    }

    private IEnumerator HoverDown()
    {
        float time = 0f;
        float duration = 0.3f;
        Vector3 startPos = transform.localPosition;
        Vector3 targetPos = new Vector3(transform.localPosition.x, y, transform.localPosition.z);

        while (time < duration)
        {
            transform.localPosition = Vector3.Lerp(startPos, targetPos, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = targetPos;
    }
}
