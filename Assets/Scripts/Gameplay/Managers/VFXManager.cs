using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    [Header("Hamster Limb Objects")]
    [SerializeField] private GameObject[] limbs;
    [SerializeField] private GameObject[] bones;

    [Header("Customisation")]
    [Range(0f, 1.5f)]
    [SerializeField] private float spawnRadius;
    [Range(0f, 1.5f)]
    [SerializeField] private float spawnHeight;
    [SerializeField] private float limbProjectionForce;
    [Range(1, 5)]
    [SerializeField] private int limbQuantity;
    [SerializeField] private float limbDestroyDuration; 
    [SerializeField] private float boneDestroyDuration;
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

    public void HamsterDeathBoneSpawn(Transform hamsterPos)
    {
        float offset = 0.4f;
        Vector3 headSpawnPos = hamsterPos.position + hamsterPos.forward * offset;

        GameObject skullInstance = Instantiate(bones[0], headSpawnPos, Quaternion.identity);

        Vector3 ribPos = hamsterPos.position - hamsterPos.forward * offset / 2 - hamsterPos.right * offset;

        GameObject ribInstance = Instantiate(bones[1], ribPos, Quaternion.identity);

        Vector3 leg1SpawnPos = hamsterPos.position + hamsterPos.forward * offset + hamsterPos.right * offset;

        GameObject legBone1Instance = Instantiate(bones[2], leg1SpawnPos, Quaternion.identity);

        Vector3 leg2SpawnPos = hamsterPos.position + hamsterPos.forward * offset - hamsterPos.right * offset;

        GameObject legBone2Instance = Instantiate(bones[3], leg2SpawnPos, Quaternion.identity);

        Vector3 backLeg1SpawnPos = hamsterPos.position - hamsterPos.forward * offset + hamsterPos.right * offset;

        GameObject backLeg1Instances = Instantiate(bones[4], backLeg1SpawnPos, Quaternion.identity);

        Vector3 backLeg2SpawnPos = hamsterPos.position - hamsterPos.forward * offset - hamsterPos.right * offset;

        GameObject backLeg2Instances = Instantiate(bones[5], backLeg2SpawnPos, Quaternion.identity);

        StartCoroutine(boneDespawn(skullInstance));
        StartCoroutine(boneDespawn(ribInstance));
        StartCoroutine(boneDespawn(legBone1Instance));
        StartCoroutine(boneDespawn(legBone2Instance));
        StartCoroutine(boneDespawn(backLeg1Instances));
        StartCoroutine(boneDespawn(backLeg2Instances));
    }

    IEnumerator boneDespawn(GameObject limbInstance)
    {
        yield return new WaitForSeconds(boneDestroyDuration);
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
