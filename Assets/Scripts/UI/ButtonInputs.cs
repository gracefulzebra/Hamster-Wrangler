using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonInputs : MonoBehaviour
{

    public GameObject lawnMower;
    public GameObject fan;
    public GameObject rake;
    public SnapToGrid objectToSnap;

    void SpawnItem(GameObject itemToSpawn)
    {
        objectToSnap.holdingItem = false;
        if (!objectToSnap.holdingItem)
        {
            Vector3 spawnPos = new Vector3(0f, 100f, 0f);
            Instantiate(itemToSpawn, spawnPos, Quaternion.identity);
            objectToSnap.hasItem = true;
            objectToSnap.holdingItem = true;
        }
    }

    public void SpawnLawnMower()
    {     
            SpawnItem(lawnMower);
    }

    public void SpawnFan()
    {
            SpawnItem(fan);
    }

    public void SpawnRake()
    {
            SpawnItem(rake);
    }
}
 