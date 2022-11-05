using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapToGrid : MonoBehaviour
{

    public GridGenerator gridRef;

    public void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
           // Vector3 down = transform.TransformDirection(Vector3.down);
            RaycastHit hit;
            Ray mousePos = Camera.main.ScreenPointToRay(Input.mousePosition);
            print("john");
            if (Physics.Raycast(mousePos, out hit, 10000))
            {
               // gameObject = hit.transform.gameObject;
                Debug.Log(hit.point);
                Debug.Log(hit.transform.name);
            }
            Node nodehit = gridRef.GetNodeFromWorldPoint(hit.point);
            print(nodehit);
        }
    }
}
