using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFlagSpear : MonoBehaviour
{

    [SerializeField] GameObject flagSpear;
    [SerializeField] Vector3 spawnPoint;

    bool activate;

    private void Start()
    {
        activate = true;
        spawnPoint = new Vector3(transform.position.x, transform.position.y + 40f, transform.position.z);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (!activate)
            return;
        if (col.CompareTag("Hamster"))
        {
            Instantiate(flagSpear, spawnPoint, Quaternion.identity);
            activate = false;
        }
    }
}

