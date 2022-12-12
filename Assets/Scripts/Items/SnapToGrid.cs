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
    GameObject gameManagerObject;
    GameManager gameManager;
    [SerializeField] GameObject confirmButton;
    [SerializeField] GameObject cancelButton;
    [SerializeField] GameObject rotateButton;
    public string itemID;
    Node nodehit;

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
        // holdingItem = false;
        gameManagerObject = GameObject.Find("Game Manager");
        gameManager = gameManagerObject.GetComponent<GameManager>();

    }

    private void Update()
    {
        PlacementConfirmtation();
        if (hasItem)
        {
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
             if (hit.transform.gameObject.tag == "Ground")
             {
                // if player clicks else where menu disappears
                nodehit = gridRef.GetNodeFromWorldPoint(hit.point);
                if (nodehit.placeable)
                {
                  gameObject.transform.position = new Vector3(nodehit.worldPosition.x, nodehit.worldPosition.y -0.5f, nodehit.worldPosition.z);
                }       
             }
        }
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
            nodehit.placeable = false;
            hasItem = false;
            gameObject.tag = "Placed Item";
            gameManager.CheckIfItemHeld();
            
            confirmButton.SetActive(false);
            cancelButton.SetActive(false);
            rotateButton.SetActive(false);
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

}