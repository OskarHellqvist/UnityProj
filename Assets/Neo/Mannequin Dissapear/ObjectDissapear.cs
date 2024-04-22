using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SojaExiles
{
    public class ObjectDissapear : MonoBehaviour
    {
        public Transform target;
        public Camera Camera;

        // If the object is ready to dissapear
        public bool ReadyToPoof;

        private VisibilityCheck visibilityCheck;

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            if (visibilityCheck != null && ReadyToPoof)
            {
                
            }
        }
    }
}
