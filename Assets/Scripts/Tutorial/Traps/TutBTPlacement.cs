using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TutBTPlacement : BaseSnapToGrid
{

    bool singleUseCourtine;

    void Start()
    {
        itemID = "Lighter";        
        hasItem = true;
        gridRefObject = GameObject.Find("OliverGriddy");
        gridRef = gridRefObject.GetComponent<GridGenerator>();
    }

    private void Update()
    {
        //place this in if 
        if (hasItem)
        {
            CheckForTrap();
            PlacementConfirmtation();
            nodeCheck();
        }
    }

    void CheckForTrap()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hit, 100))
        {
            if (hit.transform.CompareTag("Placed Item"))
            {
                tutCorrectRotation = true;
            }
            else
                tutCorrectRotation = false;
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (col.transform.CompareTag("TutBT"))
        {
            tutCanPlace = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.transform.CompareTag("TutBT"))
        {
            tutCanPlace = false;
        }
    }

    void OnMouseDown()
    {
        if (hasItem == false)
        {
            if (TutManager.tutInstance.tutEnd)
            {
                GetComponentInChildren<TrapBase>().ActivateTrap();
                TutManager.tutInstance.NextStep();
            }

            if (TutManager.tutInstance.tutCanUseBT)
            {
                GetComponentInChildren<TrapBase>().ActivateTrap();
                TutManager.tutInstance.NextStep();
                TutManager.tutInstance.tutCanUseBT = false;
                if (!singleUseCourtine)
                {
                    StartCoroutine(SyenrgyDialouge());
                }        
            }
        }
        else
        {
            TrapPlacement();
        }
    }

    IEnumerator SyenrgyDialouge()
    {
        yield return new WaitForSeconds(1f);
        singleUseCourtine = true;
        FindObjectOfType<DialogueManager>().DisplayNextSentence();
    }

}
