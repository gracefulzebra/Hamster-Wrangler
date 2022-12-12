using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotPlaceable : MonoBehaviour
{

    Node nodehit;
    GameObject gridRefObject;
    GridGenerator gridRef;



    private void Awake()
    {
        gridRefObject = GameObject.Find("OliverGriddy");
        gridRef = gridRefObject.GetComponent<GridGenerator>();
    }
    // make unpalcebale; anmd give to hamster house
    private void Start()
    {

        RaycastHit hit;
        /* Ray mousePos = Vector3.down;
        if (Physics.Raycast(mousePos, out hit))
        {
            if (hit.transform.gameObject.tag == "Ground")
            {
                // if player clicks else where menu disappears
                nodehit = gridRef.GetNodeFromWorldPoint(hit.point);
            }

            if (GetComponentInParent<SnapToGrid>().hasItem == false)
            {
                nodehit.placeable = false;
            }
        }
        */
    }
}
