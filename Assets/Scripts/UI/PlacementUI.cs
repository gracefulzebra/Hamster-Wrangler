using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlacementUI : MonoBehaviour
{

    public GameObject mainCamera;
    public Camera uiCamera;


    private void Awake()
    {
        mainCamera = GameObject.Find("Main Camera");
        uiCamera = GameObject.Find("UI Camera").GetComponent<Camera>();

        GetComponent<Canvas>().worldCamera = uiCamera;
    }

    void Update()
    {
       //Vector3 lookAtVec = new Vector3(camera.transform.position.x, camera.transform.position.y, camera.transform.position.z);

       transform.LookAt(mainCamera.transform.position);
        //MoveCanvas();
    }

    void MoveCanvas()
    {

        if (Input.GetKeyDown(KeyCode.R))
        {
            gameObject.transform.Rotate(0f, 0f, 90f, Space.Self);
        }

        if (Input.GetKeyDown(KeyCode.A) | Input.GetKeyDown(KeyCode.LeftArrow))
        {
            gameObject.transform.Rotate(90f, 90f, 0f, Space.Self);
        }
        if (Input.GetKeyDown(KeyCode.D) | Input.GetKeyDown(KeyCode.RightArrow))
            {
            gameObject.transform.Rotate(90f, -90f, 0, Space.Self);
        }
    }
}
