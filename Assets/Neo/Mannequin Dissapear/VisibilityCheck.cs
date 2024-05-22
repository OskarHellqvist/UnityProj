using UnityEngine;

namespace SojaExiles
{
    public class VisibilityCheck
    {
        public GameObject target; // The target object we want to check visibility for
        public Camera playerCamera; // The camera used by the player

        public VisibilityCheck(GameObject target, Camera playerCamera)
        {
            this.target = target;
            this.playerCamera = playerCamera;
        }

        public bool IsTargetObjectVisible()
        {
            BoxCollider collider = target.GetComponent<BoxCollider>();
            if (collider == null)
            {
                Debug.LogError("Object does not have a BoxCollider");
                return false;
            }

            // Defines 10 points on the box collider that I want to be visible
            Bounds bounds = collider.bounds;
            Vector3[] pointsToCheck = {
            bounds.center,
            bounds.min,
            bounds.max,
            new Vector3(bounds.min.x, bounds.min.y, bounds.max.z),
            new Vector3(bounds.min.x, bounds.max.y, bounds.min.z),
            new Vector3(bounds.max.x, bounds.min.y, bounds.min.z),
            new Vector3(bounds.max.x, bounds.max.y, bounds.min.z),
            new Vector3(bounds.max.x, bounds.min.y, bounds.max.z),
            new Vector3(bounds.min.x, bounds.max.y, bounds.max.z)
            };

            int visiblePoints = 0;
            foreach (var point in pointsToCheck)
            {
                Vector3 viewportPoint = playerCamera.WorldToViewportPoint(point);
                if (viewportPoint.x >= 0 && viewportPoint.x <= 1 && viewportPoint.y >= 0 && viewportPoint.y <= 1 && viewportPoint.z > 0)
                {
                    // Raycast to check for obstructions
                    Ray ray = playerCamera.ScreenPointToRay(playerCamera.WorldToScreenPoint(point));
                    if (!Physics.Raycast(ray, out RaycastHit hit, Vector3.Distance(playerCamera.transform.position, point)) || hit.transform == target)
                    {
                        visiblePoints++;
                    }
                }
            }

            return visiblePoints >= 1; 
        }

        // Comments

        // RenderMesh visibility
        //public bool IsTargetObjectVisibleREND()
        //{
        //    if (!playerCamera || !target)
        //    {
        //        Debug.LogError($"VisibilityCheck Error: Camera or Target is null on '{target.name}'.");
        //        return false;
        //    }

        //    Renderer renderer = target.GetComponent<Renderer>();
        //    if (renderer == null)
        //    {
        //        Debug.LogError($"VisibilityCheck Error: Renderer missing on '{target.name}'.");
        //        return false; // No renderer attached to the object
        //    }

        //    Bounds bounds = renderer.bounds;
        //    bool isVisible = false;

        //    for (int i = 0; i < 8; i++)
        //    {
        //        Vector3 corner = bounds.center;
        //        corner.x += (i & 1) == 0 ? bounds.extents.x : -bounds.extents.x;
        //        corner.y += (i & 2) == 0 ? bounds.extents.y : -bounds.extents.y;
        //        corner.z += (i & 4) == 0 ? bounds.extents.z : -bounds.extents.z;

        //        Vector3 viewportPoint = playerCamera.WorldToViewportPoint(corner);

        //        if (viewportPoint.z > 0 && viewportPoint.x >= 0 && viewportPoint.x <= 1 && viewportPoint.y >= 0 && viewportPoint.y <= 1)
        //        {
        //            RaycastHit hit;
        //            Vector3 direction = corner - playerCamera.transform.position;
        //            if (Physics.Raycast(playerCamera.transform.position, direction, out hit))
        //            {
        //                if (hit.transform == target.transform)
        //                {
        //                    isVisible = true;
        //                    break; // Stop checking if one corner is visible
        //                }
        //            }
        //        }
        //    }

        //    //Debug.Log($"Is Visible: {isVisible}");
        //    return isVisible;
        //}

        // If the objects transform position can be seen (Useful for checking point)
        //bool CanSeeObject(Transform target)
        //{
        //    // Convert the target's position to "viewport"
        //    Vector3 viewportPoint = playerCamera.WorldToViewportPoint(target.position);

        //    // Check if the point is inside the camera's view
        //    if (viewportPoint.z > 0 && viewportPoint.x > 0 && viewportPoint.x < 1 && viewportPoint.y > 0 && viewportPoint.y < 1)
        //    {
        //        // Check for obstacles between the camera and the target using a raycast
        //        RaycastHit hit;
        //        Vector3 direction = target.position - playerCamera.transform.position;
        //        if (Physics.Raycast(playerCamera.transform.position, direction, out hit))
        //        {
        //            // If the first object hit by the raycast is our target it's visible
        //            if (hit.transform == target)
        //            {
        //                return true;
        //            }
        //        }
        //    }
        //    return false;
        //}
    }
}
