using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HamsterSpawner : MonoBehaviour
{
    public GameObject hamster;
    public Transform spawnPoint;
    

    // Start is called before the first frame update
   void SpawnHamster()
    {
        Instantiate(hamster, spawnPoint.position, Quaternion.identity);
    }
}
