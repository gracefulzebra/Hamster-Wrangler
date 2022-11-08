using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonInputs : MonoBehaviour
{

    public GameObject lawnMower;
    public GameObject fan;
    public SnapToGrid objectToSnap;

    public void SpawnLawnMower()
    {
        Vector3 spawnPos = new Vector3(0f, 100f, 0f);
        Instantiate(lawnMower, spawnPos, Quaternion.identity);
       // objectToSnap.gameObject = lawnMower;
        objectToSnap.hasItem = true;
    }

    public void SpawnFan()
    {
        Vector3 spawnPos = new Vector3(0f, 100f, 0f);
        Instantiate(fan, spawnPos, Quaternion.identity);
      //  objectToSnap.gameObject = fan;
        objectToSnap.hasItem = true;
    }
}
 