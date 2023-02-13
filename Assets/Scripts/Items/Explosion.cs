using System.Collections;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    public float delay;
    bool hasExploded;
    bool decreaseSize;
    float timer;
    Vector3 startLerp;

    // Start is called before the first frame update
    void Start()
    {
        startLerp = new Vector3(-0.01f, -0.01f, -0.01f); ;
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
        hasExploded = true;
    }

    IEnumerator DestroyExplosion()
    {
        yield return new WaitForSeconds(delay);
        decreaseSize = true;
        // change scale so it fades overtime
    }

    private void OnTriggerEnter(Collider col)
    {
        if (hasExploded)
        {
            // deal damage and effects etc
        }
        else
        {
            if (col.gameObject.name == "Hamster1(Clone)")
            {
                GetComponent<ItemEffects>().OnFire();
            }
        }
    }
}
