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
    // used to lock player movement
    private bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        MovementLock();

        // needs this so player can move/look on game start
        if (canMove)
        {
         CameraMovements();
         CameraRotate();
        }   
    }

    /// <summary>
    /// Locks camera in position, player can still rotate.
    /// unlocks cursor
    /// </summary>
    void MovementLock()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (canMove)
            {
                CameraRotate();
                CameraMovements();
                canMove = false;
                Cursor.lockState = CursorLockMode.Confined;
            }
            else
            {
                canMove = true;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    /// <summary>
    /// Used to move the camera 
    /// </summary>
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

    /// <summary>
    /// Used to rotate the camera
    /// </summary>
    void CameraRotate()
    {
        rotationX += Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        rotationY += Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;
        rotationY = Mathf.Clamp(rotationY, -90.0f, 90.0f);

        transform.localRotation = Quaternion.AngleAxis(rotationX, Vector3.up);
        transform.localRotation *= Quaternion.AngleAxis(rotationY, Vector3.left);
    }           
}
