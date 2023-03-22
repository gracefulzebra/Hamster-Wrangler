using System.Collections;
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
    private int wave = 0;
    private int maxWaves;

    [SerializeField] private int endOfRoundCashBonus;

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
        if (waveCompleted && wave < hamstersPerWave.Length) 
        {
            manager.DisplayWaves(wave + 1, maxWaves);
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
        hamsterSpawn.GetComponent<HamsterSpawner>().SpawnHamster(hamsterPrefab);
    }

    public void HamstersRemaining() //Called in kill function of hamster.
    {
        hamstersKilled++;

        if ( hamstersKilled >= hamstersPerWave[wave] * hamsterSpawners.Length)
        {
            GameManager.instance.currencyManager.IncrementCurrency(endOfRoundCashBonus);

            waveCompleted = true;
            wave++;
            hamstersKilled = 0;

            if (wave >= maxWaves)
            {
                //GameManager.instance.WinGame();
            }

        }
       // manager.DisplayWaves(wave + 1, maxWaves);
    }
}
