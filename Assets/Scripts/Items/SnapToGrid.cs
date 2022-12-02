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
        if (Physics.Raycast(mousePos, out hit))
        {
            Node nodehit = gridRef.GetNodeFromWorldPoint(hit.point);
            if (nodehit.walkable)
            {
                gameObject.transform.position = new Vector3(nodehit.worldPosition.x, nodehit.worldPosition.y -0.5f, nodehit.worldPosition.z);
            }
        }
    }

    private void OnMouseDown()
    {
        if (hasItem)
        {
            confirmPlacement = true;
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
        if (Input.GetMouseButtonDown(1) && hasItem)
        {
            Destroy(gameObject);
            //resets colour
            gameObject.tag = "Placed Item";
            gameManager.CheckIfItemHeld();
        }
        if (Input.GetMouseButtonDown(0) && confirmPlacement)
        {
            hasItem = false;
            gameObject.tag = "Placed Item";
            gameManager.CheckIfItemHeld();
            DestroyImmediate(GetComponent<BoxCollider>());
        }
    }
}