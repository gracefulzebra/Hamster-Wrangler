using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnapToGrid : MonoBehaviour
{
    GameObject gridRefObject;
    GridGenerator gridRef;
    public bool hasItem;
    public bool confirmPlacement;
    Vector3 rotVector = new Vector3(0f, 90f, 0f);
   // public bool holdingItem;
    public GameObject buttonRef;
    public GameObject placementGuide;
    [SerializeField] GameObject gameManagerObject;
    [SerializeField] GameManager gameManager;
    [SerializeField] LayerMask layerMask;

    void Awake()
    {
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
        if (Physics.Raycast(mousePos, out hit, ~layerMask))
        {
            Node nodehit = gridRef.GetNodeFromWorldPoint(hit.point);
            if (nodehit.walkable)
            {
                gameObject.transform.position = new Vector3(nodehit.worldPosition.x, nodehit.worldPosition.y -0.5f, nodehit.worldPosition.z);
            }
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
            placementGuide.transform.Rotate(rotVector, Space.Self);
        }
        // and then they confrim placement
        if (Input.GetMouseButtonDown(0) && confirmPlacement)
        {
            // need this bool so when you place item it doesnt place all previosu items aswell
            hasItem = false;
            confirmPlacement = false;
            // no longer holding item
            gameManager.holdingItem = false;
            //resets colour
            buttonRef.gameObject.GetComponent<Image>().color = Color.white;
            // sets item as placed
            gameObject.tag = "Placed Item";
        }
    }

    private void OnMouseDown()
    {
        print("john");
        confirmPlacement = true;
    }
}


 



