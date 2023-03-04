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
    [SerializeField] float LightningAOEDistance;

    int electricDamage;
    float shockDur;
    float hamsterShockRad;


   public bool canLightingAOE;

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

    ///<summary>
    /// called when hamster has been hit by bugzapper
    ///</summary>
    public void BeenElectrocuted(float shockDuration, int electricDmg, float hamsterShockRadius)
    {
        shockDur = shockDuration;
        electricDamage = electricDmg;
        hamsterShockRad = hamsterShockRadius;

        if (!hasBeenShocked)
        {
            hasBeenShocked = true;

            ElectricDamage();
            BugZapperDistance();

            //play animation;
        }
    }

    public void ElectricDamage()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        GetComponent<HamsterBase>().speed = 0;
        GetComponent<HamsterBase>().TakeDamage(electricDamage);
        StartCoroutine(ResetSpeed());
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

        if (prevDist < hamsterShockRad)
        {       
            if (nearHamster != null)
            {
                nearHamster.GetComponent<ItemEffects>().BeenElectrocuted(shockDur, electricDamage, hamsterShockRad);   
            }
        }
    }

    public void StartLightingAOE()
    {
        canLightingAOE = true;
    }

    void FinishLightningAOE()
    {

        GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag("Hamster");

        foreach (GameObject hamster in gameObjectArray)
        {
            distance = (transform.position - hamster.transform.position).magnitude;
            if (distance < 10)
            {
                hamster.GetComponent<ItemEffects>().ElectricDamage();
            }
        }
    }

    ///<summary>
    /// called once hamster has been electrocuted and values need to be reset
    ///</summary>
    IEnumerator ResetSpeed()
    {
        yield return new WaitForSeconds(shockDur);
        hasBeenShocked = false;
        GetComponent<HamsterBase>().speed = GetComponent<HamsterBase>().maxSpeed;
    }

    public void InExplosionRadius(int explosionDamage)
    {
        GetComponent<HamsterBase>().TakeDamage(explosionDamage);
    }

    private void OnCollisionEnter(Collision col)
    {
        if (canLightingAOE)
        {
            FinishLightningAOE();
            canLightingAOE = false;         
        }
    }
}



