using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    private GameObject[] hamsterSpawners;
    [SerializeField] private GameObject hamsterPrefab;

    [SerializeField] private int hamstersPerWave;
    [SerializeField] private float spawnDelay;
    private int hamstersKilled;
    private bool waveCompleted = false;


    private void Awake()
    {
        InitializeSpawns();
    }

    private void InitializeSpawns()
    {
        hamsterSpawners = GameObject.FindGameObjectsWithTag("Spawner");
    }

    public IEnumerator StartWave()
    {
        if(!waveCompleted) 
        {
            for (int i = 1; i <= hamstersPerWave; i++)
            {
                for (int j = 0; j < hamsterSpawners.Length; j++)
                {
                    SpawnHamster(hamsterSpawners[j]);
                }
                yield return new WaitForSeconds(spawnDelay);
            } 
        }
    }

    private void SpawnHamster(GameObject hamsterSpawn)
    {
        Instantiate(hamsterPrefab, hamsterSpawn.transform.position, hamsterSpawn.transform.rotation);
    }

    public void HamstersRemaining() //Called in kill function of hamster.
    {
        hamstersKilled++;
        if( hamstersKilled > hamstersPerWave)
        {
            waveCompleted = true;
            hamstersKilled = 0;
        }
    }
}
