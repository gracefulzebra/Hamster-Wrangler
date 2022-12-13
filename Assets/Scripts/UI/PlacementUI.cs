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

        transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.back);

    }
}
