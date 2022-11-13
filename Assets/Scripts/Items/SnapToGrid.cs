using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapToGrid : MonoBehaviour
{
    GameObject gridRefObject;
    GridGenerator gridRef;
    GameObject buttonInputObject;
    ButtonInputs buttonRef;
    GameObject itemObject;
    public bool hasItem;
    public bool confirmPlacement;
    Vector3 rotVector = new Vector3(0f, 90f, 0f);

     void Awake()
     {
        // finds the game object with gridgenerator script
        // then assigns the compentant, cant just drag
        // inspector cause its prefab
        gridRefObject = GameObject.Find("OliverGriddy");
        gridRef = gridRefObject.GetComponent<GridGenerator>();
        buttonInputObject = GameObject.Find("LawnMower button");
        buttonRef = buttonInputObject.GetComponent<ButtonInputs>();
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
                gameObject.transform.position = nodehit.worldPosition;
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
            confirmPlacement = true;
        }
        // they can then rotate item to correct direction
        if (Input.GetKeyDown(KeyCode.R) && hasItem)
        {
            gameObject.transform.Rotate(rotVector, Space.Self);
        }
        // and then they confrim placement
        if (Input.GetMouseButtonDown(1) && confirmPlacement)
        {
            itemObject = null;
            hasItem = false;
            confirmPlacement = false;
            buttonRef.holdingItem = false;
        }
    }
}

