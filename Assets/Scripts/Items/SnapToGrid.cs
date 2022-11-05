using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapToGrid : MonoBehaviour
{
    public LayerMask layerMask;
    public GridGenerator gridRef;
    public GameObject gameObject;
    Vector3 adjustOffset = new Vector3(0f, 0.26f, 0f);

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray mousePos = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(mousePos, out hit))
            {
                // gameObject = hit.transform.gameObject;
                print("john");
                Node nodehit = gridRef.GetNodeFromWorldPoint(hit.point);
                if (nodehit.walkable)
                {
                    gameObject.transform.position = nodehit.worldPosition + adjustOffset;
                }
            }
        }
  
    }
    
    void OnMouseDown()
    {
    
    }


    void OnMouseDrag()
    {
       // nodeCheck();
    }
    

    void nodeCheck()
    {
   
    }
}

