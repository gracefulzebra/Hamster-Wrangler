using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{

   public bool isGrounded;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance;
    [SerializeField] LayerMask groundMask;
    [SerializeField] GameObject parentObject;
    Vector3 rotVector = new Vector3(0f, 90f, 0f);

    public void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (!isGrounded && parentObject.GetComponent<SnapToGrid>().confirmPlacement)
        {
            parentObject.gameObject.transform.Rotate(rotVector, Space.Self);
        }
    }
}
