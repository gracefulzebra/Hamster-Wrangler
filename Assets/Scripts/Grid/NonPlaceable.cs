using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlaceable : MonoBehaviour
{

    GameObject gridRefObject;
    GridGenerator gridRef;
    Node nodeHit;

    private void Awake()
    {
        gridRefObject = GameObject.Find("OliverGriddy");
        gridRef = gridRefObject.GetComponent<GridGenerator>();
    }
    private void Start()
    {
        nodeHit = gridRef.GetNodeFromWorldPoint(transform.position);
        nodeHit.placeable = false;
    }
}
