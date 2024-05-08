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
        private KeyCode closeKey = KeyCode.Mouse0;

        [Space(10)]
        //[SerializeField] private PlayerMovement player;

        [Header("Logbook")]
        [SerializeField] private TMP_Text[] Logbook;

        [Header("UI Text")]
        [SerializeField] private GameObject notePanel;
        private TMP_Text noteTextAreaUI;
        private TMP_Text TextOnNoteObj;

        [Space(10)]
        [SerializeField] [TextArea] private string noteText;

        [Space(10)]
        [SerializeField] private UnityEvent openEvent;

        [SerializeField] private AudioSource audioSource;
        private bool isOpen = false;

        void Start()
        {
            TextOnNoteObj = transform.Find("text").GetComponent<TMP_Text>();
            TextOnNoteObj.text = noteText;

            if (notePanel == null)
            {
                Debug.Log("Note panel is not assigned. Please assign the note panel GameObject in the Inspector.");
                return; // Exit the method if notePanel is not assigned
            }

            Transform noteImage = notePanel.transform.Find("NoteImage");

            if (noteImage != null)
            {
                noteTextAreaUI = noteImage.GetChild(0).GetComponent<TMP_Text>();
            }
            else
            {
                Debug.Log("NoteImage object not found under notePanel.");
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
            noteTextAreaUI.text = noteText;
            notePanel.SetActive(true);
            openEvent.Invoke();
            isOpen = true;
        }

        void DisableNote()
        {
            Time.timeScale = 1f;
            notePanel.SetActive(false);
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