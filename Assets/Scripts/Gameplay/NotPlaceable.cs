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
        nodehit = gridRef.GetNodeFromWorldPoint(gameObject.transform.position);
        nodehit.placeable = false;
    }
}
