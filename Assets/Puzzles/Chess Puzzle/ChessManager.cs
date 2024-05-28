using SojaExiles;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using Random = UnityEngine.Random;

//ChessManager written by Vilmer Juvin
//This script creates and manages the chess puzzle
public class ChessManager : MonoBehaviour
{
    //Translates an integer, representing a square on the board, to a position in the local space of the board that holds this Chessmanager
    public Dictionary<int, Vector2> numToPos = new Dictionary<int, Vector2>();
    readonly float y = 0.016f;
    Dictionary<int, float> rankToX = new Dictionary<int, float>();
    Dictionary<int, float> fileToZ = new Dictionary<int, float>();

    //Handles the data for the chess position and solution
    List<ChessSolution> solutionList = new();
    ChessSolution cs;

    //Handles the piece selection
    public Action unSelect;
    private PieceScript selectedPiece;

    //Handles the button selection
    public Action numButtonUnSelect;
    public Action letterButtonUnSelect;
    private string numButton;
    private string letterButton;

    //Destroys Interaction script on all chess pieces, ending the puzzle
    public Action destroyInteraction;

    #region Materials and Pieces (check inspector)

    public Material white;
    public Material black;

    public GameObject king;
    public GameObject queen;
    public GameObject rook;
    public GameObject bishop;
    public GameObject knight;
    public GameObject pawn;

    #endregion

    //Saves which king is supposed to be checkmated
    private GameObject whiteKing;
    private GameObject blackKing;

    void Start()
    {
        DictionaryStuff(); //Setting up the dictionaries

        int num = 1;
        for (int rank = 1; rank <= 8; rank++)
        {
            for (int file = 1; file <= 8; file++)
            {
                numToPos.Add(num, new Vector2(rankToX[rank], fileToZ[file]));
                num++;
            }
        }

        //Selects a random position and solution
        //System.Random rng = new();
        //solutionList[rng.Next(0, solutionList.Count)];

        //Selects the first solution in the list
        cs = solutionList[0];

        //Placing all the pieces on the board
        string[] split = cs.fen.Split('/');
        for (int i = 0; i < split.Length; i++)
        {
            int file = 1;
            foreach (char ch in split[i])
            {
                if (int.TryParse(ch.ToString(), out int result))
                {
                    file += result;
                }
                else
                {
                    GeneratePiece(ch.ToString(), (i * 8) + file);
                    file++;
                }
            }
        }
    }

    public void StartPuzzle() //This method starts the puzzle (duh)
    {
        Transform[] children = transform.parent.GetComponentsInChildren<Transform>();

        Transform[] panels = Array.FindAll(children, t => t.gameObject.name.Split(":")[0].ToLower() == "panel");

        StartCoroutine(MovePanel(panels[0], new Vector3(0, 0, 0.29f)));
        StartCoroutine(MovePanel(panels[1], new Vector3(-0.29f, 0, 0)));
        StartCoroutine(MovePanel(panels[2], new Vector3(-0.29f, 0, 0.29f)));
    }

