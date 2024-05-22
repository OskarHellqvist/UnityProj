using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using Unity.VisualScripting;

namespace SojaExiles
{
    public class NoteController : MonoBehaviour, Interactable
    {
        private KeyCode closeKey = KeyCode.Mouse1;

        [Header("Logbook")]
        [SerializeField] private TMP_Text[] Logbook;

        enum NoteType
        {
            Note,
            Paper,
            PostIt,
            UtilityBill,
            SpectralConvergance
        };

        [SerializeField] private NoteType noteType;

        [Header("UI Text")]
        private GameObject canvas;
        private NoteAssigner noteAssigner;
        private GameObject notePanel;
        private GameObject noteImage;
        private TMP_Text noteTextAreaUI;
        private TMP_Text TextOnNoteObj;

        [Space(10)]
        [SerializeField][TextArea] private string noteText;

        [Space(10)]
        [SerializeField] private UnityEvent openEvent;

        [SerializeField] private AudioSource audioSource;
        private bool isOpen = false;

        void Start()
        {
            canvas = GameObject.Find("Canvas");
            noteAssigner = canvas.gameObject.GetComponent<NoteAssigner>();

            if (transform.childCount > 0)
            {
                TextOnNoteObj = transform.Find("text").GetComponent<TMP_Text>();
            }

            if (TextOnNoteObj != null)
            {
                TextOnNoteObj.text = noteText;
            }

            notePanel = NoteAssigner.notePanel;

            if (notePanel == null)
            {
                Debug.LogError("Note panel is not assigned");
                return; // Exit the method if notePanel is not assigned
            }

            switch (noteType)
            {
                case NoteType.Note:
                    noteImage = NoteAssigner.noteImage;
                    break;
                case NoteType.Paper:
                    noteImage = NoteAssigner.PaperImage;
                    break;
                case NoteType.PostIt:
                    noteImage = NoteAssigner.PostITImage;
                    break;
                case NoteType.UtilityBill:
                    noteImage = NoteAssigner.UtilityBillImage;
                    break; 
                case NoteType.SpectralConvergance:
                    noteImage = NoteAssigner.SpectralConverganceImage;
                    break;
            } 
            
            if (noteImage.transform != null && transform.childCount > 0)
            {
                noteTextAreaUI = noteImage.transform.GetChild(0).GetComponent<TMP_Text>();
            }
        }


        public void Interact()
        {
            ShowNote();
            //AddNoteToLog();
            audioSource.Play();
        }

        public void AddNoteToLog()
        {
            TMP_Text note = noteTextAreaUI;
            Logbook.AddRange((IEnumerable<TMP_Text>)note);
        }

        public void ShowNote()
        {
            Time.timeScale = 0f;

            if (noteTextAreaUI != null)
            {
                noteTextAreaUI.text = noteText;
            }

            notePanel.SetActive(true);
            noteImage.SetActive(true);
            openEvent.Invoke();
            isOpen = true;
        }

        void DisableNote()
        {
            Time.timeScale = 1f;

            notePanel.SetActive(false);
            noteImage.SetActive(false);
            isOpen = false;
            gameObject.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            if (isOpen)
            {
                if (Input.GetKeyDown(closeKey))
                {
                    DisableNote();
                }
            }
        }
    }
}