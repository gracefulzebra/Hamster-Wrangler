using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TutBTPlacement : BaseSnapToGrid
{
    void Start()
    {
        if (gameObject.name == "Lighter(Clone)")
        {
            itemID = "Lighter";
        }
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
            print(hit.transform.gameObject.tag);
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
        if (col.transform.gameObject.tag == "TutBT")
        {
            tutCanPlace = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.transform.gameObject.tag == "TutBT")
        {
            tutCanPlace = false;
        }
    }

    void OnMouseDown()
    {
        if (hasItem == false)
        {
            if (TutManager.tutInstance.tutCanUse)
            {
                GetComponentInChildren<TrapBase>().ActivateTrap();
                TutManager.tutInstance.NextStep();
                TutManager.tutInstance.tutCanUse = false;
            }
        }
        else
        {
            TrapPlacement();
        }
    }
}
