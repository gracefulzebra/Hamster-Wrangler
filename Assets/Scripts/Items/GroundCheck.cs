using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Types;

public class GroundCheck : MonoBehaviour
{

   public bool isGrounded;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance;
    [SerializeField] LayerMask groundMask;
    [SerializeField] GameObject parentObject;
    Vector3 rotVector = new Vector3(0f, 90f, 0f);
    Node nodehit;
    GameObject gridRefObject;
    GridGenerator gridRef;
    // if gameobejct == holdignitem then button goes item
    private void Awake()
    {
        gridRefObject = GameObject.Find("OliverGriddy");
        gridRef = gridRefObject.GetComponent<GridGenerator>();
    }

    public void Update()
    {
       if (!GetComponentInParent<SnapToGrid>().hasItem)
            return;

        nodehit = gridRef.GetNodeFromWorldPoint(groundCheck.position);

        if (GetComponentInParent<SnapToGrid>().hasItem == false)
        {
            nodehit.placeable = false;
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (!isGrounded) //||  !nodehit.placeable)//&& parentObject.GetComponent<SnapToGrid>().confirmPlacement)
        {
            parentObject.gameObject.transform.Rotate(rotVector, Space.Self);
        }
    }
}
