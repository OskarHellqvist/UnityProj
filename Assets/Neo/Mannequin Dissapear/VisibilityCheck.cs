using UnityEngine;

public class VisibilityCheck : MonoBehaviour
{
    public Transform target; // The target object we want to check visibility for
    public Camera playerCamera; // The camera used by the player

    private void Start()
    {
        if (target == null) 
            target = this.GetComponent<Transform>();
    }

    void Update()
    {
        if (IsObjectFullyVisible(target))
        {
            Debug.Log("Target is visible");
        }
        else
        {
            Debug.Log("Target is not visible");
        }
    }

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

    bool IsObjectFullyVisible(Transform target)
    {
        Renderer renderer = target.GetComponent<Renderer>();
        if (renderer == null) return false; // No renderer attached to the object

        // Get the bounding box of the object in world space
        Bounds bounds = renderer.bounds;
        bool isVisible = false;

        for (int i = 0; i < 8; i++)
        {
            Vector3 corner = bounds.center;

            // Maps out corners
            corner.x += (i & 1) == 0 ? bounds.extents.x : -bounds.extents.x;
            corner.y += (i & 2) == 0 ? bounds.extents.y : -bounds.extents.y;
            corner.z += (i & 4) == 0 ? bounds.extents.z : -bounds.extents.z;

            Vector3 viewportPoint = playerCamera.WorldToViewportPoint(corner);

            // Check if at least one corner is inside the camera's viewport
            if (viewportPoint.z > 0 && viewportPoint.x >= 0 && viewportPoint.x <= 1 && viewportPoint.y >= 0 && viewportPoint.y <= 1)
            {
                RaycastHit hit;
                Vector3 direction = corner - playerCamera.transform.position;
                // Check if the corner is not obstructed
                if (Physics.Raycast(playerCamera.transform.position, direction, out hit))
                {
                    if (hit.transform == target)
                    {
                        isVisible = true;
                    }
                    else if (hit.distance < direction.magnitude)
                    {
                        // An obstacle is blocking this corner
                        continue;
                    }
                }
            }
        }

        return isVisible;
    }
}
