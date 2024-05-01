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
        [SerializeField] private PlayerMovement player;

        [Header("Logbook")]
        [SerializeField] private TMP_Text[] Logbook;

        [Header("UI Text")]
        [SerializeField] private GameObject noteCanvas;
        [SerializeField] private TMP_Text noteTextAreaUI;
        [SerializeField] private TMP_Text TextOnNoteObj;

        [Space(10)]
        [SerializeField] [TextArea] private string noteText;

        [Space(10)]
        [SerializeField] private UnityEvent openEvent;

        [SerializeField] private AudioSource audioSource;
        private bool isOpen = false;

        public void Interact()
        {
            ShowNote();
            AddNoteToLog();
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
            noteCanvas.SetActive(true);
            openEvent.Invoke();
            DisablePlayer(true);
            isOpen = true;
        }

        void DisableNote()
        {
            Time.timeScale = 1f;
            noteCanvas.SetActive(false);
            DisablePlayer(false);
            isOpen = false;
            gameObject.SetActive(false);
        }

        void DisablePlayer(bool disable)
        {
            player.enabled = !disable;
        }


        void Start()
        {
            TextOnNoteObj.text = noteText;
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