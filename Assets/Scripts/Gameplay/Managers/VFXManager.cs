using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    [Header("Hamster Limb Objects")]
    [SerializeField] private GameObject[] limbs;

    [Header("Customisation")]
    [Range(0f, 1.5f)]
    [SerializeField] private float spawnRadius;
    [Range(0f, 1.5f)]
    [SerializeField] private float spawnHeight;
    [SerializeField] private float limbProjectionForce;
    [Range(1, 5)]
    [SerializeField] private int limbQuantity;
    [SerializeField] private float limbDestroyDuration;
    [SerializeField] GameObject coinsDeath;

    private List<int> limbRestrictionList = new List<int>();

    public void HamsterDeathLimbSpawn(Transform hamsterPos)
    {
        Vector3 coinSpawnPos = new Vector3(hamsterPos.position.x, hamsterPos.position.y + 0.8f, hamsterPos.position.z);
        GameObject coinEffect = Instantiate(coinsDeath, coinSpawnPos, Quaternion.LookRotation(transform.up));
        StartCoroutine(DestroyCoins(coinEffect));

        limbRestrictionList.Clear();
        for(int i = 0; i < limbQuantity; i++)
        { SpawnLimb(ChooseLimb(), hamsterPos); }
    }

    private GameObject ChooseLimb()
    {
        int i = Random.Range(0, 5);
        
        while(limbRestrictionList.Contains(i))
        {
            i = Random.Range(0, 5);
        }
        limbRestrictionList.Add(i);
        
        return limbs[i];
    }

    private void SpawnLimb(GameObject limb, Transform hamsterPos)
    {
        Vector3 spawnPos = new Vector3(hamsterPos.position.x + Random.Range(-spawnRadius, spawnRadius), hamsterPos.position.y + spawnHeight, hamsterPos.position.z + Random.Range(-spawnRadius, spawnRadius));
        GameObject limbInstance = Instantiate(limb, spawnPos, Quaternion.identity);
        
        Vector3 force = LimbForce(hamsterPos, limbInstance.transform);
        limbInstance.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
        
        StartCoroutine(LimbDespawn(limbInstance, hamsterPos));
    }

    IEnumerator LimbDespawn(GameObject limbInstance, Transform hamsterPos)
    {
        yield return new WaitForSeconds(limbDestroyDuration);
        Destroy(limbInstance);
    }

    IEnumerator DestroyCoins(GameObject coinEffect)
    {
        yield return new WaitForSeconds(3f);
        Destroy(coinEffect);
    }

    private Vector3 LimbForce(Transform hamsterPos, Transform limbPos)
    {
        return (limbPos.position - hamsterPos.position).normalized * limbProjectionForce;
    }

}
