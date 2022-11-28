using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    private GameObject[] hamsterSpawners;
    [SerializeField] private GameObject hamsterPrefab;

    private void Awake()
    {
        //InitializeSpawns();
    }

    private void InitializeSpawns()
    {
        hamsterSpawners = GameObject.FindGameObjectsWithTag("Spawner");
    }

    public void StartWave()
    {
        //Starts Current Wave
        //Probably call coroutine and have wave logic there or have this be coroutine.
    }

    private void SpawnHamster(GameObject hamsterSpawn)
    {
        Instantiate(hamsterPrefab, hamsterSpawn.transform.position, hamsterSpawn.transform.rotation);
    }
}
