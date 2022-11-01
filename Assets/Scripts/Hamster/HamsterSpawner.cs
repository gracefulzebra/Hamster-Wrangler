using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HamsterSpawner : MonoBehaviour
{
    public GameObject hamster;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(hamsterSpawn());
    }

    IEnumerator hamsterSpawn()
    {
        for (; ; )
        {
            yield return new WaitForSeconds(4);
            Instantiate(hamster, transform.position, Quaternion.identity);
        }
    }
}
