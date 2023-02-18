using System.Collections;
using UnityEngine;

public class ItemEffects : MonoBehaviour
{

    [Header("Item Effects")]
    float lighterDelay = 1;
    [SerializeField] GameObject fireEffect;
    private bool onFire = false;
    private int burnIndex = 0;

    public void InExplosionRadius(int explosionDamage)
    {
        GetComponent<HamsterBase>().TakeDamage(explosionDamage);
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
        GetComponent<HamsterBase>().speed = 0;
        GetComponent<HamsterBase>().TakeDamage(electricDmg);
        //play animation;
        StartCoroutine(ResetSpeed());
    }

    ///<summary>
    /// called once hamster has been electrocuted and values need to be reset
    ///</summary>
    IEnumerator ResetSpeed()
    {
        yield return new WaitForSeconds(2f);
        GetComponent<HamsterBase>().speed = GetComponent<HamsterBase>().maxSpeed;
    }

    ///<summary>
    /// when player is no longer in tar the flammablke affect will wear off 
    ///</summary>
    public void LeftTarArea()
    {
        StartCoroutine(RemoveTar());
    }

    IEnumerator RemoveTar()
    {
        yield return new WaitForSeconds(lighterDelay);
        gameObject.tag = "Untagged";
    }

    void OnCollisionEnter(Collision col)
    {
        // if a hamster is onfire he can set other hamster on fire
        if (col.gameObject.tag == "Flammable" && onFire)
        {
            //col.gameObject.GetComponent<ItemEffects>().OnFire();
        }
    }
}
