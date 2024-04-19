using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace SojaExiles
{
    public class Raycast : MonoBehaviour
    {
        [Header("Raycast Features")]
        [SerializeField] private float rayLength =0;
        private Camera _camera;
        private Interactable _interactableObject;

        [Header("Crosshair")]
        [SerializeField] private Image crosshair;

        [Header("Input Key")]
        [SerializeField] private KeyCode interactKey;

        void Start()
        {
            _camera = GetComponent<Camera>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Physics.Raycast(_camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f)), transform.forward, out RaycastHit hit, rayLength))
            {
                _interactableObject = hit.collider.GetComponent<Interactable>();
                if (_interactableObject != null)
                {
                    HighlightCrosshair(true);
                }
                else
                {
                    ClearInteraction();
                }
            }
            else
            {
                ClearInteraction();
            }

            if (_interactableObject != null)
            {
                if (Input.GetKeyDown(interactKey))
                {
                    _interactableObject.Interact();
                }
            }
        }

        public void ClearInteraction()
        {
            HighlightCrosshair(false);
            _interactableObject = null;
        }

        public void HighlightCrosshair(bool on)
        {
            if (on)
            {
                crosshair.transform.localScale = Vector2.one * 2;
            }
            else
            {
                crosshair.transform.localScale = Vector2.one;
            }
        }
    }
}