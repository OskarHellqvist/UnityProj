using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SojaExiles
{
    public class ObjectDissapear : MonoBehaviour
    {
        public GameObject target;
        public Camera playerCamera;

        // If the object is ready to dissapear
        [SerializeField] bool ReadyToPoof;

        private VisibilityCheck visibilityCheck;

        // Start is called before the first frame update
        void Start()
        {
            visibilityCheck = new VisibilityCheck(target, playerCamera);
        }

        // Update is called once per frame
        void Update()
        {
            if (ReadyToPoof)
            {
                if (!visibilityCheck.IsTargetObjectVisible())
                {
                    // When the target is out of player view and ready to dissapear
                    target.gameObject.SetActive(false);
                }
                //else
                //{
                //    Debug.Log($"{target.name} awaitin poof...");
                //}
            }
        }

        public void SetReadyToPoof(bool isReady)
        {
            ReadyToPoof = isReady;
        }
    }
}
