using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChessManager : MonoBehaviour
{
    Dictionary<int, Vector2> numToPos = new Dictionary<int, Vector2>();
    readonly float y = 0.016f;

    Dictionary<int, float> rankToX = new Dictionary<int, float>();
    Dictionary<int, float> fileToZ = new Dictionary<int, float>();

    public Action unSelect;
    private PieceScript selectedPiece;

    public Material white;
    public Material black;

    public GameObject king;
    public GameObject queen;
    public GameObject rook;
    public GameObject bishop;
    public GameObject knight;
    public GameObject pawn;

    void Start()
    {
        DictionaryStuff();

        int num = 1;
        for (int rank = 1; rank <= 8; rank++)
        {
            for (int file = 1; file <= 8; file++)
            {
                numToPos.Add(num, new Vector2(rankToX[rank], fileToZ[file]));
                num++;
            }
        }

        ChessSolution cs = new ChessSolution("1r2R3/8/2p2k1p/p5p1/Pp1n4/6Pq/QP3P2/4R1K1", 29, 19);

        //string fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR";
        string fen = "r1bk3r/p2pBpNp/n4n2/1p1NP2P/6P1/3P4/P1P1K3/q5b1";

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

    void Update()
    {
        
    }

    public void SelectPiece(PieceScript piece)
    {
        if (!unSelect.IsUnityNull())
        {
            unSelect.Invoke();
        }
        
        selectedPiece = piece;
        unSelect += selectedPiece.UnSelect;
    }

    private void GeneratePiece(string piece, int pos)
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
        if (!char.IsUpper(piece, 0))
        {
            pieceObj.transform.Rotate(0, 180, 0);
        }
        pieceObj.GetComponent<MeshRenderer>().material = char.IsUpper(piece, 0) ? white : black;
        pieceObj.AddComponent<PieceScript>().position = pos;
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
    }

    private struct ChessSolution
    {
        public string fen;
        public int startPos;
        public int endPos;

        public ChessSolution(string fen, int startPos, int endPos)
        {
            this.fen = fen;
            this.startPos = startPos;
            this.endPos = endPos;
        }
    }
}
