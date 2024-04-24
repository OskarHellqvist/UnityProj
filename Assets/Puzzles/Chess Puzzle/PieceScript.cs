using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

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

        if (position == 36) { EventManager.manager.AddTimer(3f, Select); }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Select()
    {
        chessManager.SelectPiece(this);
        StartCoroutine(HoverUpDown(hoverHeight));
    }

    public void UnSelect()
    {
        chessManager.unSelect -= UnSelect;
        StartCoroutine(HoverUpDown(y));
    }

    private IEnumerator HoverUpDown(float targetY)
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

    public void StartMoveTo(Vector2 pos) { StartCoroutine(MoveTo(pos)); }
    public IEnumerator MoveTo(Vector2 pos)
    {
        float time = 0f;
        float duration = 0.3f;
        Vector3 startPos = transform.localPosition;
        Vector3 targetPos = new Vector3(pos.x, transform.localPosition.y, pos.y);

        while (time < duration)
        {
            transform.localPosition = Vector3.Lerp(startPos, targetPos, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = targetPos;
        StartCoroutine(HoverUpDown(y));
    }
}
