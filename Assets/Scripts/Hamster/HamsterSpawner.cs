using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HamsterSpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    public bool[] activeOnWave;
    [SerializeField] private GameObject houseLight;
    
    public Transform[] checkPoints;
    
    public bool checkActive(int waveToCheck)
    {
        if(waveToCheck > activeOnWave.Length) { print("Waves not assigned properly in HamsterSpawner"); return false; }

        return activeOnWave[waveToCheck]; 
    }

    public void StartRoutines()
    {
        StartCoroutine(GetComponent<PredictedPathRenderer>().AnimationHandler());
    }

    public void SetLight(int wave)
    {
        houseLight.SetActive(activeOnWave[wave]);
        if (activeOnWave[wave])
        {
            GetComponent<PredictedPathRenderer>().active = true;
        }
        else
        {
            GetComponent<PredictedPathRenderer>().active = false;
        }
    }

    public void StopPathAnimations()
    {
        GetComponent<PredictedPathRenderer>().StopPathAnimations();
    }

    public void SpawnHamster(GameObject hamsterPrefab)
    {
        GameObject hamsterInstance = Instantiate(hamsterPrefab, spawnPoint.position, Quaternion.identity);
        hamsterInstance.GetComponent<HamsterBase>().SetCheckPoints(checkPoints);
    }
}
