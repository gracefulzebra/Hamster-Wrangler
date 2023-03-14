using System.Collections;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    public float particleEffectRemovealDelay;
    [SerializeField] int damage;
    bool explosionOver;

    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(ExplosionOver());
        StartCoroutine(DestroyExplosion());
    }

    IEnumerator ExplosionOver()
    {
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
        if(!explosionOver)
        {
            if (col.gameObject.transform.CompareTag("Hamster"))
            {
                col.gameObject.GetComponent<ItemEffects>().InExplosionRadius(damage);
            }
        }
    }
}
