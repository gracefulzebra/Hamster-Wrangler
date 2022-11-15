using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEffects : MonoBehaviour
{

    [Header("Item Effects")]
    WaitForSeconds delay = new WaitForSeconds(2);
    [SerializeField] GameObject cadaver;
   public bool onFire = false;

    ///<summary>
    ///Starts courtine that will kill hamster in 2 seconds 
    ///</summary>
    public void OnFire()
    {
        print("john");
        onFire = true;
      //  gameObject.GetComponent<Renderer>().material.color = Color.red;
        StartCoroutine(burnToDeath());
    }

    IEnumerator burnToDeath()
    {
        yield return delay;
        Vector3 offset = new Vector3(transform.position.x, transform.position.y -0.3f, transform.position.z);
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
        yield return delay;
        gameObject.tag = "Untagged";
        gameObject.GetComponent<HamsterBase>().speed *= 2;
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
