using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Networking.Types;

public class GroundCheck : MonoBehaviour
{

    Node nodeHit;
    GameObject gridRefObject;
    GridGenerator gridRef;

    [SerializeField] GameObject colour;

    private void Awake()
    {
        gridRefObject = GameObject.Find("OliverGriddy");
        gridRef = gridRefObject.GetComponent<GridGenerator>();
    }

    public void Update()
    {
        if (!GetComponentInParent<SnapToGrid>().hasItem)
            return;

        nodeHit = gridRef.GetNodeFromWorldPoint(transform.position);

        if (nodeHit.placeable)
        {
            colour.GetComponent<Renderer>().material.color = Color.yellow;
            GetComponentInParent<SnapToGrid>().canBePlaced = true;
        }
        else
        {
            colour.GetComponent<Renderer>().material.color = Color.red;
            GetComponentInParent<SnapToGrid>().canBePlaced = false;
        }
    }
}
