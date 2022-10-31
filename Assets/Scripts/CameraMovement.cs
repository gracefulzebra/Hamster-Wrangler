using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Camera")]
    public float mouseSens;
    public Transform cameraParent;
    float rotationX = 0f;
    float rotationY = 0f;


    [Header("Movement")]
    public float moveSpeed;
    public float verticalSpeed;
        
    [Header("Locking Movement")]
    private bool canMove = true;



    // Start is called before the first frame update
    void Start()
    {   
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        CameraRotate();

        if (canMove)
            CameraMovements();

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (canMove)
            {
                CameraMovements();
                canMove = false;
                print("b");
            }
            else
            {
                canMove = true;
                print("l");

            }
        }
    }


    void CameraMovements()
    {

        if (Input.GetKey(KeyCode.Space))
        {

            transform.position += transform.up * verticalSpeed * Time.deltaTime;

        }

        if (Input.GetKey(KeyCode.LeftShift))
        {

            transform.position -= transform.up * verticalSpeed * Time.deltaTime;

        }
        transform.position += transform.forward * moveSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
        transform.position += transform.right * moveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime;
    }

    void CameraRotate()
    {

        rotationX += Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        rotationY += Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;
        rotationY = Mathf.Clamp(rotationY, -90.0f, 90.0f);

        transform.localRotation = Quaternion.AngleAxis(rotationX, Vector3.up);
        transform.localRotation *= Quaternion.AngleAxis(rotationY, Vector3.left);
    }
}
