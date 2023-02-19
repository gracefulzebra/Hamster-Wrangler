using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemEffects : MonoBehaviour
{

    [Header("Item Effects")]
    [SerializeField] GameObject fireEffect;
    private bool onFire = false;
    private int burnIndex = 0;
    public List<GameObject> hamsterNo = new List<GameObject>();
    float prevDist = 100;
    public float distance;

    public void InExplosionRadius(int explosionDamage)
    {
        GetComponent<HamsterBase>().TakeDamage(explosionDamage);
    }

    public void BugZapperDistance()
    {
        hamsterNo.Clear();

        GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag("Hamster");

        foreach (GameObject hamster in gameObjectArray)
        {
            hamsterNo.Add(hamster);
            distance = (transform.position - hamster.transform.position).magnitude;
            if (distance < prevDist)
            {
                prevDist = distance;
            }
        }
        if (prevDist < 4)
        {
            print("shocked");
        }

    }

    ///<summary>
    /// called when hamster interacts with fire 
    ///</summary>
    public void OnFire(int burnDamage, float burnDuration, int burnAmount)
    {
        gameObject.GetComponentInChildren<Renderer>().material.color = new Color(0.91f, 0.3f, 0.21f);
        fireEffect.SetActive(true);

        if (onFire)
        {
            burnIndex = 0;
        }
        else
        {
            StartCoroutine(burnToDeath(burnDamage, burnDuration, burnAmount));
        }
    }

    ///<summary>
    /// called when player is on fire to damage hamster
    ///</summary>
    IEnumerator burnToDeath(int burnDamage, float burnDuration, int burnAmount)
    {
        onFire = true;
        for (burnIndex = 0; burnIndex < burnAmount; burnIndex++)
        {
            yield return new WaitForSeconds(burnDuration);
            GetComponent<HamsterBase>().TakeDamage(burnDamage);
        }
        onFire = false;
    }

    ///<summary>
    /// called when hamster has been hit by bugzapper
    ///</summary>
    public void BeenElectrocuted(int electricDmg)
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        GetComponent<HamsterBase>().speed = 0;
        print("been shocked");
        GetComponent<HamsterBase>().TakeDamage(electricDmg);
        //play animation;
        StartCoroutine(ResetSpeed());
    }

    ///<summary>
    /// called once hamster has been electrocuted and values need to be reset
    ///</summary>
    IEnumerator ResetSpeed()
    {
        yield return new WaitForSeconds(1f);
        GetComponent<HamsterBase>().speed = GetComponent<HamsterBase>().maxSpeed;
    }
}
