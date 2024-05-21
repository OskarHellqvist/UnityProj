using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

//PieceScript written by Vilmer Juvin
//This script handles the actions of the chess pieces on the chessboard
public class PieceScript : MonoBehaviour
{
    private ChessManager chessManager;

    public int position;
    public float y;
    readonly float hoverHeight = 0.03f;

    void Start()
    {
        chessManager = gameObject.GetComponentInParent<ChessManager>();
        chessManager.destroyInteraction += DestroyInteraction;
        y = transform.localPosition.y;
    }

    public void Select() //This method makes the piece hover and tells the ChessManager to select this piece
    {
        chessManager.SelectPiece(this);
        StartCoroutine(HoverUpDown(hoverHeight));
    }

    public void UnSelect() //This method moves the piece back down and removes itself from the unselect event in the ChessManager
    {
        chessManager.unSelect -= UnSelect;
        StartCoroutine(HoverUpDown(y));
    }

    private IEnumerator HoverUpDown(float targetY) //This method hovers the piece up or down in local space
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
    public IEnumerator MoveTo(Vector2 pos) //This method moves the piece to the desired location in local space
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

    private void DestroyInteraction() //Destroys the interaction script, activate this after the puzzle is finished
    {
        Destroy(GetComponent<PieceInteraction>());
    }
}
