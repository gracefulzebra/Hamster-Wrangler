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

    private void Update()
    {
        if (GameManager.instance.waveManager.waveCompleted)
        {
            ResetFlagSpear();
        }
    }

    public void ResetFlagSpear()
    {
       GameObject flagSpear = GameObject.FindGameObjectWithTag("Environmental Trap");
       Destroy(flagSpear);

       activate = true;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (!activate)
            return;
        GameManager.instance.audioManager.FlagSpearSpawnAudio();
        Instantiate(flagSpear, spawnPoint, Quaternion.identity);
        activate = false;
    }
}

