using System.Collections;
using UnityEngine;

public class ItemEffects : MonoBehaviour
{

    [Header("Item Effects")]
    WaitForSeconds delay = new WaitForSeconds(3);
    WaitForSeconds lighterDelay = new WaitForSeconds(1);
    [SerializeField] GameObject cadaver;
    [SerializeField] GameObject fireEffect;
    public bool onFire = false;

    ///<summary>
    ///Starts courtine that will kill hamster in 2 seconds 
    ///</summary>
    public void OnFire()
    {
        gameObject.GetComponentInChildren<Renderer>().material.color = new Color(0.91f, 0.3f, 0.21f);
        fireEffect.SetActive(true);
        StartCoroutine(burnToDeath());
    }

    public void InExplosionRadius()
    {
        print("doing explosion");
        gameObject.GetComponent<HamsterBase>().Kill();
    }

    IEnumerator burnToDeath()
    {
        yield return delay;
        Vector3 offset = new Vector3(transform.position.x, transform.position.y-0.15f, transform.position.z);
        Instantiate(cadaver, offset, Quaternion.identity);
        gameObject.GetComponent<HamsterBase>().Kill();
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
        yield return lighterDelay;
        gameObject.tag = "Untagged";
    }

    void OnCollisionEnter(Collision col)
    {
        // if a hamster is onfire he can set other hamster on fire
        if (col.gameObject.tag == "Flammable" && onFire)
        {
            col.gameObject.GetComponent<ItemEffects>().OnFire();
        }
    }
}
