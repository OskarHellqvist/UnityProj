using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

namespace SojaExiles

{
    public class NoteController : MonoBehaviour
    
    {
        [Header("Input")]
        [SerializeField] private KeyCode closeKey;

        [Space(10)]
        [SerializeField] private PlayerMovement player;

        [Header("UI Text")]
        [SerializeField] private GameObject noteCanvas;
        [SerializeField] private TMP_Text noteTextAreaUI;
        [SerializeField] private TMP_Text noteTextAreaObj;

        [Space(10)]
        [SerializeField] [TextArea] private string noteText;

        [Space(10)]
        [SerializeField] private UnityEvent openEvent;
        private bool isOpen = false;

        bool isPaused = false;

        public void ShowNote()
        {
            noteTextAreaUI.text = noteText;
            noteCanvas.SetActive(true);
            openEvent.Invoke();
            DisablePlayer(true);
            isOpen = true;
            Time.timeScale = 0f;
            isPaused = true;
        }

        void DisableNote()
        {
            noteCanvas.SetActive(false);
            DisablePlayer(false);
            isOpen = false;
            Time.timeScale = 1f;
            isPaused = true;
        }

        void DisablePlayer(bool disable)
        {
            player.enabled = !disable;
        }


        void Start()
        {
            noteTextAreaObj.text = noteText;
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