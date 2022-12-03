using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlacementUI : MonoBehaviour
{

    void Update()
    {
        MoveCanvas();
    }

    void MoveCanvas()
    {

        if (Input.GetKeyDown(KeyCode.R))
        {
            gameObject.transform.Rotate(0f, 0f, 90f, Space.Self);
        }

        if (Input.GetKeyDown(KeyCode.A) | Input.GetKeyDown(KeyCode.LeftArrow))
        {
            gameObject.transform.Rotate(0f, 0f, 90f, Space.Self);
        }
        if (Input.GetKeyDown(KeyCode.D) | Input.GetKeyDown(KeyCode.RightArrow))
            {
            gameObject.transform.Rotate(0f, 0f, -90f, Space.Self);
        }
    }
}
