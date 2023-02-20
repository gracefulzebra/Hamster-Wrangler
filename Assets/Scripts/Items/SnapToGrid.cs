using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class SnapToGrid : MonoBehaviour, IPointerUpHandler, IPointerEnterHandler
{

    GameObject gridRefObject;
    GridGenerator gridRef;
    public bool hasItem;
    public bool confirmPlacement;
    Vector3 rotVector = new Vector3(0f, 90f, 0f);
    GameObject gameManagerObject;
    GameManager gameManager;
    [SerializeField] GameObject confirmButton;
    [SerializeField] GameObject cancelButton;
    [SerializeField] GameObject rotateButton;
    [SerializeField] ParticleSystem placementEffect;
    public string itemID;
    Node nodeHit;

    void Awake()
    {
        if (gameObject.name == "Lawnmower(Clone)")
        {
            itemID = "LawnMower";
        }
        if (gameObject.name == "Leafblower(Clone)")
        {
            itemID = "LeafBlower";
        }
        if (gameObject.name == "Lighter(Clone)")
        {
            itemID = "Lighter";
        }
        if (gameObject.name == "Rake(Clone)")
        {
            itemID = "Rake";
        }
        if (gameObject.name == "Tar(Clone)")
        {
            itemID = "Tar";
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
        PlacementConfirmtation();
        if (hasItem)
        {
            nodeCheck();
        }
/*
        if (fullyPlaced == false)
        {
            if (Input.GetMouseButtonUp(0))
            {
                GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag("Unplaced Item");

                foreach (GameObject go in gameObjectArray)
                {
                    Destroy(go);
                }
            }
        }
*/
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
             if (hit.transform.gameObject.tag == "Ground")
             {
                nodeHit = gridRef.GetNodeFromWorldPoint(hit.point);
                if (nodeHit.placeable)
                {
                  gameObject.transform.position = new Vector3(nodeHit.worldPosition.x, nodeHit.worldPosition.y -0.5f, nodeHit.worldPosition.z);
                }       
             }
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
        if(hasItem == false)
        {
            GetComponentInChildren<TrapBase>().ActivateTrap();
        }
    }

    void TrapPlacement()
    {
        nodeHit.placeable = false;
        hasItem = false;
        // need this sa if you let go the if statemnt in button inputs will destroy it 
        gameObject.tag = "Placed Item";
        placementEffect.Play();
        GameManager.instance.holdingItem = false;
    }

    /// <summary>
    /// Controls placement and rotation of objects 
    /// </summary>
    void PlacementConfirmtation()
    {
        if (Input.GetKeyDown(KeyCode.R) && hasItem)
        {
            gameObject.transform.Rotate(rotVector, Space.Self);
        }  
    }

    public void RotateItem()
    {
        gameObject.transform.Rotate(rotVector, Space.Self);
    }

   public void ConfirmPlacement()
    { 
        if (gameObject.tag == "Unplaced Item")
        {
            gameManager.currencyManager.TryBuy(itemID);
            nodeHit.placeable = false;
            hasItem = false;
            gameObject.tag = "Placed Item";
            gameManager.CheckIfItemHeld();           
        }
   }

    public void CancelPlacement()
    {
        if (hasItem)
        {
        gameObject.tag = "Placed Item";
        gameManager.CheckIfItemHeld();
        Destroy(gameObject);
        }
    }
}

/*  IEnumerator PlacementConfirmation()
    {
        yield return new WaitForSeconds(2.0f);
        gameManager.currencyManager.TryBuy(itemID);
        gameManager.CheckIfItemHeld();
    }
    void OnMouseDown()
    {
            if (!fullyPlaced)
            {
                StartCoroutine(StillHoldingItem());
                //  gameObject.tag = "Unplaced Item";
                StopCoroutine(lastroutine);
                nodeHit.placeable = true;
                hasItem = true;
            }
    }

    IEnumerator StillHoldingItem()
    {
        yield return new WaitForSeconds(0.3f);
        if (Input.GetMouseButton(0))
        {
            print("true");
        }
        else
        {
            print("false");
        }
    }
    */
/* from old ui input
public void CancelRepair()
{
    if (!hasItem)
    {
        confirmButton.SetActive(false);
        cancelButton.SetActive(false);
    }       
}

private void OnMouseDown()
{
    if (!hasItem) //&& hasDurability)
    {
        confirmButton.SetActive(true);
        cancelButton.SetActive(true);
    }
}
*/