    public IEnumerator MovePanel(Transform transform, Vector3 moveTo) //This method moves the panels in local space
    {
        float time = 0f;
        float duration = 0.3f;
        Vector3 startPos = transform.localPosition;
        Vector3 targetPos = moveTo;

        while (time < duration)
        {
            transform.localPosition = Vector3.Lerp(startPos, targetPos, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = targetPos;
    }

    public void SelectPiece(PieceScript piece) //This method selects a chess piece and unselects any previously selected pieces
    {
        if (!unSelect.IsUnityNull())
        {
            unSelect.Invoke();
        }
        
        selectedPiece = piece;
        unSelect += selectedPiece.UnSelect;
    }

    private void GeneratePiece(string piece, int pos) //This method generates the correct piece and adds/changes all necessary components and values
    {
        GameObject piecePrefab = null;
        switch (piece.ToLower())
        {
            case "k": piecePrefab = king; break;
            case "q": piecePrefab = queen; break;
            case "r": piecePrefab = rook; break;
            case "b": piecePrefab = bishop; break;
            case "n": piecePrefab = knight; break;
            case "p": piecePrefab = pawn; break;
            default:
                break;
        }

        if(piecePrefab == null) { return; }

        GameObject pieceObj = Instantiate(piecePrefab, transform, false);

        pieceObj.name = piece;
        Vector3 position = numToPos[pos];
        pieceObj.transform.localPosition = new Vector3(position.x, y, position.y);
        bool isWhite = char.IsUpper(piece, 0);
        if (!isWhite)
        {
            pieceObj.transform.Rotate(0, 180, 0);
        }

        pieceObj.GetComponent<MeshRenderer>().material = isWhite ? white : black;
        pieceObj.AddComponent<PieceScript>().position = pos;
        pieceObj.AddComponent<PieceInteraction>();

        if (piece.ToLower() == "k")
        {
            if (isWhite) { whiteKing = pieceObj; } 
            else { blackKing = pieceObj;}
        }
        else
        {
            pieceObj.AddComponent<BoxCollider>();
        }
    }

    public void SelectButton(ChessButton button, bool isNumPanel) //This method selects a button and unselects any previously selected buttons
    {
        if (button.buttonName == "red")
        {
            numButtonUnSelect += button.Unpressed;
            CheckSolution();
            return;
        }

        if (isNumPanel)
        {
            if (!numButtonUnSelect.IsUnityNull())
            {
                numButtonUnSelect.Invoke();
            }
            numButton = button.buttonName;
            numButtonUnSelect += button.Unpressed;
        }
        else
        {
            if (!letterButtonUnSelect.IsUnityNull())
            {
                letterButtonUnSelect.Invoke();
            }
            letterButton = button.buttonName;
            letterButtonUnSelect += button.Unpressed;
        }
    }

    public void CheckSolution() //This method checks the currently selected piece and buttons to see if they match the selected ChessSolution
    {
        if (numButtonUnSelect.IsUnityNull() || letterButtonUnSelect.IsUnityNull()) 
        { 
            Debug.Log("Have not selected two buttons"); 
            if (!letterButtonUnSelect.IsUnityNull() ) { letterButtonUnSelect.Invoke(); }
            if (!numButtonUnSelect.IsUnityNull()) { numButtonUnSelect.Invoke(); }
            return; 
        }

        int rank = 8 - int.Parse(numButton);

        int file = 0;
        switch (letterButton)
        {
            case "a": file = 1; break;
            case "b": file = 2; break;
            case "c": file = 3; break;
            case "d": file = 4; break;
            case "e": file = 5; break;
            case "f": file = 6; break;
            case "g": file = 7; break;
            case "h": file = 8; break;
            default:
                break;
        }

        int total = (rank * 8) + file;

        if (cs.startPos == selectedPiece.position && cs.endPos == total)
        {
            //Debug.Log("Correct!");
            EventManager.manager.AddTimer(0.7f, InvokeEvent);
            selectedPiece.StartMoveTo(numToPos[total]);
            GameObject king;
            if (cs.kingColor == "black") { king = blackKing; }
            else { king = whiteKing; }
            StartCoroutine(KnockKing(king));
            if (!destroyInteraction.IsUnityNull())
            {
                destroyInteraction.Invoke();
            }
        }
        else
        {
            //Debug.Log("WRONG!");
            unSelect.Invoke();
            numButtonUnSelect.Invoke();
            letterButtonUnSelect.Invoke();
            numButton = string.Empty;
            letterButton = string.Empty;
            SanityManager.manager.Sanity -= 20f;
        }
    }

    private void InvokeEvent() { EventManager.manager.chessCompleteEvent.Invoke(); }

    private IEnumerator KnockKing(GameObject king) //Knocks over the king 0.5 seconds after it's been called
    {
        yield return new WaitForSeconds(0.5f);

        king.AddComponent<BoxCollider>();
        Rigidbody rb = king.AddComponent<Rigidbody>();
        rb.mass = 0.00004f;
        rb.AddForce(new Vector3(0.001f, 0, 0.001f));
    }

    private void DictionaryStuff()
    {
        rankToX.Add(1, -0.212f);
        rankToX.Add(2, -0.152f);
        rankToX.Add(3, -0.09f);
        rankToX.Add(4, -0.029f);
        rankToX.Add(5, 0.03f);
        rankToX.Add(6, 0.091f);
        rankToX.Add(7, 0.152f);
        rankToX.Add(8, 0.212f);

        fileToZ.Add(1, -0.212f);
        fileToZ.Add(2, -0.152f);
        fileToZ.Add(3, -0.091f);
        fileToZ.Add(4, -0.0314f);
        fileToZ.Add(5, 0.0303f);
        fileToZ.Add(6, 0.0908f);
        fileToZ.Add(7, 0.1514f);
        fileToZ.Add(8, 0.212f);

        solutionList.Add(new ChessSolution("1r2R3/8/2p2k1p/p5p1/Pp1n4/6Pq/QP3P2/4R1K1", 36, 46, "white"));
        solutionList.Add(new ChessSolution("1nb1rk2/pp1p1ppQ/2p5/2q5/3P1N2/5R2/P1P3PP/RB4K1", 38, 23, "black"));
        solutionList.Add(new ChessSolution("3Q4/3R3R/6r1/3K4/1q3k2/8/8/4r3", 61, 60, "white"));
        solutionList.Add(new ChessSolution("5r1k/ppq2ppp/2p2N2/2Q5/4Bn2/2N5/PPP2P1P/6RK", 27, 6, "black"));
    }

    //This struct contains a fen string that represents a chess position
    //an int for the startposition of the correct piece and an endposition for where it's supposed to go
    //and it contains the color of the king that is supposed to be mated
    private struct ChessSolution
    {
        public string fen;
        public int startPos;
        public int endPos;
        public string kingColor;

        public ChessSolution(string fen, int startPos, int endPos, string kingColor)
        {
            this.fen = fen;
            this.startPos = startPos;
            this.endPos = endPos;
            this.kingColor = kingColor;
        }
    }
}
