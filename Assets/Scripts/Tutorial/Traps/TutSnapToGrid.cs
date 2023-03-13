using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class TutSnapToGrid : MonoBehaviour, IPointerUpHandler, IPointerEnterHandler
{

    GameObject gridRefObject;
    GridGenerator gridRef;
    public bool hasItem;
    Vector3 rotVector = new Vector3(0f, 90f, 0f);
    [SerializeField] ParticleSystem placementEffect;
    public string itemID;
    Node nodeHit;

    [SerializeField] bool tutCorrectRotation;
    [SerializeField] bool tutCanPlace;

    void Awake()
    {
        if (gameObject.name == "Lawnmower(Clone)")
        {
            itemID = "LawnMower";
        }
        hasItem = true;
        // finds the game object with gridgenerator script
        // then assigns the compentant, cant just drag
        // inspector cause its prefab
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

    /// <summary>
    /// Shoots a ray from mouse position then finds cloest walkable node
    /// </summary>
    void nodeCheck()
    {
        RaycastHit hit;
        Ray mousePos = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(mousePos, out hit))
        {
          

            if (hit.transform.gameObject.tag == "Ground" || hit.transform.gameObject.tag != "Unplaced Item")
            {     
                nodeHit = gridRef.GetNodeFromWorldPoint(hit.point);

                if (nodeHit.placeable)
                {
                        gameObject.transform.position = new Vector3(nodeHit.worldPosition.x, nodeHit.worldPosition.y - 0.5f, nodeHit.worldPosition.z);
                }
            }
        }
    }

    void CheckForHouse()
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
        if (col.transform.gameObject.tag == "Tut Placement")
        {
            tutCanPlace = true;       
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.transform.gameObject.tag == "Tut Placement")
        {
            tutCanPlace = false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData) { eventData.pointerPress = gameObject; }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (hasItem)
        {
            TrapPlacement();
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

    void TrapPlacement()
    {
        if (tutCorrectRotation && tutCanPlace)
        {

            TutManager.tutInstance.NextStep();

            GameManager.instance.holdingItem = false;
            GameManager.instance.uiManager.RemoveShopOutline();
            GameManager.instance.audioManager.ItemPlacedAudio();
            GameManager.instance.currencyManager.TryBuy(itemID);

            nodeHit.placeable = false;
            hasItem = false;
            // need this sa if you let go the if statemnt in button inputs will destroy it 
            gameObject.tag = "Placed Item";
            placementEffect.Play();
        }
    }

    /// <summary>
    /// Controls placement and rotation of objects 
    /// </summary>
    void PlacementConfirmtation()
    {
        if (Input.GetKeyDown(KeyCode.R) | Input.GetMouseButtonDown(1) && hasItem)
        {
            gameObject.transform.Rotate(rotVector, Space.Self);
        }
    }
}

