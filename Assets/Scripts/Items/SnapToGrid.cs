using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapToGrid : MonoBehaviour
{
    public GameObject gridRefObject;
    public GridGenerator gridRef;
    public GameObject gameObject;
    public bool hasItem;

     void Start()
     {
        // finds the game object with gridgenerator script
        // then assigns the compentant, cant just drag
        // inspector cause its prefab
        gridRefObject = GameObject.Find("OliverGriddy");
        gridRef = gridRefObject.GetComponent<GridGenerator>();
     }

    private void Update()
    {
        // has item is sanity check so it doesnt run this everytime mouse is clicked
        if (Input.GetMouseButtonDown(0) && hasItem)
        {
            nodeCheck();
        }
    }

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
                gameObject = null;
                hasItem = false;
            }
        }
    }
}

