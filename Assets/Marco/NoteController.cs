using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

namespace SojaExiles

{
    public class NoteController : MonoBehaviour, Interactable
    
    {
        [Header("Input")]
        [SerializeField] private KeyCode closeKey;

        [Space(10)]
        [SerializeField] private PlayerMovement player;

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
            audioSource.Play();
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