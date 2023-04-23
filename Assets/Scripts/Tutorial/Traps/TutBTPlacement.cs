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

            if (tutCorrectRotation && tutCanPlace)
            {
                TutManager.tutInstance.btPlaceable = true;
            }
            else
            {
                TutManager.tutInstance.btPlaceable = false;
            }
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
            else if (!hit.transform.CompareTag("Placed Item"))
            {
               tutCorrectRotation = false;
            }             
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
            if (hasItem)
            {
                if (CompareTag("Unplaced Item"))
                {
                    TrapPlacement();
                }
            }
            else
            {
                if (CompareTag("Placed Item") && (TutManager.tutInstance.posCounter == 22 || TutManager.tutInstance.tutEnd))
                {
                    GetComponentInChildren<TrapBase>().ActivateTrap();
                }

                if (TutManager.tutInstance.tutCanUseBT)
                {
                    GetComponentInChildren<TrapBase>().ActivateTrap();
                    TutManager.tutInstance.NextStep();
                    TutManager.tutInstance.tutCanUseBT = false;
                    if (!singleUseCourtine)
                    {
                        StartCoroutine(SynergyDialouge());
                    }
                }
            }
        if (GameManager.instance.currencyManager.deleteItemMode)
        {
            // when item sold next step occurs
            SellItem();
            TutManager.tutInstance.DialougeWithNoPC();
        }
        }

    

    IEnumerator SynergyDialouge()
    {
        yield return new WaitForSeconds(1f);
        singleUseCourtine = true;
        FindObjectOfType<DialogueManager>().DisplayNextSentence();
    }

}
