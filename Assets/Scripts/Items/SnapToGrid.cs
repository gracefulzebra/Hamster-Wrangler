using System.Collections;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class SnapToGrid : MonoBehaviour, IPointerUpHandler, IPointerEnterHandler
{

    GameObject gridRefObject;
    GridGenerator gridRef;
    public bool hasItem;
    Vector3 rotVector = new Vector3(0f, 90f, 0f);
    [SerializeField] ParticleSystem placementEffect;
    public string itemID;
    Node nodeHit;

    [SerializeField] LayerMask layerMask;

    public bool canBePlaced;

    [SerializeField] GameObject colour;

    void Awake()
    {

        switch (gameObject.name)
        {
            case "Lawnmower(Clone)":
                itemID = "LawnMower";
                break;
           case "Golf Leafblower Variant(Clone)":
                itemID = "LeafBlower";
                break;
            case "Leafblower(Clone)":
                itemID = "LeafBlower";
                break;
            case "Lighter(Clone)":
                itemID = "Lighter";
                break;
            case "Rake(Clone)":
                itemID = "Rake";
                break;
            case "BugZapper(Clone)":
                itemID = "BugZapper";
                break;
          
        }
        hasItem = true;
        // finds the game object with gridgenerator script
        // then assigns the compentant, cant just drag
        // inspector cause its prefab
        gridRefObject = GameObject.Find("OliverGriddy");
        gridRef = gridRefObject.GetComponent<GridGenerator>();

        gameObject.transform.Rotate(GameManager.instance.globalTrapRotation, Space.Self);
    }

    private void Update()
    {
        if (GameManager.instance.uiManager.pauseMenu.activeSelf)
            return;
        if (hasItem)
        {
            NodeCheck();
            PlacementConfirmtation();
        }
        //print(nodeHit.placeable);
    }

    /// <summary>
    /// Shoots a ray from mouse position then finds cloest walkable node
    /// </summary>
    void NodeCheck()
    {
        RaycastHit hit;
        Ray mousePos = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(mousePos, out hit, Mathf.Infinity, layerMask))
        {        
                // for regular placement 
                if (!hit.transform.CompareTag("Unplaced Item"))//hit.transform.gameObject.tag == "Ground")
               {
                    nodeHit = gridRef.GetNodeFromWorldPoint(hit.point);

                    gameObject.transform.position = new Vector3(nodeHit.worldPosition.x, nodeHit.worldPosition.y - 0.5f, nodeHit.worldPosition.z);

                    // for colour changing
                    if (nodeHit.placeable)
                    {
                        Color customColor = new Color(0.7f, 0.7f, 0.7f, 1f);
                        canBePlaced = true;
                        colour.GetComponent<Renderer>().material.color = customColor;
                    }
                    else
                    {
                        canBePlaced = false;
                        colour.GetComponent<Renderer>().material.color = Color.red;
                    }
                }           
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
            if (CompareTag("Placed Item"))
            {
                GetComponentInChildren<TrapBase>().ActivateTrap();
            }
        }    
        if (GameManager.instance.currencyManager.deleteItemMode)
        {
            SellItem();
        }
    }

    void OnMouseOver()
    {
        if (GameManager.instance.uiManager.defaultCursor == null)
            return;
        if (!GameManager.instance.currencyManager.deleteItemMode)
        {
            GameManager.instance.uiManager.overTrap = true;
        }
    }
    void OnMouseExit()
    {
        if (GameManager.instance.uiManager.defaultCursor == null)
            return;
        GameManager.instance.uiManager.overTrap = false;
        GameManager.instance.uiManager.ChangeCursorDefault();
    }
    void SellItem()
    {
        RaycastHit hit;
        Ray mousePos = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(mousePos, out hit, Mathf.Infinity, layerMask))
        {
            nodeHit = gridRef.GetNodeFromWorldPoint(hit.transform.position);
            nodeHit.placeable = true;
            GameManager.instance.currencyManager.SellItem(itemID);

            Destroy(hit.transform.gameObject);
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

        void TrapPlacement()
        {
        if (!canBePlaced)
            return;

            colour.GetComponent<Renderer>().material.color = Color.white;
            GameManager.instance.holdingItem = false;
            GameManager.instance.uiManager.RemoveShopOutline();
            GameManager.instance.audioManager.ItemPlacedAudio();
            GameManager.instance.currencyManager.TryBuy(itemID);

            nodeHit.placeable = false;
            print("Placed : " + nodeHit.gridX + "," + nodeHit.gridY);
            hasItem = false;
            // need this sa if you let go the if statemnt in button inputs will destroy it 
            gameObject.tag = "Placed Item";
            placementEffect.Play();
        }

        /// <summary>
        /// Controls placement and rotation of objects 
        /// </summary>
        void PlacementConfirmtation()
        {
            if (Input.GetKeyDown(KeyCode.R) | Input.GetMouseButtonDown(1) && hasItem)
            {
                gameObject.transform.Rotate(rotVector, Space.Self);
                GameManager.instance.globalTrapRotation = gameObject.transform.eulerAngles;
            }
        } 
    
    public void RemoveNodePlacement()
    {
        nodeHit.placeable = false;
    }
}
