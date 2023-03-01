using System.Collections;
using UnityEngine;

public class ItemEffects : MonoBehaviour
{

    [Header("Blow Torch")]
    [SerializeField] GameObject fireEffect;
    private bool onFire = false;
    private int burnIndex = 0;

    [Header("Bug Zapper")]
    float prevDist = 100;
    float distance;
    GameObject nearHamster;
    public bool hasBeenShocked;
    int electricDmg = 5;
    float hamsterShockRadius = 50;

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
        fireEffect.SetActive(false);
    }

    public void ElectricDamage()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        GetComponent<HamsterBase>().speed = 0;
        GetComponent<HamsterBase>().TakeDamage(electricDmg);
    }

    ///<summary>
    /// called when hamster has been hit by bugzapper
    ///</summary>
    public void BeenElectrocuted()
    {
        if (!hasBeenShocked)
        {
            hasBeenShocked = true;
            BugZapperDistance();

            ElectricDamage();

            //play animation;
            StartCoroutine(ResetSpeed());
            GetComponent<HamsterScore>().UpdateInteracts(this.gameObject, "Tar"); ;
        }
    }

    public void BugZapperDistance()
    {
        nearHamster = null;

        GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag("Hamster");

        foreach (GameObject hamster in gameObjectArray)
        {
            distance = (transform.position - hamster.transform.position).magnitude;
            if (prevDist > distance && distance != 0)
            {
                prevDist = distance;
                nearHamster = hamster;
            }
        }
        hasBeenShocked = false;
        if (prevDist < hamsterShockRadius)
        {       
            if (nearHamster != null)
            {
                nearHamster.GetComponent<ItemEffects>().BeenElectrocuted();   
            }
        }
    }

    ///<summary>
    /// called once hamster has been electrocuted and values need to be reset
    ///</summary>
    IEnumerator ResetSpeed()
    {
        yield return new WaitForSeconds(1f);
        GetComponent<HamsterBase>().speed = GetComponent<HamsterBase>().maxSpeed;
    }

    public void InExplosionRadius(int explosionDamage)
    {
        GetComponent<HamsterBase>().TakeDamage(explosionDamage);
    }
}
