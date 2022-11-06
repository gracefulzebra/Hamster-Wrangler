using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonInputs : MonoBehaviour
{

    public GameObject shopItem;
    public SnapToGrid objectToSnap;

    public void SpawnItem()
    {
        Vector3 spawnPos = new Vector3(0f, 100f, 0f);
        Instantiate(shopItem, spawnPos, Quaternion.identity);
        objectToSnap.gameObject = this.gameObject;
       // objectToSnap.hasItem = true;
    }
}
 