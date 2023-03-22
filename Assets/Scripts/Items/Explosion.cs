using System.Collections;
using UnityEngine;

public class Explosion : TrapBase
{
    public float particleEffectRemovealDelay;
    
    void Start()
    {
        StartCoroutine(DestroyExplosion());
    }

    IEnumerator DestroyExplosion()
    {
        yield return new WaitForSeconds(particleEffectRemovealDelay);
        Destroy(gameObject);
    }

}
