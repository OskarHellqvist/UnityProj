using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SojaExiles
{
    public class ObjectDissapear : MonoBehaviour
    {
        public GameObject target;
        private Camera playerCamera;

        // If the object is ready to dissapear
        [SerializeField] bool ReadyToPoof;

        private VisibilityCheck visibilityCheck;

        // Start is called before the first frame update
        void Start()
        {
            visibilityCheck = new VisibilityCheck(target);
            playerCamera = Camera.main;
        }

        // Update is called once per frame
        void Update()
        {
            if (ReadyToPoof)
            {
                if (Vector3.Distance(playerCamera.transform.position, target.transform.position) > 5
                    && !visibilityCheck.IsTargetObjectVisible())
                {
                    // When the target is out of player view and ready to dissapear
                    target.gameObject.SetActive(false);
                }
            }
        }

        public void SetReadyToPoof(bool isReady)
        {
            ReadyToPoof = isReady;
        }
    }
}
