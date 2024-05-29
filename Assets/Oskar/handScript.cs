using SojaExiles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handScript : MonoBehaviour
{
    public GameObject hand;
    public GameObject key;
    public GameObject doll;
    private bool activedoll = false;
    public Transform keyFinalPos;
    public float moveDistance = -0.1f;
    public float moveSpeed = 1.0f;
    private Vector3 originalPosition;
    private Vector3 targetPosition;
    private bool shouldMove = false;
    public AudioSource girlLaugh;

    private VisibilityCheck visibilityCheck;

    [SerializeField] private PlayerMovement pMoveScript;

    private void Start()
    {
        originalPosition = hand.transform.position; 
        targetPosition = originalPosition; 

        visibilityCheck = doll.AddComponent<VisibilityCheck>();
        visibilityCheck = new VisibilityCheck(doll);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !activedoll) // Ensuring this block only runs once
        {
            activedoll = true; // Set to true so this block won't run again
            targetPosition = originalPosition + new Vector3(moveDistance, 0, 0);
            shouldMove = true;
            key.transform.position = keyFinalPos.position;
            girlLaugh.Play();
            doll.SetActive(true);
        }
    }

    private void Update()
    {
        if (shouldMove)
        {
            // hand movement
            hand.transform.position = Vector3.MoveTowards(hand.transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // target position is reached
            if (hand.transform.position == targetPosition)
            {
                shouldMove = false; // Optionally, set it to move back to the original position or perform other actions

                StartCoroutine(DollEvent());
                
                hand.SetActive(false);
            }
        }       
    }

    private IEnumerator DollEvent()
    {
        while (!visibilityCheck.IsTargetObjectVisible())
        {
            yield return null;
        }
        
        Vector3 startPos = doll.transform.position;

        yield return new WaitForSeconds(0.2f);

        StartCoroutine(pMoveScript.FlickerFlashlight(3));

        yield return new WaitForSeconds(0.5f);

        while (Vector3.Distance(doll.transform.position, startPos) < 10)
        {
            doll.transform.position -= new Vector3(0,Time.deltaTime*20,0);
            yield return null;
        }

        Invoke("RemoveDoll", 2);
    }

    private void RemoveDoll()
    {
        doll.SetActive(false);
        Destroy(gameObject);
    }


}

