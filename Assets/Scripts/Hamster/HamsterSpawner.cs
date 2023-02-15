using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HamsterSpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    
    public Transform[] checkPoints;
        
    public void SpawnHamster(GameObject hamsterPrefab)
    {
        GameObject hamsterInstance = Instantiate(hamsterPrefab, spawnPoint.position, Quaternion.identity);
        hamsterInstance.GetComponent<HamsterBase>().SetCheckPoints(checkPoints);
    }
}
