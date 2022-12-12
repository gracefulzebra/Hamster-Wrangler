using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    private GameManager manager;
    private GameObject[] hamsterSpawners;
    [SerializeField] private GameObject hamsterPrefab;
    [SerializeField] private int[] hamstersPerWave;
    [SerializeField] private float spawnDelay;
    private int hamstersKilled;
    private bool waveCompleted;
    private int wave;
    private int maxWaves;


    private void Awake()
    {
        manager = GetComponent<GameManager>();
        InitializeSpawns();

        waveCompleted = true;
        maxWaves = hamstersPerWave.Length;
    }

    private void InitializeSpawns()
    {
        hamsterSpawners = GameObject.FindGameObjectsWithTag("Spawner");
    }

    public IEnumerator StartWave()
    {
        if(waveCompleted && wave < hamstersPerWave.Length) 
        {
            waveCompleted = false;
            for (int i = 0; i <= hamstersPerWave[wave] - 1; i++)
            {
                if (i != 0)
                    yield return new WaitForSeconds(spawnDelay);

                for (int j = 0; j < hamsterSpawners.Length; j++)
                {
                    SpawnHamster(hamsterSpawners[j]);
                }
                
            } 
            StopCoroutine(StartWave());
        }

        
    }

    private void SpawnHamster(GameObject hamsterSpawn)
    {
        Instantiate(hamsterPrefab, hamsterSpawn.transform.position, hamsterSpawn.transform.rotation);
    }

    public void HamstersRemaining() //Called in kill function of hamster.
    {
        hamstersKilled++;
        if ( hamstersKilled >= hamstersPerWave[wave] * 2)
        {
            

            waveCompleted = true;
            wave++;
            hamstersKilled = 0;

            if (wave >= maxWaves)
            {
                manager.WinGame();
            }

        }
    }
}
