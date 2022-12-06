using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;

public class SnapToGrid : MonoBehaviour
{
    GameObject gridRefObject;
    GridGenerator gridRef;
    public bool hasItem;
    public bool confirmPlacement;
    Vector3 rotVector = new Vector3(0f, 90f, 0f);
    [SerializeField] GameObject gameManagerObject;
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject confirmButton;
    [SerializeField] GameObject cancelButton;
    public string itemID;

    void Awake()
    {
        if (gameObject.name == "LawnMower(Clone)")
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
        // holdingItem = false;
        gameManagerObject = GameObject.Find("Game Manager");
        gameManager = gameManagerObject.GetComponent<GameManager>();
    }

    private void Update()
    {
        PlacementConfirmtation();
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
                // if player clicks else where menu disappears
                Node nodehit = gridRef.GetNodeFromWorldPoint(hit.point);
                if (nodehit.walkable)
                {
                  gameObject.transform.position = new Vector3(nodehit.worldPosition.x, nodehit.worldPosition.y -0.5f, nodehit.worldPosition.z);
                }       
            }
            if (!hasItem)
            {
                confirmButton.SetActive(false);
                cancelButton.SetActive(false);
            }
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

    /// <summary>
    /// Controls placement and rotation of objects 
    /// </summary>
    void PlacementConfirmtation()
    {
        // when player has item they choose grid square they want
        if (Input.GetMouseButtonDown(0) && hasItem)
        {
            nodeCheck();
        }
        // they can then rotate item to correct direction
        if (Input.GetKeyDown(KeyCode.R) && hasItem)
        {
            gameObject.transform.Rotate(rotVector, Space.Self);
        }  
    }

   public void ConfirmPlacement()
    { 
        if (gameObject.tag == "Unplaced Item")
        {
            gameManager.currencyManager.TryBuy(itemID);
            hasItem = false;
            gameObject.tag = "Placed Item";
            gameManager.CheckIfItemHeld();
            confirmButton.SetActive(false);
            cancelButton.SetActive(false);
        }
   }

    public void CancelPlacement()
    {
        if (hasItem)
        {
        Destroy(gameObject);
        gameManager.CheckIfItemHeld();
        }
    }

    public void CancelRepair()
    {
        if (!hasItem)
        {
            confirmButton.SetActive(false);
            cancelButton.SetActive(false);
        }       
    }
}