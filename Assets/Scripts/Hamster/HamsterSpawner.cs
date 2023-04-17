using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HamsterSpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    public bool[] activeOnWave;
    
    public Transform[] checkPoints;
    
    public bool checkActive(int waveToCheck)
    {
        if(waveToCheck > activeOnWave.Length) { print("Waves not assigned properly in HamsterSpawner"); return false; }

        return activeOnWave[waveToCheck]; 
    }

    public void SpawnHamster(GameObject hamsterPrefab)
    {
        GameObject hamsterInstance = Instantiate(hamsterPrefab, spawnPoint.position, Quaternion.identity);
        hamsterInstance.GetComponent<HamsterBase>().SetCheckPoints(checkPoints);
    }
}
