using System.Collections;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    public float delay;
    bool decreaseSize;
    float timer;
    Vector3 startLerp;
    [SerializeField] int damage;
    bool explosionOver;


    // Start is called before the first frame update
    void Start()
    {
       // startLerp = new Vector3(-0.01f, -0.01f, -0.01f); ;
        //particle
        //audio
        StartCoroutine(ExplosionOver());
        StartCoroutine(DestroyExplosion());
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (decreaseSize)
        {
          //  GetComponentInChildren<Transform>().localScale += startLerp;
        }
    }

    IEnumerator ExplosionOver()
    {
        yield return new WaitForSeconds(0.5f);
        explosionOver = true;
    }

    IEnumerator DestroyExplosion()
    {
        yield return new WaitForSeconds(delay);
        decreaseSize = true;
        Destroy(gameObject);
        // change scale so it fades overtime
    }

    private void OnTriggerStay(Collider col)
    {
        if(!explosionOver)
        {
            if (col.gameObject.name == "Hamster 1(Clone)")
            {
                col.gameObject.GetComponent<ItemEffects>().InExplosionRadius(damage);
            }
        }
    }
}
