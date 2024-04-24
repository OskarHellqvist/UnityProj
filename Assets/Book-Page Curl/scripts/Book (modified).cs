using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class Book : MonoBehaviour {
    public Canvas canvas;
    [SerializeField]
    public Sprite NoteSprite;
    public TMP_Text[] bookPagesTexts;
    public int currentPage = 0;

    public bool isActive = true;
    public int TotalPageCount
    {
        get { return bookPagesTexts.Length; }
    }

    [Header("Pages")]
    public TMP_Text Left;
    public Image leftImage;
    public TMP_Text Right;
    public Image rightImage;

    //[Header("Event on flip")]
    //public UnityEvent OnFlip;

    void Start()
    {
        if (!canvas) canvas=GetComponentInParent<Canvas>();
        if (!canvas) Debug.LogError("Book should be a child to canvas");

        Left.gameObject.SetActive(false);
        Right.gameObject.SetActive(false);
    }
    
    void Update()
    {
        if (isActive)
        {
            if (Input.GetKeyDown(KeyCode.A) && currentPage > 0)
            { 
                FlipBackward();
            }
            else if(Input.GetKeyDown(KeyCode.D) && currentPage < bookPagesTexts.Length-1)
            {
                FlipForward();
            }
            UpdateBook();
        }
    }

    void FlipForward()
    {
        currentPage++;
    }
    void FlipBackward()
    {
        currentPage--;
    }

    void UpdateBook()
    {
        if (Left)
        {
            Left.text = bookPagesTexts[currentPage].text;
            leftImage.sprite = NoteSprite;
        }
        else
        {
            leftImage.sprite = null;
        }

        if (Right)
        { 
            Right.text = bookPagesTexts[currentPage].text;
            rightImage.sprite = NoteSprite;
        }
        else
        {
            rightImage.sprite = null;
        }

    }
}
