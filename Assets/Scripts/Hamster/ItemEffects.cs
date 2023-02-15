using System.Collections;
using UnityEngine;

public class ItemEffects : MonoBehaviour
{

    [Header("Item Effects")]
    float lighterDelay = 1;
    [SerializeField] GameObject cadaver;
    [SerializeField] GameObject fireEffect;
    private bool onFire = false;

    public void InExplosionRadius(int explosionDamage)
    {
        GetComponent<HamsterBase>().TakeDamage(explosionDamage);
    }

    public void OnFire(int burnDamage, float burnDuration, int burnAmount)
    {
        gameObject.GetComponentInChildren<Renderer>().material.color = new Color(0.91f, 0.3f, 0.21f);
        fireEffect.SetActive(true);

        if (onFire)
        {
            StopCoroutine(burnToDeath(burnDamage, burnDuration, burnAmount));
            StartCoroutine(burnToDeath(burnDamage, burnDuration, burnAmount));
        }
        else
        {
            StartCoroutine(burnToDeath(burnDamage, burnDuration, burnAmount));
        }
    }

    IEnumerator burnToDeath(int burnDamage, float burnDuration, int burnAmount)
    {
        onFire = true;
        for (int i=0; i < burnAmount; i++)
        {
            GetComponent<HamsterBase>().TakeDamage(burnDamage);
            yield return new WaitForSeconds(burnDuration);
        }
        onFire = false;
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
