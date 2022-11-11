using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonInputs : MonoBehaviour
{

    public GameObject lawnMower;
    public GameObject fan;
    public SnapToGrid objectToSnap;
    public bool holdingItem = false;

    public void SpawnLawnMower()
    {
        if (!holdingItem)
        {
            Vector3 spawnPos = new Vector3(0f, 100f, 0f);
            Instantiate(lawnMower, spawnPos, Quaternion.identity);
            objectToSnap.hasItem = true;
            holdingItem = true;
        }
    }

    public void SpawnFan()
    {
        if (!holdingItem)
        {
            Vector3 spawnPos = new Vector3(0f, 100f, 0f);
            Instantiate(fan, spawnPos, Quaternion.identity);
            objectToSnap.hasItem = true;
            holdingItem = true;
        }
    }
}
 