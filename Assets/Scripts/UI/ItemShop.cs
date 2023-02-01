using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Types;

public class ItemShop : MonoBehaviour
{

    public GameObject lawnmower;

    private void Update()
    {
            RaycastHit hit;
            Ray mousePos = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(mousePos, out hit))
            {
                if (hit.transform.gameObject.tag == "Item Shop")
                {             
                }
            }
    }

    private void OnMouseDown()
    {
        Vector3 spawnPos = new Vector3(0f, 100f, 0f);
        Instantiate(lawnmower, spawnPos, Quaternion.identity);
    }

    private void OnMouseUp()
    {
        Destroy(gameObject);
    }
}
