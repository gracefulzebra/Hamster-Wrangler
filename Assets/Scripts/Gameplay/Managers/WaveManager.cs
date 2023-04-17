using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    private GameObject[] hamsterSpawners;
    [SerializeField] private GameObject hamsterPrefab;
    [SerializeField] private int[] hamstersPerWave;
    [SerializeField] private float spawnDelay;
    private int hamstersKilled = 0;
    private int wave = 0;
    private int activeSpawnersAmount = 0;

    [HideInInspector] public int maxWaves;
    [HideInInspector] public bool waveCompleted;

    [SerializeField] private int endOfRoundCashBonus;

    private void Awake()
    {
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
        GameManager.instance.DisplayWaves(wave + 1, maxWaves);

        if (waveCompleted && wave < hamstersPerWave.Length) 
        {
            GameManager.instance.DisplayWaves(wave + 1, maxWaves);
            waveCompleted = false;
            for (int i = 0; i <= hamstersPerWave[wave] - 1; i++)
            {
                if (i != 0)
                    yield return new WaitForSeconds(spawnDelay);
                activeSpawnersAmount = 0;

                for (int j = 0; j < hamsterSpawners.Length; j++)
                {
                    if (hamsterSpawners[j].GetComponent<HamsterSpawner>().checkActive(wave))
                    {
                        activeSpawnersAmount++;
                        SpawnHamster(hamsterSpawners[j]);
                    }
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
        
        if (hamstersKilled >= hamstersPerWave[wave] * activeSpawnersAmount)
        {
            GameManager.instance.currencyManager.IncrementCurrency(endOfRoundCashBonus);

            waveCompleted = true;
            wave++;
            hamstersKilled = 0;
            if (wave >= maxWaves)
            {
                GameManager.instance.WinGame();
            }

        }
    }
}
