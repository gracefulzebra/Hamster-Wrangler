using UnityEngine;

public class TutLMPlacement : BaseSnapToGrid
{

    void Start()
    {    
        itemID = "LawnMower";   
        hasItem = true;
        gridRefObject = GameObject.Find("OliverGriddy");
        gridRef = gridRefObject.GetComponent<GridGenerator>();
    }

    private void Update()
    {
        //place this in if 
        if (hasItem)
        {
            CheckForHouse();
            PlacementConfirmtation();
            nodeCheck();
        }
    } 

    protected void CheckForHouse()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 100))
        {
            if (hit.transform.gameObject.tag == "Spawner")
            {
                tutCorrectRotation = true;
            }
            else
                tutCorrectRotation = false;
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (col.transform.gameObject.tag == "TutLM")
        {
            tutCanPlace = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.transform.gameObject.tag == "TutLM")
        {
            tutCanPlace = false;
        }
    }

    void OnMouseDown()
    {
        if (hasItem == false)
        {
            if (TutManager.tutInstance.tutCanUseLM)
            {
               GetComponentInChildren<TrapBase>().ActivateTrap();
               TutManager.tutInstance.NextStep();
               TutManager.tutInstance.tutCanUseLM = false;
            }
        }
        else
        {
            TrapPlacement();
        }
    }
}

