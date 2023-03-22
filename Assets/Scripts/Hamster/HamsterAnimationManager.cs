using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HamsterAnimationManager : MonoBehaviour
{
    HamsterBase hamsterScript;
    Animator animator;
    public GameObject limb1;
    public GameObject limb2;
    public GameObject limb3;
    public GameObject limb4;
    public GameObject limb5;

    private void Start()
    {
       hamsterScript = GetComponentInParent<HamsterBase>();
       animator = GetComponent<Animator>();
    }

    public void LawnmowerDeathAnimation(Transform hamsterPos)
    {
        SpawnLimb(hamsterPos);
    }

    public void RakeLaunchAnimation()
    {
        animator.SetTrigger("RakeAnimationTrigger");

        animator.ResetTrigger("RakeAnimationTrigger");
    }

    private void SpawnLimb(Transform hamsterPos)
    {
        GameObject obj;
        for (int i = 0; i < 2; i++)
        {
            int j = Random.Range(1, 5);
            switch (j)
            {
                case 1:
                    obj = Instantiate(limb1, new Vector3(hamsterPos.position.x + Random.Range(-1f, 1f), 0.1f, hamsterPos.position.z + Random.Range(-1f, 1f)), Quaternion.identity);
                    break;
                case 2:
                    obj = Instantiate(limb2, new Vector3(hamsterPos.position.x + Random.Range(-1f, 1f), 0.1f, hamsterPos.position.z + Random.Range(-1f, 1f)), Quaternion.identity);
                    break;
                case 3:
                    obj = Instantiate(limb3, new Vector3(hamsterPos.position.x + Random.Range(-1f, 1f), 0.1f, hamsterPos.position.z + Random.Range(-1f, 1f)), Quaternion.identity);
                    break;
                case 4:
                    obj = Instantiate(limb4, new Vector3(hamsterPos.position.x + Random.Range(-1f, 1f), 0.1f, hamsterPos.position.z + Random.Range(-1f, 1f)), Quaternion.identity);
                    break;
                case 5:
                    obj = Instantiate(limb5, new Vector3(hamsterPos.position.x + Random.Range(-1f, 1f), 0.1f, hamsterPos.position.z + Random.Range(-1f, 1f)), Quaternion.identity);
                    break;
            }
        }
    }
}
