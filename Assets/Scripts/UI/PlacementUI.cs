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
       transform.LookAt(mainCamera.transform.position);
    }
}
