using System.Collections;
using UnityEngine;

public class Explosion : TrapBase
{

    public float particleEffectRemovealDelay;
    bool explosionOver;

    // Start is called before the first frame update
    void Start()
    {
        trapInteractCounter = 2;
        itemID = "LawnMower";
        StartCoroutine(ExplosionOver());
        StartCoroutine(DestroyExplosion());
    }

    IEnumerator ExplosionOver()
    {
        print(trapInteractCounter);
        yield return new WaitForSeconds(0.2f);
        explosionOver = true;
    }

    IEnumerator DestroyExplosion()
    {
        yield return new WaitForSeconds(particleEffectRemovealDelay);
        Destroy(gameObject);
    }

    private void OnTriggerStay(Collider col)
    {
        if (!explosionOver)
        {
            if (col.gameObject.transform.CompareTag("Hamster"))
            {
                ItemInteract(col.gameObject);
                col.gameObject.GetComponent<ItemEffects>().InExplosionRadius(damage);
            }
        }
    }
}
